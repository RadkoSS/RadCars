﻿namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Griesoft.AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Authorization;

using Common.Exceptions;
using ViewModels.Listing;
using ViewModels.CarImage;
using ViewModels.Thumbnail;
using Services.Data.Contracts;
using Infrastructure.Extensions;

using static Common.NotificationTypeConstants;
using static Common.ExceptionsAndNotificationsMessages;
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
    [ValidateRecaptcha(Action = nameof(Create),
        ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
    public async Task<IActionResult> Create(ListingFormModel form)
    {
        try
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["MinYear"] = YearMinimumValue;
                this.ModelState.AddModelError("Images", InvalidDataSubmitted);
                this.TempData[ErrorMessage] = InvalidDataProvidedError;

                form = await this.ReloadForm(form);

                return View(form);
            }

            var listingId = await this.listingService.CreateListingAsync(form, this.User.GetId()!);

            this.TempData[SuccessMessage] = ListingCreatedSuccessfully;

            return RedirectToAction("ChooseThumbnail", "Listing", new { listingId });
        }
        catch (InvalidImagesException e)
        {
            this.ViewData["MinYear"] = YearMinimumValue;

            form = await this.ReloadForm(form);
            this.TempData[ErrorMessage] = InvalidDataProvidedError;
            this.ModelState.AddModelError(nameof(form.Images), e.Message);
            return View(form);
        }
        catch (InvalidDataException e)
        {
            this.ViewData["MinYear"] = YearMinimumValue;

            form = await this.ReloadForm(form);
            this.TempData[ErrorMessage] = InvalidDataProvidedError;
            this.ModelState.AddModelError(nameof(form.Images), e.Message);
            return View(form);
        }
        catch (Exception)
        {
            this.ViewData["MinYear"] = YearMinimumValue;

            form = await this.ReloadForm(form);
            this.TempData[ErrorMessage] = ErrorCreatingTheListing;
            return View(form);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string listingId)
    {
        try
        {
            var userId = this.User.GetId()!;

            var listingFormModel = await this.listingService.GetListingEditAsync(listingId, userId);

            return View(listingFormModel);
        }
        catch (InvalidOperationException)
        {
            return Unauthorized();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ListingEditFormModel form)
    {
        var userId = this.User.GetId()!;

        try
        {
            if (!ModelState.IsValid)
            {
                this.TempData[ErrorMessage] = InvalidDataProvidedError;

                form = await this.ReloadEditForm(form, userId);

                return View(form);
            }

            var listingId = await this.listingService.EditListingAsync(form, userId);

            this.TempData[SuccessMessage] = ListingWasUpdatedSuccessfully;

            return RedirectToAction("ChooseThumbnail", "Listing", new { listingId });
        }
        catch(InvalidImagesException e)
        {
            form = await this.ReloadEditForm(form, userId);
            this.TempData[ErrorMessage] = InvalidDataProvidedError;
            ModelState.AddModelError(nameof(form.Images), e.Message);
            return View(form);
        }
        catch (InvalidDataException e)
        {
            form = await this.ReloadEditForm(form, userId);
            this.TempData[ErrorMessage] = InvalidDataProvidedError;
            ModelState.AddModelError(nameof(form.Images), e.Message);
            return View(form);
        }
        catch (Exception)
        {
            form = await this.ReloadEditForm(form, userId);
            this.TempData[ErrorMessage] = ErrorCreatingTheListing;
            return View(form);
        }
    }

    [AllowAnonymous]
    public async Task<IActionResult> All([FromQuery] AllListingsQueryModel queryModel)
    {
        try
        {
            var serviceModel = await this.listingService.GetAllListingsAsync(queryModel);

            queryModel.Listings = serviceModel.Listings;
            queryModel.TotalListings = serviceModel.TotalListingsCount;
            queryModel.CarMakes = await this.listingService.GetCarMakesAsync();

            if (queryModel.CarModelId.HasValue && queryModel.CarMakeId.HasValue)
            {
                queryModel.CarModels = await this.carService.GetModelsByMakeIdAsync(queryModel.CarMakeId.Value);
            }

            queryModel.Cities = await this.listingService.GetCitiesAsync();
            queryModel.EngineTypes = await this.listingService.GetEngineTypesAsync();

            return View(queryModel);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

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
            return NotFound();
        }
    }

    public async Task<IActionResult> DeactivatedDetails(string listingId)
    {
        try
        {
            var userId = this.User.GetId()!;

            var deactivatedListingModel = await this.listingService.GetDeactivatedListingDetailsAsync(listingId, userId);

            return View("Details", deactivatedListingModel);
        }
        catch (Exception)
        {
            return NotFound();
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
            this.TempData[ErrorMessage] = AnErrorOccurred;

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
            this.TempData[ErrorMessage] = AnErrorOccurred;

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

            this.TempData[WarningMessage] = ListingRemovedFromFavorites;

            return RedirectToAction("Favorites", "Listing");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "Listing");
        }
    }

    [HttpGet]
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

            this.TempData[WarningMessage] = ListingDeactivated;

            return RedirectToAction("MineDeactivated", "Listing");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

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

            this.TempData[SuccessMessage] = ListingReDeactivated;

            return RedirectToAction("Mine", "Listing");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

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

            this.TempData[SuccessMessage] = ListingPermanentlyDeleted;

            return RedirectToAction("MineDeactivated", "Listing");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("MineDeactivated", "Listing");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteImage([FromBody] ImageInputModel data)
    {
        try
        {
            await this.imageService.DeleteImageAsync(data.ListingId, data.ImageId);

            this.TempData[SuccessMessage] = ImageDeletedSuccessfully;

            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
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
            this.TempData[ErrorMessage] = AnErrorOccurred;

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

                this.TempData[ErrorMessage] = InvalidDataProvidedError;

                return View(viewModel);
            }

            await this.listingService.AddThumbnailToListingByIdAsync(form.Id, form.ThumbnailId, this.User.GetId()!);

            this.TempData[SuccessMessage] = ThumbnailSelectedSuccessfully;

            return RedirectToAction("All", "Listing");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

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

    private async Task<ListingEditFormModel> ReloadEditForm(ListingEditFormModel editForm, string userId)
    {
        editForm = (await this.ReloadForm(editForm) as ListingEditFormModel)!;

        editForm.UploadedImages = await this.listingService.GetUploadedImagesForListingByIdAsync(editForm.Id, userId);

        return editForm;
    }
}