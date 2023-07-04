namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;
using City;
using Microsoft.AspNetCore.Http;

using static Common.EntityValidationConstants.ListingConstants;

public class CreateListingFormModel
{
    public CreateListingFormModel()
    {
        this.Pictures = new HashSet<IFormFile>();
        this.Cities = new HashSet<CityViewModel>();
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
    [Range(1, ushort.MaxValue)]
    public ushort CityId { get; set; }

    public IEnumerable<CityViewModel> Cities { get; set; }

    //ToDo: add all properties needed!

    public IEnumerable<IFormFile> Pictures { get; set; }
}