namespace RadCars.Web.ViewModels.Auction;

using Common;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class AuctionIndexViewModel : BaseIndexViewModel, IMapFrom<Auction>
{
    public decimal StartingPrice { get; set; }
}