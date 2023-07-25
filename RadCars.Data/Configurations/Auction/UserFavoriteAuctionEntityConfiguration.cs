namespace RadCars.Data.Configurations.Auction;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class UserFavoriteAuctionEntityConfiguration : IEntityTypeConfiguration<UserFavoriteAuction>
{
    public void Configure(EntityTypeBuilder<UserFavoriteAuction> builder)
    {
        builder
            .HasKey(f => new { f.UserId, f.AuctionId });

        builder
            .HasOne(f => f.User)
            .WithMany(u => u.FavoriteAuctions)
            .HasForeignKey(f => f.UserId);

        builder
            .HasOne(f => f.Auction)
            .WithMany(l => l.Favorites)
            .HasForeignKey(f => f.AuctionId);
    }
}