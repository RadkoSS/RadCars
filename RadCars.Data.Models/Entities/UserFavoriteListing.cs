namespace RadCars.Data.Models.Entities;

using User;

public class UserFavoriteListing
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;

    public Guid ListingId { get; set; }
    public virtual Listing Listing { get; set; } = null!;
}