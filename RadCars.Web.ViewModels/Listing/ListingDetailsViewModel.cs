namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using CarImage;
using FeatureCategory;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class ListingDetailsViewModel : AllListingViewModel, IHaveCustomMappings
{
    public ListingDetailsViewModel()
    {
        this.Images = new HashSet<ImageViewModel>();
        this.ListingFeatures = new HashSet<FeatureCategoriesViewModel>();
    }

    public string CreatedOn { get; set; } = null!;

    public string CreatorUserName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int FavoritesCount { get; set; }

    [Display(Name = "Екстри:")]
    public ICollection<FeatureCategoriesViewModel> ListingFeatures { get; set; }

    public ICollection<ImageViewModel> Images { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration
            .CreateMap<Listing, ListingDetailsViewModel>()
            .ForMember(source => source.CreatedOn, destination => destination.MapFrom(l => l.CreatedOn.ToLocalTime().ToString("U")));

        configuration
            .CreateMap<Listing, ListingDetailsViewModel>()
            .ForMember(source => source.ListingFeatures, options => options.Ignore());
    }
}