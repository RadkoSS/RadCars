namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class FeatureEntityConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder
            .HasOne(f => f.Category)
            .WithMany(c => c.Features)
            .HasForeignKey(f => f.CategoryId);
    }
}