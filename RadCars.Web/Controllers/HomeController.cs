namespace RadCars.Web.Controllers;

using System.Diagnostics;

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
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}