namespace RadCars.Data.Configurations.Listing;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class ListingFeatureEntityConfiguration : IEntityTypeConfiguration<ListingFeature>
{
    public void Configure(EntityTypeBuilder<ListingFeature> builder)
    {
        builder
            .HasKey(lf => new { lf.ListingId, lf.FeatureId });

        builder
            .HasOne(lf => lf.Listing)
            .WithMany(l => l.ListingFeatures)
            .HasForeignKey(lf => lf.ListingId);

        builder
            .HasOne(lf => lf.Feature)
            .WithMany(f => f.ListingFeatures)
            .HasForeignKey(lf => lf.FeatureId);
    }
}