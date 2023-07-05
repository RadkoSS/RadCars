namespace RadCars.Web.ViewModels.Listing;

using CarImage;

public class ListingViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public ushort Year { get; set; }

    public string CreatorName { get; set; } = null!;

    public ImageViewModel Thumbnail { get; set; } = null!;

    //ToDo: add all properties needed!
}