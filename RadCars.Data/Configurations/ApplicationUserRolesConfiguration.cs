namespace RadCars.Data.Configurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ApplicationUserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        //builder.HasData(new IdentityUserRole<Guid>
        //{
        //    RoleId = Guid.Parse("0531D236-C024-4699-A191-BEE526045574"),
        //    UserId = Guid.Parse("5882793C-B91B-4059-AA67-1854EC0E50D4")
        //});
    }
}