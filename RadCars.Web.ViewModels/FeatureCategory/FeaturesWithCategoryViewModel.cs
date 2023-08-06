namespace RadCars.Web.ViewModels.FeatureCategory;

using Feature;

public class FeaturesWithCategoryViewModel : FeatureCategoryViewModel
{
    public FeaturesWithCategoryViewModel()
    {
        this.Features = new HashSet<FeatureViewModel>();
    }

    public IEnumerable<FeatureViewModel> Features { get; set; }
}