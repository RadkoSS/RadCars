namespace RadCars.Web.ViewModels.Home;

using Auction;
using Listing;

public class IndexViewModel
{
    public IndexViewModel()
    {
        this.Auctions = new HashSet<AuctionIndexViewModel>();
        this.Listings = new HashSet<ListingIndexViewModel>();
    }

    public IEnumerable<AuctionIndexViewModel> Auctions { get; set; }

    public IEnumerable<ListingIndexViewModel> Listings { get; set; }
}