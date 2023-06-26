namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;
using static Seeding.FeatureCategoriesSeeder;

internal class FeatureCategoryEntityConfiguration : IEntityTypeConfiguration<FeatureCategory>
{
    public void Configure(EntityTypeBuilder<FeatureCategory> builder)
    {
        builder.HasData(SeedFeatureCategories());
    }
}