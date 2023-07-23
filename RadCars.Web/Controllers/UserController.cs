namespace RadCars.Web.Controllers;

using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

using ViewModels.User;
using Data.Models.User;

using static Common.NotificationTypeConstants;
using static Common.ExceptionsAndNotificationsMessages;

public class UserController : BaseController
{
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;

    public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Register()
    {
        var registerModel = new RegisterFormModel
        {
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        };

        return View(registerModel);
    }

    [HttpPost]
    [AllowAnonymous]
    //[ValidateRecaptcha(Action = nameof(Register),
    //    ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
    public async Task<IActionResult> Register(RegisterFormModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return View(model);
        }

        ApplicationUser user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
        };

        await this.userManager.SetEmailAsync(user, model.Email);
        await this.userManager.SetUserNameAsync(user, model.UserName);
        await this.userManager.SetPhoneNumberAsync(user, model.PhoneNumber);

        IdentityResult result =
            await this.userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        await this.signInManager.SignInAsync(user, false);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        LoginFormModel model = new LoginFormModel
        {
            ReturnUrl = returnUrl,
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        };

        return View(model);
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginFormModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return View(model);
        }

        var user = await this.userManager.FindByNameAsync(model.UserNameOrEmailAddress) ?? await this.userManager.FindByEmailAsync(model.UserNameOrEmailAddress);

        if (user == null)
        {
            TempData[ErrorMessage] =
                "Невалиден опит за влизане. Опитайте отново.";

            return View(model);
        }

        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

        if (!result.Succeeded)
        {
            TempData[ErrorMessage] =
                "Невалиден опит за влизане. Опитайте отново.";

            return View(model);
        }

        this.TempData[SuccessMessage] = LoginSuccessful;

        return Redirect(model.ReturnUrl ?? "/Home/Index");
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ExternalLogin(string provider, string returnUrl)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "User", new { ReturnUrl = returnUrl });
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return new ChallengeResult(provider, properties);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
    {
        returnUrl = returnUrl ?? Url.Content("~/");

        if (remoteError != null)
        {
            TempData[ErrorMessage] = $"Грешка от: {remoteError}";
            return RedirectToAction("Login", "User");
        }

        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            TempData[ErrorMessage] = "Грешка при зареждането на външен login.";
            return RedirectToAction("Login", "User");
        }

        // Sign in the user with this external login provider if the user already has a login.
        var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            // If the user does not have an account, then ask the user to create an account.
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var registerFormModel = new RegisterFormModel { Email = email };
            return RedirectToAction("Register", "User", registerFormModel);
        }
    }


    [HttpPost]
    public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            // Get the information about the user from the external login provider
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                throw new InvalidOperationException("Error loading external login information during confirmation.");
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            await this.userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
            await this.userManager.SetEmailAsync(user, model.Email);
            await this.userManager.SetUserNameAsync(user, model.UserName);

            var result = await userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                result = await userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        ViewData["ReturnUrl"] = returnUrl;
        return View("ExternalLogin", model);
    }

}