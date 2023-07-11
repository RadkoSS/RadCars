namespace RadCars.Web.ViewModels.Listing;

using CarImage;
using AutoMapper;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class AllListingViewModel : IMapFrom<Listing>, IHaveCustomMappings
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public int Year { get; set; }

    public string CreatorUserName { get; set; } = null!;

    public ImageViewModel Thumbnail { get; set; } = null!;

    //ToDo: add all properties needed! 
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<Listing, AllListingViewModel>().ForMember(source => source.CreatorUserName, destination => destination.MapFrom(l => l.Creator.UserName));
    }
}