namespace RadCars.Web.ViewModels.Listing;

using City;
using CarImage;
using AutoMapper;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class AllListingsViewModel : IMapFrom<Listing>, IMapFrom<UserFavoriteListing>, IHaveCustomMappings
{
    public string Id { get; set; } = null!;

    public string CreatorId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public int Mileage { get; set; }

    public string EngineModel { get; set; } = null!;

    public string CarMakeName { get; set; } = null!;

    public string CarModelName { get; set; } = null!;

    public CityViewModel City { get; set; } = null!;

    public int Year { get; set; }

    public ImageViewModel Thumbnail { get; set; } = null!;

    public virtual void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<UserFavoriteListing, AllListingsViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ListingId.ToString()))
            .ForMember(dest => dest.CarMakeName, opt => opt.MapFrom(src => src.Listing.CarMake.Name))
            .ForMember(dest => dest.CarModelName, opt => opt.MapFrom(src => src.Listing.CarModel.Name))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => new CityViewModel
            {
                Id = src.Listing.City.Id,
                Name = src.Listing.City.Name
            }))
            .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => new ImageViewModel
            {
                Id = src.Listing.ThumbnailId.ToString()!,
                Url = src.Listing.Thumbnail!.Url
            }))
            .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.Listing.CreatorId.ToString()))
            .ForMember(dest => dest.EngineModel, opt => opt.MapFrom(src => src.Listing.EngineModel))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Listing.Mileage))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Listing.Title))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Listing.Price))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Listing.Year));
    }
}