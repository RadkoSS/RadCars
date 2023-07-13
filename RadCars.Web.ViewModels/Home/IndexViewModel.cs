namespace RadCars.Web.ViewModels.Home;

using Data.Models.Entities;
using Services.Mapping.Contracts;

public class IndexViewModel : IMapFrom<Listing>
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string ThumbnailUrl { get; set; } = null!;

    public decimal Price { get; set; }
}
