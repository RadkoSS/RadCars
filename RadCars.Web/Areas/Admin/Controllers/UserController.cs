namespace RadCars.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using ViewModels.User;
using Services.Data.Contracts;

using static Common.NotificationTypeConstants;
using static Common.GeneralApplicationConstants;
using static Common.ExceptionsAndNotificationsMessages;

public class UserController : BaseAdminController
{
    private readonly IUserService userService;
    private readonly IMemoryCache memoryCache;

    public UserController(IUserService userService, IMemoryCache memoryCache)
    {
        this.userService = userService;
        this.memoryCache = memoryCache;
    }
    
    public async Task<IActionResult> All()
    {
        try
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
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Index", "Home");
        }
    }
}