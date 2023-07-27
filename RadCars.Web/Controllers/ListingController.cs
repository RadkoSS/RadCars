namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Griesoft.AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Authorization;

using Common.Exceptions;
using ViewModels.Listing;
using ViewModels.Thumbnail;
using Services.Data.Contracts;
using Infrastructure.Extensions;

using static Common.NotificationTypeConstants;
using static Common.ExceptionsAndNotificationsMessages;
using static Common.EntityValidationConstants.ListingConstants;

public class ListingController : BaseController
{
    private readonly ICarService carService;
    private readonly IUserService userService;
    private readonly IListingService listingService;

    public ListingController(IListingService listingService, ICarService carService, IUserService userService)
    {
        this.carService = carService;
        this.userService = userService;
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

            var userId = this.User.GetId()!;

            if (string.IsNullOrWhiteSpace(form.PhoneNumber))
            {
                form.PhoneNumber = await this.userService.GetUserPhoneNumberByIdAsync(userId);
            }

            var listingId = await this.listingService.CreateListingAsync(form, userId);

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
            var userIsAdmin = this.User.IsAdmin();

            var listingFormModel = await this.listingService.GetListingEditAsync(listingId, userId, userIsAdmin);

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
            if (!this.ModelState.IsValid)
            {
                this.TempData[ErrorMessage] = InvalidDataProvidedError;

                form = await this.ReloadEditForm(form, userId);

                return View(form);
            }

            if (string.IsNullOrWhiteSpace(form.PhoneNumber))
            {
                form.PhoneNumber = await this.userService.GetUserPhoneNumberByIdAsync(userId);
            }

            var userIsAdmin = this.User.IsAdmin();

            var listingId = await this.listingService.EditListingAsync(form, userId, userIsAdmin);

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
            queryModel.CarMakes = await this.carService.GetCarMakesAsync();

            if (queryModel.CarModelId.HasValue && queryModel.CarMakeId.HasValue)
            {
                queryModel.CarModels = await this.carService.GetModelsByMakeIdAsync(queryModel.CarMakeId.Value);
            }

            queryModel.Cities = await this.carService.GetBulgarianCitiesAsync();
            queryModel.EngineTypes = await this.carService.GetEngineTypesAsync();

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
            var userIsAdmin = this.User.IsAdmin();

            var deactivatedListingModel = await this.listingService.GetDeactivatedListingDetailsAsync(listingId, userId, userIsAdmin);

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
            var userIsAdmin = this.User.IsAdmin();

            await this.listingService.DeactivateListingByIdAsync(listingId, userId, userIsAdmin);

            this.TempData[WarningMessage] = ListingDeactivated;

            if (userIsAdmin)
            {
                return RedirectToAction("DeactivatedDetails", new { listingId });
            }

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
            var userIsAdmin = this.User.IsAdmin();

            await this.listingService.ReactivateListingByIdAsync(listingId, userId, userIsAdmin);

            this.TempData[SuccessMessage] = ListingReDeactivated;

            if (userIsAdmin)
            {
                return RedirectToAction("Details", new { listingId });
            }

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
            var userIsAdmin = this.User.IsAdmin();

            await this.listingService.HardDeleteListingByIdAsync(listingId, userId, userIsAdmin);

            this.TempData[SuccessMessage] = ListingPermanentlyDeleted;

            if (userIsAdmin)
            {
                return RedirectToAction("All", "Listing");
            }

            return RedirectToAction("MineDeactivated", "Listing");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("MineDeactivated", "Listing");
        }
    }

    [HttpGet]
    public async Task<IActionResult> ChooseThumbnail(string listingId)
    {
        try
        {
            var userId = this.User.GetId()!;
            var userIsAdmin = this.User.IsAdmin();

            var viewModel = await this.listingService.GetChooseThumbnailAsync(listingId, userId, userIsAdmin);

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
            var userId = this.User.GetId()!;
            var userIsAdmin = this.User.IsAdmin();

            if (!this.ModelState.IsValid)
            {
                var viewModel = await this.listingService.GetChooseThumbnailAsync(form.Id, userId, userIsAdmin);

                this.TempData[ErrorMessage] = InvalidDataProvidedError;

                return View(viewModel);
            }

            await this.listingService.AddThumbnailToListingByIdAsync(form.Id, form.ThumbnailId, userId, userIsAdmin);

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
        form.Cities = await this.carService.GetBulgarianCitiesAsync();
        form.CarMakes = await this.carService.GetCarMakesAsync();
        form.EngineTypes = await this.carService.GetEngineTypesAsync();
        form.CarModels = await this.carService.GetModelsByMakeIdAsync(form.CarMakeId);
        form.FeatureCategories = await this.carService.GetFeatureCategoriesAsync();

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

        var userIsAdmin = this.User.IsAdmin();

        editForm.UploadedImages = await this.listingService.GetUploadedImagesForListingByIdAsync(editForm.Id, userId, userIsAdmin);

        return editForm;
    }
}