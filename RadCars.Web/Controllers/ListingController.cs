namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ViewModels.Listing;
using ViewModels.Thumbnail;
using Services.Data.Contracts;
using Infrastructure.Extensions;

using static Common.ExceptionsErrorMessages;
using static Common.EntityValidationConstants.ListingConstants;
using RadCars.Data.Models.Entities;

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
        try
        {
            var formModel = await this.listingService.GetListingCreateAsync();

            this.ViewData["MinYear"] = YearMinimumValue;

            return View(formModel);
        }
        catch (Exception)
        {
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ListingFormModel form)
    {
        try
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["MinYear"] = YearMinimumValue;
                this.ModelState.AddModelError("Images", InvalidDataSubmitted);

                form = await this.ReloadForm(form);

                return View(form);
            }

            var listingId = await this.listingService.CreateListingAsync(form, this.User.GetId()!);

            return RedirectToAction("ChooseThumbnail", "Listing", new { listingId });
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
        try
        {
            var listings = await this.listingService.GetAllListingsAsync();

            return View(listings);
        }
        catch (Exception)
        {
            return RedirectToAction("Index", "Home");
        }
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(string listingId)
    {
        try
        {
            var listingViewModel = await this.listingService.GetListingDetailsAsync(listingId);

            return View(listingViewModel);
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

    [HttpGet]
    public async Task<IActionResult> ChooseThumbnail(string listingId)
    {
        try
        {
            var viewModel = await this.listingService.GetChooseThumbnailAsync(listingId, this.User.GetId()!);

            return View(viewModel);
        }
        catch (Exception)
        {
            return RedirectToAction("Details", "Listing", new { listingId });
        }
    }

    [HttpPost]
    public async Task<IActionResult> ChooseThumbnail(ChooseThumbnailFormModel form)
    {
        try
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = await this.listingService.GetChooseThumbnailAsync(form.ListingId, this.User.GetId()!);

                return View(viewModel);
            }

            await this.listingService.AddThumbnailToListingByIdAsync(form.ListingId, form.SelectedImageId, this.User.GetId()!);

            return RedirectToAction("All", "Listing");
        }
        catch (Exception)
        {
            return RedirectToAction("Details", "Listing", new { form.ListingId });
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