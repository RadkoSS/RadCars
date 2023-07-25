namespace RadCars.Web.Controllers;

using System.Text;
using System.Security.Claims;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Griesoft.AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

using ViewModels.User;
using Data.Models.User;
using Services.Messaging.Contracts;

using static Common.NotificationTypeConstants;
using static Common.GeneralApplicationConstants;
using static Common.ExceptionsAndNotificationsMessages;

public class UserController : BaseController
{
    private readonly IEmailSender emailSender;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;

    public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        this.emailSender = emailSender;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Register(string returnUrl = null)
    {
        var registerModel = new RegisterFormModel
        {
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            ReturnUrl = returnUrl
        };

        return View(registerModel);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateRecaptcha(Action = nameof(Register),
        ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
    public async Task<IActionResult> Register(RegisterFormModel model)
    {
        model.ReturnUrl ??= Url.Content("~/");

        if (this.ModelState.IsValid == false)
        {
            this.TempData[ErrorMessage] = InvalidDataProvidedError;
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }

        ApplicationUser user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        await this.userManager.SetEmailAsync(user, model.Email);
        await this.userManager.SetUserNameAsync(user, model.UserName);
        await this.userManager.SetPhoneNumberAsync(user, model.PhoneNumber);

        IdentityResult result = await this.userManager.CreateAsync(user, model.Password);

        if (result.Succeeded == false)
        {
            foreach (IdentityError error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }

        var userId = await this.userManager.GetUserIdAsync(user);

        // Generate the email confirmation token
        var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        // Create the confirmation link
        var confirmationLink = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId = userId, code = token, returnUrl = model.ReturnUrl },
            protocol: Request.Scheme);

        // Send the email.
        await emailSender.SendEmailAsync(SendGridSenderEmail, SendGridSenderName, model.Email,"Потвърждаване на регистрация",
            $"Здравейте, {model.FirstName} {model.LastName}, добре дошли в RadCars! Моля, потвърдете своята регистрация от тази връзка --> <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>link</a>.");

        if (this.userManager.Options.SignIn.RequireConfirmedAccount == false)
        {
            // If the application doesn't require email confirmation, sign the user in and redirect them to the home page

            await this.signInManager.SignInAsync(user, false);
            this.TempData[SuccessMessage] = RegistrationSuccessful;

            return LocalRedirect(model.ReturnUrl);
        }

        // Redirect the user to a page instructing them to check their email
        var confirmationViewModel = new RegisterConfirmationViewModel
        {
            Email = model.Email,
            FullName = $"{model.FirstName} {model.LastName}"
        };
        this.TempData[SuccessMessage] = RegistrationSuccessful + " Моля, потвърдете имейла си!";

        return View("RegisterConfirmation", confirmationViewModel);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        returnUrl ??= Url.Content("~/");

        LoginFormModel model = new LoginFormModel
        {
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            ReturnUrl = returnUrl
        };

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginFormModel model)
    {
        model.ReturnUrl ??= Url.Content("~/");

        if (this.ModelState.IsValid == false)
        {
            this.TempData[ErrorMessage] = InvalidDataProvidedError;
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }

        var user = await this.userManager.FindByNameAsync(model.UserNameOrEmailAddress) ?? await this.userManager.FindByEmailAsync(model.UserNameOrEmailAddress);

        if (user == null)
        {
            this.TempData[ErrorMessage] = "Невалиден опит за влизане. Опитайте отново.";
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View(model);
        }

        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

        if (result.Succeeded == false)
        {
            this.TempData[ErrorMessage] =
                "Невалиден опит за влизане. Опитайте отново.";
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View(model);
        }

        this.TempData[SuccessMessage] = LoginSuccessful;

        return Redirect(model.ReturnUrl ?? "/Home/Index");
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ExternalLogin(string provider, string returnUrl = null)
    {
        // Request a redirect to the external login provider.
        var redirectUrl = Url.Action("Callback", "User", new { returnUrl });
        var properties = this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return new ChallengeResult(provider, properties);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> CallbackAsync(string returnUrl = null, string remoteError = null)
    {
        returnUrl = returnUrl ?? Url.Content("~/");
        if (remoteError != null)
        {
            this.TempData[ErrorMessage] = $"Грешка от външен логин: {remoteError}";
            return RedirectToAction("Login", "User", new { ReturnUrl = returnUrl });
        }
        var info = await this.signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            this.TempData[ErrorMessage] = $"Грешка при зареждането на информация от {remoteError}";
            return RedirectToAction("Login", "User", new { ReturnUrl = returnUrl });
        }
        
        //ToDo: add validation if the user has already registered with the email provided to the loginProvider, get their user and log them in to the system without asking for password or redirecting to the Login action.

        var result = await this.signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        if (result.Succeeded)
        {
            this.TempData[SuccessMessage] = LoginSuccessful;
            return LocalRedirect(returnUrl);
        }

        // If the user does not have an account, then ask the user to create an account.
        this.ViewData["ProviderDisplayName"] = info.LoginProvider;

        var externalRegisterInputModel = new ExternalRegistrationInputModel();
        externalRegisterInputModel.ReturnUrl = returnUrl;

        if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
        {
            externalRegisterInputModel.Email = info.Principal.FindFirstValue(ClaimTypes.Email);
        }

        this.TempData[SuccessMessage] = "Въведете допълнителните данни, за да се регистрирате.";
        return View("ExternalLogin", externalRegisterInputModel);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateRecaptcha(Action = nameof(ConfirmationAsync),
        ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
    public async Task<IActionResult> ConfirmationAsync(ExternalRegistrationInputModel input)
    {
        input.ReturnUrl ??= Url.Content("~/");

        var info = await this.signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            this.TempData[ErrorMessage] = "Грешка при зареждането на данните за външния login по време на потвърждаването.";
            return RedirectToAction("Login", "User", new { input.ReturnUrl });
        }

        if (this.ModelState.IsValid == false)
        {
            this.ViewData["ProviderDisplayName"] = info.LoginProvider;
            TempData[ErrorMessage] = InvalidDataProvidedError;
            return View("ExternalLogin", input);
        }

        var userByEmail = await this.userManager.FindByEmailAsync(input.Email);
        var userByUserName = await this.userManager.FindByNameAsync(input.UserName);

        if (userByEmail != null || userByUserName != null)
        {
            this.TempData[ErrorMessage] = "Вече сте регистрирани в сайта!";

            return RedirectToAction("Login", "User");
        }

        var user = new ApplicationUser
        {
            FirstName = input.FirstName,
            LastName = input.LastName
        };

        await this.userManager.SetUserNameAsync(user, input.UserName);
        await this.userManager.SetPhoneNumberAsync(user, input.PhoneNumber);
        await this.userManager.SetEmailAsync(user, input.Email);

        var result = await this.userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            result = await this.userManager.AddLoginAsync(user, info);
            if (result.Succeeded)
            {
                var userId = await this.userManager.GetUserIdAsync(user);

                // Generate the email confirmation token
                var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                // Create the confirmation link
                var confirmationLink = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = token, returnUrl = input.ReturnUrl },
                    protocol: Request.Scheme);
                
                await emailSender.SendEmailAsync(SendGridSenderEmail, SendGridSenderName, input.Email, "Потвърждаване на регистрация",
                    $"Здравейте, {input.FirstName} {input.LastName}, добре дошли в RadCars! Моля, потвърдете своята регистрация от тази връзка --> <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>link</a>.");

                if (this.userManager.Options.SignIn.RequireConfirmedAccount == false)
                {
                    // If the application doesn't require email confirmation, sign the user in and redirect them to the home page

                    await this.signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                    this.TempData[SuccessMessage] = LoginSuccessful;
                    return LocalRedirect(input.ReturnUrl);
                }

                var confirmationViewModel = new RegisterConfirmationViewModel
                {
                    Email = input.Email,
                    FullName = $"{input.FirstName} {input.LastName}"
                };
                this.TempData[SuccessMessage] = RegistrationSuccessful + " Моля, потвърдете имейла си!";

                return View("RegisterConfirmation", confirmationViewModel);
            }
        }

        this.ViewData["ProviderDisplayName"] = info.LoginProvider;
        TempData[ErrorMessage] = UnsuccessfulRegistration;
        return View("ExternalLogin", input);
    }
}