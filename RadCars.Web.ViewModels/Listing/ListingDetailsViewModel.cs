namespace RadCars.Web.ViewModels.Listing;

using CarImage;

public class ListingDetailsViewModel : ListingViewModel
{
    //ToDo: Add all the needed properties to display the details about each car listing!

    public ICollection<ImageViewModel> Pictures { get; set; } = null!;
}