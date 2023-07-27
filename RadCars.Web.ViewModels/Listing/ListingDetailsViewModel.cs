namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using CarImage;
using FeatureCategory;
using Data.Models.Entities;

public class ListingDetailsViewModel : AllListingsViewModel
{
    public ListingDetailsViewModel()
    {
        this.Images = new HashSet<ImageViewModel>();
        this.ListingFeatures = new HashSet<FeatureCategoriesViewModel>();
    }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    public string CreatorUserName { get; set; } = null!;

    public string CreatorFullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string VinNumber { get; set; } = null!;

    public int FavoritesCount { get; set; }

    [Display(Name = "Екстри:")]
    public ICollection<FeatureCategoriesViewModel> ListingFeatures { get; set; }

    public ICollection<ImageViewModel> Images { get; set; }

    public override void CreateMappings(IProfileExpression configuration)
    {
        configuration
            .CreateMap<Listing, ListingDetailsViewModel>()
            .ForMember(destination => destination.ListingFeatures, options => options.Ignore());
    }
}