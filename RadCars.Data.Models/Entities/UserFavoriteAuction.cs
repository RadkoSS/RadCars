namespace RadCars.Data.Models.Entities;

using Common.Contracts;
using User;

public class UserFavoriteAuction : IDeletableEntity
{
    public Guid UserId { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;

    public Guid AuctionId { get; set; }

    public virtual Auction Auction { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}