namespace RadCars.Web.ViewModels.FeatureCategory;

using System.ComponentModel.DataAnnotations;

using Data.Models.Entities;
using Services.Mapping.Contracts;

using static RadCars.Common.EntityValidationConstants.FeatureCategoryConstants;

public class FeatureCategoryFormModel : IMapFrom<Category>
{
    [Required]
    public int Id { get; set; }

    [Display(Name = "Име на категория")]
    [Required(ErrorMessage = "{0} е задължително поле.")]
    [StringLength(NameMaximumLength, MinimumLength = NameMinimumLength, ErrorMessage = "{0} трябва да има дължина между {2} и {1} символа.")]
    public string Name { get; set; } = null!;
}