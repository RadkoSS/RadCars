namespace RadCars.Data.Seeding;

using Microsoft.AspNetCore.Identity;

using Models.User;

using static RadCars.Common.GeneralApplicationConstants;

internal static class ApplicationUsersSeeder
{
    internal static ApplicationUser[] SeedApplicationUsers()
    {
        var users = new HashSet<ApplicationUser>();
        var passwordHasher = new PasswordHasher<ApplicationUser>();

        ApplicationUser user;

        user = new ApplicationUser
        {
            Id = Guid.Parse("5882793C-B91B-4059-AA67-1854EC0E50D4"),
            CreatedOn = DateTime.Parse("03.08.2023"),
            Email = DevelopmentAdminEmail,
            NormalizedEmail = DevelopmentAdminNormalizedEmail,
            UserName = DevelopmentAdminUserName,
            NormalizedUserName = DevelopmentAdminNormalizedUserName,
            PhoneNumber = "+359896969699",
            FirstName = "Силен, Здрав",
            LastName = "Як, Стабилен"
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "admin69");

        users.Add(user);

        user = new ApplicationUser
        {
            Id = Guid.Parse("4884E5A3-AE40-4BB7-9C4C-ECCEE8A89ECC"),
            CreatedOn = DateTime.Parse("04.08.2023"),
            Email = DevelopmentUserEmail,
            NormalizedEmail = DevelopmentUserNormalizedEmail,
            UserName = DevelopmentUserUserName,
            NormalizedUserName = DevelopmentUserNormalizedUserName,
            PhoneNumber = "+359896969999",
            FirstName = "Обикновен",
            LastName = "потребител"
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "user69");

        users.Add(user);

        return users.ToArray();
    }
}