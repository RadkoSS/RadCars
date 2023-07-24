// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

namespace RadCars.Web.Areas.Identity.Pages.Account;

using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

using Data.Models.User;

public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<ApplicationUser> userManager;

    public ConfirmEmailModel(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    [TempData]
    public string StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return RedirectToPage("/Index");
        }

        var user = await this.userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await this.userManager.ConfirmEmailAsync(user, code);
        StatusMessage = result.Succeeded ? "Благодарим, че потвърдихте електронната си поща! Добре дошли в RadCars." : "Неуспешно потвърждаване на електронна поща.";
        return Page();
    }
}