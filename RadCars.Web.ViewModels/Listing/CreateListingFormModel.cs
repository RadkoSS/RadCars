namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using City;
using CarMake;
using CarModel;
using CarEngineType;
using FeatureCategory;

using static Common.EntityValidationConstants.ListingConstants;

public class CreateListingFormModel
{
    public CreateListingFormModel()
    {
        this.Images = new HashSet<IFormFile>();
        this.Cities = new HashSet<CityViewModel>();
        this.SelectedFeatures = new HashSet<ushort>();
        this.CarMakes = new HashSet<CarMakeViewModel>();
        this.CarModels = new HashSet<CarModelViewModel>();
        this.EngineTypes = new HashSet<CarEngineTypeViewModel>();
        this.FeatureCategories = new HashSet<FeatureCategoriesViewModel>();
    }

    [StringLength(TitleMaximumLength, MinimumLength = TitleMinimumLength)]
    public string Title { get; set; } = null!;

    [StringLength(DescriptionMaximumLength, MinimumLength = DescriptionMinimumLength)]
    public string Description { get; set; } = null!;

    [Range(YearMinimumValue, ushort.MaxValue)]
    public ushort Year { get; set; }

    [Required]
    [Range(typeof(decimal), PriceMinimum, PriceMaximum)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(VinNumberMaximumLength, MinimumLength = VinNumberMinimumLength)]
    public string VinNumber { get; set; } = null!;

    [Required]
    [Range(MileageMinimum, uint.MaxValue)]
    public uint Mileage { get; set; }

    [Required]
    [StringLength(EngineModelMaximumLength, MinimumLength = EngineModelMinimumLength)]
    public string EngineModel { get; set; } = null!;

    [Required]
    [Range(EngineTypeMinimum, EngineTypeMaximum)]
    public byte EngineTypeId { get; set; }

    public IEnumerable<CarEngineTypeViewModel> EngineTypes { get; set; }

    [Required]
    [Range(1, ushort.MaxValue)]
    public ushort CityId { get; set; }

    public IEnumerable<CityViewModel> Cities { get; set; }

    [Required]
    [Range(1, ushort.MaxValue)]
    public ushort CarMakeId { get; set; }

    public IEnumerable<CarMakeViewModel> CarMakes { get; set; }

    [Required]
    [Range(1, ushort.MaxValue)]
    public ushort CarModelId { get; set; }

    public IEnumerable<CarModelViewModel> CarModels { get; set; }

    //ToDo: Check if this works!
    public IEnumerable<ushort> SelectedFeatures { get; set; }

    public IEnumerable<FeatureCategoriesViewModel> FeatureCategories { get; set; }

    public IEnumerable<IFormFile> Images { get; set; }
}