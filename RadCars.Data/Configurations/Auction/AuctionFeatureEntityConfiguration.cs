namespace RadCars.Data.Configurations.Auction;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class AuctionFeatureEntityConfiguration : IEntityTypeConfiguration<AuctionFeature>
{
    public void Configure(EntityTypeBuilder<AuctionFeature> builder)
    {
        builder
            .HasKey(lf => new { lf.AuctionId, lf.FeatureId });

        builder
            .HasOne(lf => lf.Auction)
            .WithMany(l => l.AuctionFeatures)
            .HasForeignKey(lf => lf.AuctionId);

        builder
            .HasOne(lf => lf.Feature)
            .WithMany(f => f.AuctionFeatures)
            .HasForeignKey(lf => lf.FeatureId);
    }
}