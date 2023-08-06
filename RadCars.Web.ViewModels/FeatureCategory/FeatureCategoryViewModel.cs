namespace RadCars.Web.ViewModels.FeatureCategory;

using Data.Models.Entities;
using Services.Mapping.Contracts;

public class FeatureCategoryViewModel : IMapFrom<Category>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int FeaturesCount { get; set; }
    
    public bool IsDeleted { get; set; }
}