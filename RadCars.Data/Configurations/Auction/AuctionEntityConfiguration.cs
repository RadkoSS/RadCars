namespace RadCars.Data.Configurations.Auction;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class AuctionEntityConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        builder
            .HasOne(a => a.Creator)
            .WithMany(u => u.Auctions)
            .HasForeignKey(a => a.CreatorId);

        builder.HasOne(a => a.CarMake)
            .WithMany()
            .HasForeignKey(a => a.CarMakeId);

        builder.HasOne(a => a.CarModel)
            .WithMany()
            .HasForeignKey(a => a.CarModelId);

        builder
            .HasOne(a => a.EngineType)
            .WithMany()
            .HasForeignKey(l => l.EngineTypeId);

        builder.HasOne(a => a.Thumbnail)
            .WithMany()
            .HasForeignKey(a => a.ThumbnailId);

        builder.HasMany(a => a.AuctionFeatures)
            .WithOne()
            .HasForeignKey(lf => lf.AuctionId);

        builder
            .HasOne(a => a.City)
            .WithMany()
            .HasForeignKey(a => a.CityId);

        builder
            .Property(a => a.StartingPrice)
            .HasPrecision(10, 2);

        builder
            .Property(a => a.CurrentPrice)
            .HasPrecision(10, 2);

        builder
            .Property(a => a.BlitzPrice)
            .HasPrecision(10, 2)
            .IsRequired(false);

        //When we start supporting other countries we will uncomment this code!
        //builder
        //    .HasOne(l => l.Country)
        //    .WithMany();
    }
}