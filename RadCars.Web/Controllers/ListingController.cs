namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ViewModels.Listing;
using Services.Data.Contracts;
using Infrastructure.Extensions;

using static Common.EntityValidationConstants.ListingConstants;

public class ListingController : BaseController
{
    private readonly IListingService listingService;
    private readonly IImageService imageService;

    public ListingController(IListingService listingService, IImageService imageService)
    {
        this.listingService = listingService;
        this.imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var formModel = await this.listingService.GetListingCreateAsync();

        ViewData["MinYear"] = YearMinimumValue;

        return View(formModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ListingFormModel form)
    {
        try
        {
            await this.listingService.CreateListingAsync(form, this.User.GetId()!);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            return View();
        }
    }

    [AllowAnonymous]
    public async Task<IActionResult> All()
    {
        var listings = await this.listingService.GetAllListingsAsync();

        return View(listings);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(string listingId)
    {
        try
        {
            var listing = await this.listingService.GetListingDetailsAsync(listingId);

            return View(listing);
        }
        catch (Exception)
        {
            return RedirectToAction("All", "Listing");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeletePicture(string id)
    {
        //ToDo: Find a way to also get the imageId from the formData!

        try
        {
            await this.imageService.DeleteImageAsync(id, "");

            return RedirectToAction("All", "Listing");
        }
        catch
        {
            return RedirectToAction("Index", "Home");
        }
    }
}