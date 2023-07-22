namespace RadCars.Services.Data.Models.Listing;

using Web.ViewModels.Listing;

public class AllListingsFilteredAndPagedServiceModel
{
    public AllListingsFilteredAndPagedServiceModel()
    {
        this.Listings = new HashSet<AllListingsViewModel>();
    }

    public int TotalListingsCount { get; set; }

    public IEnumerable<AllListingsViewModel> Listings { get; set; }
}