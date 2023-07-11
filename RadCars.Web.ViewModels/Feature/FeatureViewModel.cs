namespace RadCars.Web.ViewModels.Feature;

using Data.Models.Entities;
using Services.Mapping.Contracts;

public class FeatureViewModel : IMapFrom<Feature>
{
    public FeatureViewModel()
    {
        this.IsSelected = false;
    }

    public int Id { get; set; }

    public bool IsSelected { get; set; }

    public string Name { get; set; } = null!;
}