namespace RadCars.Web.ViewModels.Home;

using AutoMapper;

using Data.Models.Entities;
using Services.Mapping.Contracts;

public class IndexViewModel : IMapFrom<Listing>, IMapFrom<Auction>, IHaveCustomMappings
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string ThumbnailUrl { get; set; } = null!;

    public decimal Price { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration
            .CreateMap<Auction, IndexViewModel>()
            .ForMember(destination => destination.Price,
                opt => 
                    opt.MapFrom(source => source.CurrentPrice));
    }
}
