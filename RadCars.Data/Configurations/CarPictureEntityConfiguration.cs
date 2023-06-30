namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class CarPictureEntityConfiguration : IEntityTypeConfiguration<CarPicture>
{
    public void Configure(EntityTypeBuilder<CarPicture> builder)
    {
        builder
            .HasOne(cp => cp.Listing)
            .WithMany(l => l.Pictures)
            .HasForeignKey(cp => cp.ListingId);
    }
}