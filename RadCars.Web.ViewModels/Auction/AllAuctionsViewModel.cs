namespace RadCars.Web.ViewModels.Auction;

using AutoMapper;

using City;
using Common;
using CarImage;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class AllAuctionsViewModel : BaseAllViewModel, IMapFrom<Auction>, IMapFrom<UserFavoriteAuction>, IHaveCustomMappings
{
    //public decimal CurrentPrice { get; set; }
    public decimal StartingPrice { get; set; }

    public virtual void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<UserFavoriteAuction, AllAuctionsViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AuctionId.ToString()))
            .ForMember(dest => dest.CarMakeName, opt => opt.MapFrom(src => src.Auction.CarMake.Name))
            .ForMember(dest => dest.CarModelName, opt => opt.MapFrom(src => src.Auction.CarModel.Name))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => new CityViewModel
            {
                Id = src.Auction.City.Id,
                Name = src.Auction.City.Name
            }))
            .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => new ImageViewModel
            {
                Id = src.Auction.ThumbnailId.ToString()!,
                Url = src.Auction.Thumbnail!.Url
            }))
            .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.Auction.CreatorId.ToString()))
            .ForMember(dest => dest.EngineModel, opt => opt.MapFrom(src => src.Auction.EngineModel))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Auction.Mileage))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Auction.Title))
            .ForMember(dest => dest.StartingPrice, opt => opt.MapFrom(src => src.Auction.StartingPrice))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Auction.Year));
    }
}