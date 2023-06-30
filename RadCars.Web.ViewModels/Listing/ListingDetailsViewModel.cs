namespace RadCars.Web.ViewModels.Listing;

using CarPicture;

public class ListingDetailsViewModel : ListingViewModel
{
    //ToDo: Add all the needed properties to display the details about each car listing!

    public ICollection<PictureViewModel> Pictures { get; set; } = null!;
}