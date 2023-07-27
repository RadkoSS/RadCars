namespace RadCars.Data.Configurations.Auction;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class AuctionCarImageEntityConfiguration : IEntityTypeConfiguration<AuctionCarImage>
{
    public void Configure(EntityTypeBuilder<AuctionCarImage> builder)
    {
        builder
            .HasOne(cp => cp.Auction)
            .WithMany(l => l.Images)
            .HasForeignKey(cp => cp.AuctionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}