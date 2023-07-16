namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ViewModels.Home;
using RadCars.Services.Data.Contracts;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> logger;
    private readonly IListingService listingService;

    public HomeController(ILogger<HomeController> logger, IListingService listingService)
    {
        this.logger = logger;
        this.listingService = listingService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var viewModel = await this.listingService.GetMostRecentListingsAsync();

        return View(viewModel);
    }

    [AllowAnonymous]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 400 || statusCode == 404)
        {
            return this.View("Error404");
        }

        if (statusCode == 401)
        {
            return this.View("Error401");
        }

        return this.View();
    }
}