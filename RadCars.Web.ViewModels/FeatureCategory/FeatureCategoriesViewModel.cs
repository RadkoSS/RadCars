namespace RadCars.Web.ViewModels.FeatureCategory;

using Feature;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class FeatureCategoriesViewModel : IMapFrom<Category>
{
    public FeatureCategoriesViewModel()
    {
        this.Features = new HashSet<FeatureViewModel>();
    }

    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public IEnumerable<FeatureViewModel> Features { get; set; }
}