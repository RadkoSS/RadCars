namespace RadCars.Web.ViewModels.Feature;

public class FeatureViewModel
{
    public FeatureViewModel()
    {
        this.IsSelected = false;
    }

    public int Id { get; set; }

    public bool IsSelected { get; set; }

    public string Name { get; set; } = null!;
}