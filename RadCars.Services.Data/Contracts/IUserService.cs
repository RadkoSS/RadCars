namespace RadCars.Services.Data.Contracts;

public interface IUserService
{
    Task<string?> GetUserFullNameByUserNameAsync(string userName);

    Task<string> GetUserPhoneNumberByIdAsync(string userId);
}