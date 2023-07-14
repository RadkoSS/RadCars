namespace RadCars.Api.Models;

public class ListingFavoritesInputModel
{
    public string UserId { get; set; } = null!;

    public string ListingId { get; set; } = null!;
}