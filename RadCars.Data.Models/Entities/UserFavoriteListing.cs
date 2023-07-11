namespace RadCars.Data.Models.Entities;

using Common.Contracts;
using User;

public class UserFavoriteListing : IDeletableEntity
{
    public Guid UserId { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;

    public Guid ListingId { get; set; }

    public virtual Listing Listing { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}