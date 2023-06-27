namespace RadCars.Data.Models.Entities;

using User;

public class UserFavoriteListing
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public Guid ListingId { get; set; }
    public Listing Listing { get; set; } = null!;
}