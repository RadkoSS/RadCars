namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class ListingEntityConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder
            .HasOne(l => l.Creator)
            .WithMany(c => c.Listings)
            .HasForeignKey(l => l.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.CarMake)
            .WithMany()
            .HasForeignKey(l => l.CarMakeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.CarModel)
            .WithMany()
            .HasForeignKey(l => l.CarModelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(l => l.ListingFeatures)
            .WithOne()
            .HasForeignKey(lf => lf.ListingId);
    }
}