namespace RadCars.Web.ViewModels.Common;

public abstract class BaseIndexViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string ThumbnailUrl { get; set; } = null!;
}