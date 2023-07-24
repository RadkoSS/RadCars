namespace RadCars.Services.Data;

using Microsoft.AspNetCore.Identity;

using Contracts;
using RadCars.Data.Models.User;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<string?> GetUserFullNameByUserNameAsync(string userName)
    {
        if (!string.IsNullOrWhiteSpace(userName))
        {
            var user = await this.userManager.FindByNameAsync(userName);

            return $"{user.FirstName} {user.LastName}";
        }
        
        return null;
    }
}