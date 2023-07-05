namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

using static Seeding.CarEngineTypesSeeder;

internal class EngineTypeEntityConfiguration : IEntityTypeConfiguration<EngineTypes>
{
    public void Configure(EntityTypeBuilder<EngineTypes> builder)
    {
        builder.HasData(SeedEngineTypes());
    }
}