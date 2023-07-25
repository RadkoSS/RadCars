namespace RadCars.Data.Configurations.Listing;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class CarImageEntityConfiguration : IEntityTypeConfiguration<CarImage>
{
    public void Configure(EntityTypeBuilder<CarImage> builder)
    {
        builder
            .HasOne(cp => cp.Listing)
            .WithMany(l => l.Images)
            .HasForeignKey(cp => cp.ListingId);
    }
}