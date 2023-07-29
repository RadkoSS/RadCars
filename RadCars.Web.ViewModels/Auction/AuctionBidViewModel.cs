namespace RadCars.Web.ViewModels.Auction;

using AutoMapper;

using Data.Models.Entities;
using Services.Mapping.Contracts;

public class AuctionBidViewModel : IMapFrom<UserAuctionBid>, IHaveCustomMappings
{
    public string BidderId { get; set; } = null!;

    public string BidderFullName { get; set; } = null!;

    public string BidderUserName { get; set; } = null!;

    public decimal Amount { get; set; }

    public string CreatedOn { get; set; } = null!;

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration
            .CreateMap<UserAuctionBid, AuctionBidViewModel>()
            .ForMember(destination => destination.CreatedOn,
                options => options.MapFrom(source => source.CreatedOn.ToString("G")));
    }
}