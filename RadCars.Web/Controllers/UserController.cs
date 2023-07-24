namespace RadCars.Web.Controllers;

using System.Text;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Griesoft.AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;

using ViewModels.User;
using Data.Models.User;

using static Common.NotificationTypeConstants;
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

        if (!this.ModelState.IsValid)
        {
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

        if (!result.Succeeded)
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

        // Send the email. This will depend on how you've set up your email service.
        await emailSender.SendEmailAsync(model.Email, "Confirm your email",
            $"Please confirm your account by clicking this <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>link</a>.");

        // Check if the application requires email confirmation
        if (this.userManager.Options.SignIn.RequireConfirmedAccount)
        {
            // Redirect the user to a page instructing them to check their email
            return RedirectToPage("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl });
        }
        else
        {
            // If the application doesn't require email confirmation, sign the user in and redirect them to the home page

            this.TempData[SuccessMessage] = RegistrationSuccessful;

            await this.signInManager.SignInAsync(user, false);

            return LocalRedirect(model.ReturnUrl);
            //return RedirectToAction("Index", "Home");
        }
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

        if (!this.ModelState.IsValid)
        {
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }

        var user = await this.userManager.FindByNameAsync(model.UserNameOrEmailAddress) ?? await this.userManager.FindByEmailAsync(model.UserNameOrEmailAddress);

        if (user == null)
        {
            TempData[ErrorMessage] =
                "Невалиден опит за влизане. Опитайте отново.";
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View(model);
        }

        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

        if (!result.Succeeded)
        {
            TempData[ErrorMessage] =
                "Невалиден опит за влизане. Опитайте отново.";
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View(model);
        }

        this.TempData[SuccessMessage] = LoginSuccessful;

        return Redirect(model.ReturnUrl ?? "/Home/Index");
    }
}