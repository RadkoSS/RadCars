// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

namespace RadCars.Web.Areas.Identity.Pages.Account;

using System.Text;
using System.Text.Encodings.Web;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Data.Models.User;

using Services.Messaging.Contracts;

using static Common.GeneralApplicationConstants;
using static Common.EntityValidationConstants.ApplicationUser;

public class ForgotPasswordModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }
    
    [BindProperty]
    public InputModel Input { get; set; }
    
    public class InputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Имейл адрес")]
        [StringLength(EmailMaximumLength, MinimumLength = EmailMinimumLength, ErrorMessage = "{0}ът трябва да е с дължина между {2} и {1} символа.")]
        public string Email { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(SendGridSenderEmail, SendGridSenderName,
                Input.Email,
                "Смяна на парола",
                $"Моля, последвайте този <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>линк</a>, за да смените паролата си.");

            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        return Page();
    }
}