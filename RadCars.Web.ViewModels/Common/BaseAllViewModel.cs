namespace RadCars.Web.ViewModels.Common;

using City;
using CarImage;

public abstract class BaseAllViewModel
{
    public string Id { get; set; } = null!;

    public string CreatorId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int Mileage { get; set; }

    public string EngineModel { get; set; } = null!;

    public string CarMakeName { get; set; } = null!;

    public string CarModelName { get; set; } = null!;

    public CityViewModel City { get; set; } = null!;

    public int Year { get; set; }

    public ImageViewModel Thumbnail { get; set; } = null!;
}