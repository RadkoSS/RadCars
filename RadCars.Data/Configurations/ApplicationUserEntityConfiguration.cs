namespace RadCars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.User;

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .HasMany(e => e.Claims)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(e => e.Logins)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(e => e.Roles)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(u => u.Listings)
            .WithOne(l => l.Creator)
            .HasForeignKey(l => l.CreatorId);
    }
}