namespace RadCars.Data.Configurations.Auction;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class UserAuctionBidEntityConfiguration : IEntityTypeConfiguration<UserAuctionBid>
{
    public void Configure(EntityTypeBuilder<UserAuctionBid> builder)
    {
        builder
            .HasKey(f => new { f.BidderId, f.AuctionId });

        builder
            .HasOne(f => f.Bidder)
            .WithMany(u => u.AuctionsBids)
            .HasForeignKey(f => f.BidderId);

        builder
            .HasOne(f => f.Auction)
            .WithMany(l => l.Bids)
            .HasForeignKey(f => f.AuctionId);

        builder
            .Property(a => a.Amount)
            .HasPrecision(10, 2);
    }
}