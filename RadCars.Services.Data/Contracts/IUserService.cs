namespace RadCars.Services.Data.Contracts;

using Web.ViewModels.User;

public interface IUserService
{
    Task<string?> GetUserFullNameByUserNameAsync(string userName);

    Task<string> GetUserPhoneNumberByIdAsync(string userId);

    Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
}