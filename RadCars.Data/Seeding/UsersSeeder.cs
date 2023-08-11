namespace RadCars.Data.Seeding;

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Contracts;
using Models.User;

using static RadCars.Common.GeneralApplicationConstants;

internal class UsersSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await SeedAdminAndNormalUserAsync(userManager);
    }

    private static async Task SeedAdminAndNormalUserAsync(UserManager<ApplicationUser> userManager)
    {
        if (await userManager.FindByEmailAsync(DevelopmentAdminEmail) != null)
        {
            return;
        }

        ApplicationUser adminUser = new ApplicationUser
        {
            FirstName = "Силен, Здрав",
            LastName = "Як, Стабилен"
        };

        await userManager.SetUserNameAsync(adminUser, DevelopmentAdminUserName);
        await userManager.SetEmailAsync(adminUser, DevelopmentAdminEmail);
        await userManager.SetPhoneNumberAsync(adminUser, DevelopmentAdminPhoneNumber);
        var token = await userManager.GenerateEmailConfirmationTokenAsync(adminUser);
        await userManager.ConfirmEmailAsync(adminUser, token);

        var result = await userManager.CreateAsync(adminUser, DevelopmentAdminPassword);

        if (result.Succeeded == false)
        {
            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }

        result = await userManager.AddToRoleAsync(adminUser, AdminRoleName);

        if (result.Succeeded == false)
        {
            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }

        ApplicationUser normalUser = new ApplicationUser
        {
            FirstName = "Обикновен",
            LastName = "потребител"
        };

        await userManager.SetUserNameAsync(normalUser, DevelopmentUserUserName);
        await userManager.SetEmailAsync(normalUser, DevelopmentUserEmail);
        await userManager.SetPhoneNumberAsync(normalUser, DevelopmentUserPhoneNumber);
        token = await userManager.GenerateEmailConfirmationTokenAsync(normalUser);
        await userManager.ConfirmEmailAsync(normalUser, token);

        result = await userManager.CreateAsync(normalUser, DevelopmentUserPassword);

        if (result.Succeeded == false)
        {
            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }
    }
}