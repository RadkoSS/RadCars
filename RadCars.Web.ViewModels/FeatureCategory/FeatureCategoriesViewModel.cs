namespace RadCars.Web.ViewModels.FeatureCategory;

using Feature;

public class FeatureCategoriesViewModel
{
    public FeatureCategoriesViewModel()
    {
        this.Features = new HashSet<FeatureViewModel>();
    }

    public ushort Id { get; set; }
    
    public string Name { get; set; } = null!;

    public IEnumerable<FeatureViewModel> Features { get; set; }
}