namespace RadCars.Services.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Mapping;
using Contracts;
using Web.ViewModels.User;
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

    public async Task<string> GetUserPhoneNumberByIdAsync(string userId)
    {
        var user = await this.userManager.FindByIdAsync(userId);
        return user.PhoneNumber!;
    }

    public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
    {
        var users = await this.userManager
            .Users
            .To<UserViewModel>()
            .ToArrayAsync();

        return users;
    }
}