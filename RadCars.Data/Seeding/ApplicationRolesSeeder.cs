namespace RadCars.Data.Seeding;

using Models.User;

using static RadCars.Common.GeneralApplicationConstants;

internal static class ApplicationRolesSeeder
{
    internal static ApplicationRole[] SeedApplicationRoles()
    {
        var roles = new ApplicationRole[]
        {
            new ApplicationRole
            {
                Id = Guid.Parse("0531D236-C024-4699-A191-BEE526045574"),
                ConcurrencyStamp = "4a46ae71-35f9-4e7e-a6be-273a7624763b",
                CreatedOn = DateTime.Parse("03.08.2023"),
                Name = AdminRoleName,
                NormalizedName = AdminRoleNormalizedName
            }
        };

        return roles;
    }
}