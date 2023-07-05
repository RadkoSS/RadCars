namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

using static Seeding.CarEngineTypesSeeder;

internal class CarEngineTypeEntityConfiguration : IEntityTypeConfiguration<CarEngineType>
{
    public void Configure(EntityTypeBuilder<CarEngineType> builder)
    {
        builder.HasData(SeedEngineTypes());
    }
}