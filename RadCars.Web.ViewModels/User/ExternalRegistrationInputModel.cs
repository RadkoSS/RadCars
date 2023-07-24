namespace RadCars.Web.ViewModels.User;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.ApplicationUser;

public class ExternalRegistrationInputModel
{
    [Required]
    [Display(Name = "Имейл адрес")]
    [DataType(DataType.EmailAddress)]
    [StringLength(EmailMaximumLength, MinimumLength = EmailMinimumLength, ErrorMessage = "{0}ът трябва да е с дължина между {2} и {1} символа.")]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Име")]
    [StringLength(FirstNameMaximumLength, MinimumLength = FirstNameMinimumLength, ErrorMessage = "{0}то трябва да е с дължина между {2} и {1} символа.")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Фамилия")]
    [StringLength(LastNameMaximumLength, MinimumLength = LastNameMinimumLength, ErrorMessage = "{0}та трябва да е с дължина между {2} и {1} символа.")]
    public string LastName { get; set; } = null!;

    [Required]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Телефонен номер")]
    [StringLength(PhoneNumberMaximumLength, MinimumLength = PhoneNumberMinimumLength, ErrorMessage = "Телефонният номер не е валиден.")]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [Display(Name = "Потребителско име")]
    [StringLength(UserNameMaxLength, MinimumLength = UserNameMinimumLength, ErrorMessage = "Потребителското име трябва да е с дължина между {2} и {1} символа.")]
    [RegularExpression(@"^[a-z0-9]([._]?[a-z0-9]){2,49}$", ErrorMessage = "Потребителското име трябва да започва с буква и може да съдържа само малки букви, цифри, точки и долни черти.")]
    public string UserName { get; set; } = null!;

    public string? ReturnUrl { get; set; }
}