namespace RadCars.Data.Configurations.Listing;

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
            .HasForeignKey(l => l.CreatorId);

        builder.HasOne(l => l.CarMake)
            .WithMany()
            .HasForeignKey(l => l.CarMakeId);

        builder.HasOne(l => l.CarModel)
            .WithMany()
            .HasForeignKey(l => l.CarModelId);

        builder
            .HasOne(l => l.EngineType)
            .WithMany()
            .HasForeignKey(l => l.EngineTypeId);

        builder.HasOne(l => l.Thumbnail)
            .WithMany()
            .HasForeignKey(l => l.ThumbnailId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(l => l.ListingFeatures)
            .WithOne()
            .HasForeignKey(lf => lf.ListingId);

        builder
            .HasOne(l => l.City)
            .WithMany()
            .HasForeignKey(l => l.CityId);

        builder
            .Property(l => l.Price)
            .HasPrecision(10, 2);

        //When we start supporting other countries we will uncomment this code!
        //builder
        //    .HasOne(l => l.Country)
        //    .WithMany();
    }
}