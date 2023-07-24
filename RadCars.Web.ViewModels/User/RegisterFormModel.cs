namespace RadCars.Web.ViewModels.User;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authentication;

using static Common.EntityValidationConstants.ApplicationUser;

public class RegisterFormModel : ExternalRegistrationInputModel
{
    public RegisterFormModel()
    {
        this.ExternalLogins = new List<AuthenticationScheme>();
    }

    [Required]
    [Display(Name = "Парола")]
    [DataType(DataType.Password)]
    [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = "{0}та трябва да е с дължина между {2} и {1} символа.")]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Потвърди парола")]
    [Compare(nameof(Password), ErrorMessage = "Полетата \"Парола\" и \"Потвърди парола\" трябва да съвпадат!")]
    public string ConfirmPassword { get; set; } = null!;

    public IList<AuthenticationScheme> ExternalLogins { get; set; }
}