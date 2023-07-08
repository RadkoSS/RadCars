namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using City;
using CarMake;
using CarModel;
using CarEngineType;
using FeatureCategory;

using static Common.EntityValidationConstants.ListingConstants;

public class ListingFormModel
{
    public ListingFormModel()
    {
        this.Images = new HashSet<IFormFile>();
        this.Cities = new HashSet<CityViewModel>();
        this.SelectedFeatures = new HashSet<ushort>();
        this.CarMakes = new HashSet<CarMakeViewModel>();
        this.CarModels = new HashSet<CarModelViewModel>();
        this.EngineTypes = new HashSet<EngineTypeViewModel>();
        this.FeatureCategories = new HashSet<FeatureCategoriesViewModel>();
    }

    [StringLength(TitleMaximumLength, MinimumLength = TitleMinimumLength)]
    [Display(Name = "Заглавие")]
    public string Title { get; set; } = null!;

    [StringLength(DescriptionMaximumLength, MinimumLength = DescriptionMinimumLength)]
    [Display(Name = "Описание")]
    public string Description { get; set; } = null!;

    [Range(YearMinimumValue, ushort.MaxValue)]
    [Display(Name = "Година на производство")]
    public ushort Year { get; set; }

    [Required]
    [Range(typeof(decimal), PriceMinimum, PriceMaximum)]
    [Display(Name = "Цена")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(VinNumberMaximumLength, MinimumLength = VinNumberMinimumLength)]
    [Display(Name = "Номер на рамата (VIN номер)")]
    public string VinNumber { get; set; } = null!;

    [Required]
    [Range(MileageMinimum, uint.MaxValue)]
    [Display(Name = "Пробег")]
    public uint Mileage { get; set; }

    [Required]
    [StringLength(EngineModelMaximumLength, MinimumLength = EngineModelMinimumLength)]
    [Display(Name = "Модел на двигателя")]
    public string EngineModel { get; set; } = null!;

    [Required]
    [Display(Name = "Тип двигател")]
    public byte EngineTypeId { get; set; }

    public IEnumerable<EngineTypeViewModel> EngineTypes { get; set; }

    [Required]
    [Range(1, ushort.MaxValue)]
    [Display(Name = "Местоположение")]
    public ushort CityId { get; set; }

    public IEnumerable<CityViewModel> Cities { get; set; }

    [Required]
    [Range(1, ushort.MaxValue)]
    [Display(Name = "Марка на автомобила")]
    public ushort CarMakeId { get; set; }

    public IEnumerable<CarMakeViewModel> CarMakes { get; set; }

    [Required]
    [Display(Name = "Модел на автомобила")]
    public ushort CarModelId { get; set; }

    public IEnumerable<CarModelViewModel> CarModels { get; set; }

    [Display(Name = "Списък на екстрите")]
    public IEnumerable<ushort> SelectedFeatures { get; set; }

    public IEnumerable<FeatureCategoriesViewModel> FeatureCategories { get; set; }

    [Required]
    [Display(Name = "Снимки")]
    public IEnumerable<IFormFile> Images { get; set; }
}