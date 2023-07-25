namespace RadCars.Web.ViewModels.Listing;

using AutoMapper;

using Common;
using FeatureCategory;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class ListingFormModel : BaseCreateFormModel, IMapTo<Listing>, IMapFrom<Listing>, IHaveCustomMappings
{
    public ListingFormModel()
    {
        this.FeatureCategories = new HashSet<FeatureCategoriesViewModel>();
    }

    public IEnumerable<FeatureCategoriesViewModel> FeatureCategories { get; set; }

    public virtual void CreateMappings(IProfileExpression configuration)
    {
        configuration
            .CreateMap<ListingFormModel, Listing>()
            .ForMember(destination => destination.ListingFeatures, options => options.Ignore())
            .ForMember(destination => destination.Images, options => options.Ignore());
    }
}