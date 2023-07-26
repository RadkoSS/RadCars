// ReSharper disable VirtualMemberCallInConstructor
namespace RadCars.Web.ViewModels.Common;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using City;
using CarMake;
using CarModel;
using CarEngineType;
using FeatureCategory;

using static RadCars.Common.EntityValidationConstants.ApplicationUser;
using static RadCars.Common.EntityValidationConstants.ListingConstants;

public abstract class BaseCreateFormModel
{
    protected BaseCreateFormModel()
    {
        this.Images = new HashSet<IFormFile>();
        this.Cities = new HashSet<CityViewModel>();
        this.SelectedFeatures = new HashSet<int>();
        this.CarMakes = new HashSet<CarMakeViewModel>();
        this.CarModels = new HashSet<CarModelViewModel>();
        this.EngineTypes = new HashSet<EngineTypeViewModel>();
        this.FeatureCategories = new HashSet<FeatureCategoriesViewModel>();
    }

    [Display(Name = "Заглавие")]
    [Required(ErrorMessage = "{0}то e задължително поле.")]
    [StringLength(TitleMaximumLength, MinimumLength = TitleMinimumLength, ErrorMessage = "{0}то трябва да има дължина между {2} и {1} символа.")]
    public string Title { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Телефонен номер")]
    [StringLength(PhoneNumberMaximumLength, MinimumLength = PhoneNumberMinimumLength, ErrorMessage = "Тел. номер трябва да има дължина между {2} и {1} символа. Пример: +359896141722 или 0896141722")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Описание")]
    [Required(ErrorMessage = "{0}то е задължително поле.")]
    [StringLength(DescriptionMaximumLength, MinimumLength = DescriptionMinimumLength, ErrorMessage = "{0}то трябва да има дължина между {2} и {1} символа.")]
    public string Description { get; set; } = null!;

    [Display(Name = "Година на производство")]
    [Required(ErrorMessage = "{0}та е задължително поле.")]
    [Range(YearMinimumValue, int.MaxValue, ErrorMessage = "{0}та трябва да е след {2}.")]
    public int Year { get; set; }

    [Display(Name = "Номер на рамата (VIN номер)")]
    [Required(ErrorMessage = "{0} е задължително поле.")]
    [StringLength(VinNumberMaximumLength, MinimumLength = VinNumberMinimumLength, ErrorMessage = "{0} трябва да е комбинация от числа и букви с дължина между {2} и {1} символа.")]
    public string VinNumber { get; set; } = null!;

    [Display(Name = "Пробег")]
    [Required(ErrorMessage = "{0}ът е задължително поле.")]
    [Range(MileageMinimum, int.MaxValue, ErrorMessage = "{0}ът не трябва да е число по-голямо от {2}.")]
    public int Mileage { get; set; }

    [Display(Name = "Модел на двигателя")]
    [Required(ErrorMessage = "{0} e задъжително поле.")]
    [StringLength(EngineModelMaximumLength, MinimumLength = EngineModelMinimumLength, ErrorMessage = "{0} трябва да има дължина от {2} до {1} символа.")]
    public string EngineModel { get; set; } = null!;

    [Display(Name = "Тип двигател")]
    [Required(ErrorMessage = "{0} е задължително поле.")]
    public int EngineTypeId { get; set; }

    public IEnumerable<EngineTypeViewModel> EngineTypes { get; set; }

    [Display(Name = "Населено място")]
    [Required(ErrorMessage = "{0} е задължително поле.")]
    [Range(1, int.MaxValue)]
    public int CityId { get; set; }

    public IEnumerable<CityViewModel> Cities { get; set; }

    [Required(ErrorMessage = "{0} е задължително поле.")]
    [Range(1, int.MaxValue)]
    [Display(Name = "Марка на автомобила")]
    public int CarMakeId { get; set; }

    public IEnumerable<CarMakeViewModel> CarMakes { get; set; }

    [Required(ErrorMessage = "{0} е задължително поле.")]
    [Display(Name = "Модел на автомобила")]
    public int CarModelId { get; set; }

    public IEnumerable<CarModelViewModel> CarModels { get; set; }

    [Display(Name = "Изберете какви екстри има автомовила:")]
    public IEnumerable<int> SelectedFeatures { get; set; }

    [Required(ErrorMessage = "{0}те са задължително поле.")]
    [Display(Name = "Снимки")]
    public virtual IEnumerable<IFormFile> Images { get; set; }

    public IEnumerable<FeatureCategoriesViewModel> FeatureCategories { get; set; }
}