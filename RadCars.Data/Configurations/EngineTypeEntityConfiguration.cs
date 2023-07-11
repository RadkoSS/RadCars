namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;
using static Seeding.CarEngineTypesSeeder;

internal class EngineTypeEntityConfiguration : IEntityTypeConfiguration<EngineType>
{
    public void Configure(EntityTypeBuilder<EngineType> builder)
    {
        builder.HasData(SeedEngineTypes());
    }
}