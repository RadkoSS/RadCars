namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using City;
using CarMake;
using FeatureCategory;

using static Common.EntityValidationConstants.ListingConstants;

public class CreateListingFormModel
{
    public CreateListingFormModel()
    {
        this.Pictures = new HashSet<IFormFile>();
        this.Cities = new HashSet<CityViewModel>();
        this.FeatureCategories = new HashSet<FeatureCategoriesViewModel>();
        this.CarMakes = new HashSet<CarMakeViewModel>();
    }

    [StringLength(TitleMaximumLength, MinimumLength = TitleMinimumLength)]
    public string Title { get; set; } = null!;

    [StringLength(DescriptionMaximumLength, MinimumLength = DescriptionMinimumLength)]
    public string Description { get; set; } = null!;

    [Range(YearMinimumValue, ushort.MaxValue)]
    public ushort Year { get; set; }

    [Required]
    //[Range(1, decimal.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(VinNumberMaximumLength, MinimumLength = VinNumberMinimumLength)]
    public string VinNumber { get; set; } = null!;

    [Required]
    public uint Mileage { get; set; }

    [Required]
    [Range(EngineTypeMinimum, EngineTypeMaximum)]
    public int EngineTypeId { get; set; }
    

    [Required]
    [Range(1, ushort.MaxValue)]
    public ushort CityId { get; set; }

    public IEnumerable<CityViewModel> Cities { get; set; }

    [Required]
    public ushort CarMakeId { get; set; }

    public IEnumerable<CarMakeViewModel> CarMakes { get; set; }

    //ToDo: How will we map the choices?
    public IEnumerable<FeatureCategoriesViewModel> FeatureCategories { get; set; }
    


    public IEnumerable<IFormFile> Pictures { get; set; }
}