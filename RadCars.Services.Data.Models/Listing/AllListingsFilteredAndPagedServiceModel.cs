namespace RadCars.Services.Data.Models.Listing;

using Web.ViewModels.Listing;

public class AllListingsFilteredAndPagedServiceModel
{
    public AllListingsFilteredAndPagedServiceModel()
    {
        this.Listings = new HashSet<AllListingViewModel>();
    }

    public int TotalListingsCount { get; set; }

    public IEnumerable<AllListingViewModel> Listings { get; set; }
}