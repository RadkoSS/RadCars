namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ViewModels.Listing;
using ViewModels.Thumbnail;
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

    public async Task<IActionResult> Mine()
    {
        try
        {
            var userId = this.User.GetId()!;

            var listings = await this.listingService.GetAllListingsByUserIdAsync(userId);

            return View(listings);
        }
        catch (Exception)
        {
            return RedirectToAction("All", "Listing");
        }
    }

    public async Task<IActionResult> Favorites()
    {
        try
        {
            var userId = this.User.GetId()!;

            var favoriteListings = await this.listingService.GetFavoriteListingsByUserIdAsync(userId);

            return View(favoriteListings);
        }
        catch (Exception)
        {
            return RedirectToAction("All", "Listing");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UnFavorite(string listingId)
    {
        try
        {
            var userId = this.User.GetId()!;

            await this.listingService.UnFavoriteListingByIdAsync(listingId, userId);

            return RedirectToAction("Favorites", "Listing");
        }
        catch (Exception)
        {
            return RedirectToAction("All", "Listing");
        }
    }

    public async Task<IActionResult> MineDeactivated()
    {
        try
        {
            var userId = this.User.GetId()!;

            var deactivatedListings = await this.listingService.GetAllDeactivatedListingsByUserIdAsync(userId);

            return View(deactivatedListings);
        }
        catch (Exception)
        {
            return RedirectToAction("Mine", "Listing");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Deactivate(string listingId)
    {
        try
        {
            var userId = this.User.GetId()!;

            await this.listingService.DeactivateListingByIdAsync(listingId, userId);

            return RedirectToAction("MineDeactivated", "Listing");
        }
        catch (Exception)
        {
            return RedirectToAction("All", "Listing");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Reactivate(string listingId)
    {
        try
        {
            var userId = this.User.GetId()!;

            await this.listingService.ReactivateListingByIdAsync(listingId, userId);

            return RedirectToAction("Mine", "Listing");
        }
        catch (Exception)
        {
            return RedirectToAction("All", "Listing");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string listingId)
    {
        try
        {
            var userId = this.User.GetId()!;

            await this.listingService.HardDeleteListingByIdAsync(listingId, userId);

            return RedirectToAction("MineDeactivated", "Listing");
        }
        catch (Exception)
        {
            return RedirectToAction("Mine", "Listing");
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
                var viewModel = await this.listingService.GetChooseThumbnailAsync(form.Id, this.User.GetId()!);

                return View(viewModel);
            }

            await this.listingService.AddThumbnailToListingByIdAsync(form.Id, form.ThumbnailId, this.User.GetId()!);

            return RedirectToAction("All", "Listing");
        }
        catch (Exception)
        {
            return RedirectToAction("Details", "Listing", new { ListingId = form.Id });
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