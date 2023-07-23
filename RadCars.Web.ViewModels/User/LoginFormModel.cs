namespace RadCars.Web.ViewModels.User;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authentication;

public class LoginFormModel
{
    public LoginFormModel()
    {
        this.ExternalLogins = new List<AuthenticationScheme>();
    }

    [Required]
    [Display(Name = "Имейл адрес или потребителско име")]
    public string UserNameOrEmailAddress { get; set; } = null!;

    [Required]
    [Display(Name = "Парола")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Запомни ме")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }
}