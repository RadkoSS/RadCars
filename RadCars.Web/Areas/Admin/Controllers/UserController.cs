namespace RadCars.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using Web.ViewModels.User;
using Services.Data.Contracts;

using static Common.GeneralApplicationConstants;

public class UserController : BaseAdminController
{
    private readonly IUserService userService;
    private readonly IMemoryCache memoryCache;

    public UserController(IUserService userService, IMemoryCache memoryCache)
    {
        this.userService = userService;
        this.memoryCache = memoryCache;
    }
    
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client, NoStore = false)]
    public async Task<IActionResult> All()
    {
        IEnumerable<UserViewModel> users =
            this.memoryCache.Get<IEnumerable<UserViewModel>>(UsersCacheKey);

        if (users == null)
        {
            users = await this.userService.GetAllUsersAsync();

            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan
                    .FromMinutes(UsersCacheDurationMinutes));

            this.memoryCache.Set(UsersCacheKey, users, cacheOptions);
        }

        return View(users);
    }
}