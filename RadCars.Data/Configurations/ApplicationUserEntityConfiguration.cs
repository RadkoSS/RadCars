namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.User;

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .HasMany(u => u.Listings)
            .WithOne(l => l.Creator)
            .HasForeignKey(l => l.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}