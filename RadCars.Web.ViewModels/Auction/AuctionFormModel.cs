namespace RadCars.Web.ViewModels.Auction;

using AutoMapper;

using Common;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class AuctionFormModel : BaseCreateFormModel, IMapFrom<Auction>, IMapTo<Auction>, IHaveCustomMappings
{


    public virtual void CreateMappings(IProfileExpression configuration)
    {
        configuration
            .CreateMap<AuctionFormModel, Auction>()
            .ForMember(destination => destination.AuctionFeatures, options => options.Ignore())
            .ForMember(destination => destination.Images, options => options.Ignore());
    }
}