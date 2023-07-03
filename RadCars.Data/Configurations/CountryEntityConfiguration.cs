namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

using static Seeding.CountriesSeeder;

internal class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        //There might be cities that are in two different countries.
        builder
            .HasMany(c => c.Cities)
            .WithOne(c => c.Country)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(SeedCountries());
    }
}