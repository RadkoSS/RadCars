namespace RadCars.Web.ViewModels.Listing;

using CarImage;
using AutoMapper;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class ListingDetailsViewModel : IMapFrom<Listing>, IHaveCustomMappings
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Year { get; set; }

    public string CreatorId { get; set; } = null!;

    public string CreatorUserName { get; set; } = null!;

    public ImageViewModel Thumbnail { get; set; } = null!;

    public ICollection<ImageViewModel> Images { get; set; } = null!;

    //ToDo: Add all the needed properties to display the details about each car listing!

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<Listing, ListingDetailsViewModel>().ForMember(source => source.CreatorUserName, destination => destination.MapFrom(l => l.Creator.UserName));

        configuration.CreateMap<Listing, ListingDetailsViewModel>().ForMember(source => source.CreatorId, destination => destination.MapFrom(l => l.CreatorId));
    }
}