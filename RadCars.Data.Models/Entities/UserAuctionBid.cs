namespace RadCars.Data.Models.Entities;

using User;
using Common.Models;

public class UserAuctionBid : BaseDeletableModel<Guid>
{
    public UserAuctionBid()
    {
        this.Id = Guid.NewGuid();
    }

    public Guid BidderId { get; set; }

    public virtual ApplicationUser Bidder { get; set; } = null!;

    public Guid AuctionId { get; set; }

    public virtual Auction Auction { get; set; } = null!;

    public decimal Amount { get; set; }
}