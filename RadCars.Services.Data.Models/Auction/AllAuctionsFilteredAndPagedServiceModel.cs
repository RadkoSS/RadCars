namespace RadCars.Services.Data.Models.Auction;

using Web.ViewModels.Auction;

public class AllAuctionsFilteredAndPagedServiceModel
{
    public AllAuctionsFilteredAndPagedServiceModel()
    {
        this.Auctions = new HashSet<AllAuctionsViewModel>();
    }

    public int TotalAuctionsCount { get; set; }

    public IEnumerable<AllAuctionsViewModel> Auctions { get; set; }
}