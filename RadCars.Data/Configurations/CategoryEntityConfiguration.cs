﻿namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;
using static Seeding.FeatureCategoriesSeeder;

internal class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasMany(fc => fc.Features)
            .WithOne(f => f.Category)
            .HasForeignKey(fc => fc.CategoryId);

        builder.HasData(SeedFeatureCategories());
    }
}