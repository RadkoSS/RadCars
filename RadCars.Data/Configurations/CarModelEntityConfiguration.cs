namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class CarModelEntityConfiguration : IEntityTypeConfiguration<CarModel>
{
    public void Configure(EntityTypeBuilder<CarModel> builder)
    {
        builder
            .HasOne(m => m.CarMake)
            .WithMany(m => m.Models);
    }
}