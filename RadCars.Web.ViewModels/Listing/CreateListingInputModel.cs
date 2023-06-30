namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using static Common.EntityValidationConstants.ListingConstants;

public class CreateListingInputModel
{
    [StringLength(TitleMaximumLength, MinimumLength = TitleMinimumLength)]
    public string Title { get; set; } = null!;

    [StringLength(DescriptionMaximumLength, MinimumLength = DescriptionMinimumLength)]
    public string Description { get; set; } = null!;

    [Range(YearMinimumValue, ushort.MaxValue)]
    public ushort Year { get; set; }

    //ToDo: add all properties needed!

    public IEnumerable<IFormFile> Pictures { get; set; } = null!;
}