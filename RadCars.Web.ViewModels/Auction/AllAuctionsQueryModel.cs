namespace RadCars.Web.ViewModels.Auction;

using System.ComponentModel.DataAnnotations;

using Enums;
using Common;

using static RadCars.Common.GeneralApplicationConstants;

public class AllAuctionsQueryModel : BaseAllQueryModel
{
    public AllAuctionsQueryModel()
    {
        this.AuctionsPerPage = EntitiesPerPage;

        this.Auctions = new HashSet<AllAuctionsViewModel>();
    }

    [Display(Name = "Сортиране:")]
    public AuctionSorting AuctionSorting { get; set; }

    [Display(Name = "Брой търгове на страница:")]
    public int AuctionsPerPage { get; set; }

    public int TotalAuctions { get; set; }

    public IEnumerable<AllAuctionsViewModel> Auctions { get; set; }
}