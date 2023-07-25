namespace RadCars.Data.Models.Entities;

using User;
using Common.Contracts;

public class UserAuctionBid : IDeletableEntity
{
    public Guid BidderId { get; set; }

    public virtual ApplicationUser Bidder { get; set; } = null!;

    public Guid AuctionId { get; set; }

    public virtual Auction Auction { get; set; } = null!;

    public decimal Amount { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}