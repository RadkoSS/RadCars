﻿namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

using static Seeding.CitiesSeeder;

internal class CityEntityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder
            .HasOne(c => c.Country)
            .WithMany(c => c.Cities)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.Latitude).HasPrecision(9, 6);
        builder.Property(c => c.Longitude).HasPrecision(9, 6);

        builder.HasData(SeedBulgarianCities());
    }
}