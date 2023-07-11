namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;
using static Seeding.MakesSeeder;

internal class CarMakeEntityConfiguration : IEntityTypeConfiguration<CarMake>
{
    public void Configure(EntityTypeBuilder<CarMake> builder)
    {
        builder
            .HasMany(m => m.Models)
            .WithOne(m => m.CarMake);

        builder.HasData(SeedMakes());
    }
}