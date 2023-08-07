namespace RadCars.Web.ViewModels.Auction;

using AutoMapper;

using Common;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class AuctionIndexViewModel : BaseIndexViewModel, IMapFrom<Auction>, IHaveCustomMappings
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal StartingPrice { get; set; }

    public decimal CurrentPrice { get; set; }

    public bool? IsOver { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<Auction, AuctionIndexViewModel>()
            .ForMember(destination => destination.StartTime,
                options => options.MapFrom(source => source.StartTime.ToLocalTime()))
            .ForMember(destination => destination.EndTime,
                options => options.MapFrom(source => source.EndTime.ToLocalTime()));
    }
}