namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ViewModels.Listing;
using Services.Data.Contracts;
using Infrastructure.Extensions;

using static Common.ExceptionsErrorMessages;
using static Common.EntityValidationConstants.ListingConstants;

public class ListingController : BaseController
{
    private readonly ICarService carService;
    private readonly IImageService imageService;
    private readonly IListingService listingService;

    public ListingController(IListingService listingService, IImageService imageService, ICarService carService)
    {
        this.carService = carService;
        this.imageService = imageService;
        this.listingService = listingService;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var formModel = await this.listingService.GetListingCreateAsync();

        this.ViewData["MinYear"] = YearMinimumValue;

        return View(formModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ListingFormModel form)
    {
        if (!this.ModelState.IsValid)
        {
            this.ViewData["MinYear"] = YearMinimumValue;
            this.ModelState.AddModelError("Images", InvalidDataSubmitted);

            form = await this.ReloadForm(form);

            return View(form);
        }

        try
        {
            await this.listingService.CreateListingAsync(form, this.User.GetId()!);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            this.ViewData["MinYear"] = YearMinimumValue;
            this.ModelState.AddModelError("Images", ErrorCreatingTheListing);

            form = await this.ReloadForm(form);

            return View(form);
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

    private async Task<ListingFormModel> ReloadForm(ListingFormModel form)
    {
        form.Cities = await this.listingService.GetCitiesAsync();
        form.CarMakes = await this.listingService.GetCarMakesAsync();
        form.EngineTypes = await this.listingService.GetEngineTypesAsync();
        form.CarModels = await this.carService.GetModelsByMakeIdAsync(form.CarMakeId);
        form.FeatureCategories = await this.listingService.GetFeatureCategoriesAsync();

        foreach (var category in form.FeatureCategories)
        {
            foreach (var feature in category.Features)
            {
                if (form.SelectedFeatures.Contains(feature.Id))
                {
                    feature.IsSelected = true;
                }
            }
        }

        return form;
    }
}