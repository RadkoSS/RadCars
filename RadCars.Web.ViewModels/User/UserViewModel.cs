namespace RadCars.Web.ViewModels.User;

using Data.Models.User;
using Services.Mapping.Contracts;

public class UserViewModel : IMapFrom<ApplicationUser>
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
}