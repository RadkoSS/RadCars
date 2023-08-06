namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Griesoft.AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

using ViewModels.City;
using Common.Exceptions;
using ViewModels.Auction;
using ViewModels.CarMake;
using ViewModels.Thumbnail;
using Services.Data.Contracts;
using Infrastructure.Extensions;
using BackgroundServices.Contracts;

using static Common.NotificationTypeConstants;
using static Common.GeneralApplicationConstants;
using static Common.ExceptionsAndNotificationsMessages;
using static Common.EntityValidationConstants.ListingConstants;

public class AuctionController : BaseController
{
    private readonly ICarService carService;
    private readonly IMemoryCache memoryCache;
    private readonly IUserService userService;
    private readonly IAuctionService auctionService;
    private readonly IAuctionBackgroundJobService auctionBackgroundJobService;

    public AuctionController(IAuctionService auctionService, IUserService userService, ICarService carService, IAuctionBackgroundJobService auctionBackgroundJobService, IMemoryCache memoryCache)
    {
        this.carService = carService;
        this.memoryCache = memoryCache;
        this.userService = userService;
        this.auctionService = auctionService;
        this.auctionBackgroundJobService = auctionBackgroundJobService;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            var formModel = await this.auctionService.GetAuctionCreateAsync();

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
    public async Task<IActionResult> Create(AuctionFormModel form)
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

            var auctionId = await this.auctionService.CreateAuctionAsync(form, userId);

            await this.auctionBackgroundJobService.ScheduleAuctionStart(auctionId);
            await this.auctionBackgroundJobService.ScheduleAuctionEnd(auctionId);

            this.TempData[SuccessMessage] = AuctionCreateSuccessfully;

            return RedirectToAction("ChooseThumbnail", "Auction", new { auctionId });
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
            this.TempData[ErrorMessage] = ErrorCreatingTheAuction;
            return View(form);
        }
    }

    [HttpGet]
    public async Task<IActionResult> ChooseThumbnail(string auctionId)
    {
        try
        {
            var userId = this.User.GetId()!;
            var userIsAdmin = this.User.IsAdmin();

            var viewModel = await this.auctionService.GetChooseThumbnailAsync(auctionId, userId, userIsAdmin);

            return View(viewModel);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Details", "Auction", new { auctionId });
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
                var viewModel = await this.auctionService.GetChooseThumbnailAsync(form.Id, userId, userIsAdmin);

                this.TempData[ErrorMessage] = InvalidDataProvidedError;

                return View(viewModel);
            }

            await this.auctionService.AddThumbnailToAuctionByIdAsync(form.Id, form.ThumbnailId, userId, userIsAdmin);

            this.TempData[SuccessMessage] = ThumbnailSelectedSuccessfully;

            return RedirectToAction("All", "Auction");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Details", "Auction", new { form.Id });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string auctionId)
    {
        try
        {
            var userId = this.User.GetId()!;
            var userIsAdmin = this.User.IsAdmin();

            var auctionEditFormModel = await this.auctionService.GetAuctionEditAsync(auctionId, userId, userIsAdmin);

            return View(auctionEditFormModel);
        }
        catch (InvalidOperationException)
        {
            this.TempData[ErrorMessage] = AuctionHasAlreadyStarted;
            return RedirectToAction("Details", "Auction", new { auctionId });
        }
        catch (InvalidDataException)
        {
            return Unauthorized();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AuctionEditFormModel form)
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

            var auctionId = await this.auctionService.EditAuctionAsync(form, userId, userIsAdmin);

            await this.auctionBackgroundJobService.RescheduleEditedAuctionStartAndEndAsync(auctionId);

            this.TempData[SuccessMessage] = AuctionWasUpdatedSuccessfully;

            return RedirectToAction("ChooseThumbnail", "Auction", new { auctionId });
        }
        catch (InvalidOperationException)
        {
            this.TempData[ErrorMessage] = CannotEditActiveAuctionWithBids;

            return RedirectToAction("Details", "Auction", new { form.Id });
        }
        catch (InvalidImagesException e)
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
            this.TempData[ErrorMessage] = ErrorEditingTheAuction;
            return View(form);
        }
    }

    [AllowAnonymous]
    public async Task<IActionResult> All([FromQuery] AllAuctionsQueryModel queryModel)
    {
        if (this.User.IsAdmin())
        {
            return RedirectToAction("AllActive", "Auction", new { Area = AdminAreaName });
        }

        try
        {
            var withDeletedAuctions = false;

            var serviceModel = await this.auctionService.GetAllAuctionsAsync(queryModel, withDeletedAuctions);

            queryModel.Auctions = serviceModel.Auctions;
            queryModel.TotalAuctions = serviceModel.TotalAuctionsCount;

            if (queryModel.CarModelId.HasValue && queryModel.CarMakeId.HasValue)
            {
                queryModel.CarModels = await this.carService.GetModelsByMakeIdAsync(queryModel.CarMakeId.Value);
            }

            queryModel.CarMakes = this.memoryCache.Get<IEnumerable<CarMakeViewModel>>(CarMakesCacheKey);

            if (queryModel.CarMakes == null || queryModel.CarMakes.Any() == false)
            {
                queryModel.CarMakes = await this.carService.GetCarMakesAsync();

                this.memoryCache.Set(CarMakesCacheKey, queryModel.CarMakes);
            }

            queryModel.Cities = this.memoryCache.Get<IEnumerable<CityViewModel>>(CitiesCacheKey);

            if (queryModel.Cities == null || queryModel.Cities.Any() == false)
            {
                queryModel.Cities = await this.carService.GetBulgarianCitiesAsync();

                this.memoryCache.Set(CitiesCacheKey, queryModel.Cities);
            }

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
    public async Task<IActionResult> Details(string auctionId)
    {
        try
        {
            var userId = this.User.GetId();
            var isAdmin = this.User.IsAdmin();

            var auctionDetailsViewModel = await this.auctionService.GetAuctionDetailsAsync(auctionId, userId, isAdmin);

            return View(auctionDetailsViewModel);
        }
        catch (Exception)
        {
            this.TempData[WarningMessage] = AuctionDoesNotExistError;
            return RedirectToAction("All", "Auction");
        }
    }

    public async Task<IActionResult> Mine()
    {
        try
        {
            var userId = this.User.GetId()!;

            var auctions = await this.auctionService.GetAllAuctionsByUserIdAsync(userId);

            return View(auctions);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "Auction");
        }
    }

    public async Task<IActionResult> MineExpired()
    {
        try
        {
            var userId = this.User.GetId()!;

            var auctions = await this.auctionService.GetAllExpiredAuctionsByUserIdAsync(userId);

            return View(auctions);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "Auction");
        }
    }

    public async Task<IActionResult> Favorites()
    {
        try
        {
            var userId = this.User.GetId()!;

            var favoriteAuctions = await this.auctionService.GetFavoriteAuctionsByUserIdAsync(userId);

            return View(favoriteAuctions);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "Auction");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UnFavorite(string auctionId)
    {
        try
        {
            var userId = this.User.GetId()!;

            await this.auctionService.UnFavoriteAuctionByIdAsync(auctionId, userId);

            this.TempData[WarningMessage] = AuctionRemovedFromFavorites;

            return RedirectToAction("Favorites", "Auction");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "Auction");
        }
    }

    [HttpGet]
    public async Task<IActionResult> MineDeactivated()
    {
        try
        {
            var userId = this.User.GetId()!;

            var deactivatedAuctions = await this.auctionService.GetAllDeactivatedAuctionsByUserIdAsync(userId);

            return View(deactivatedAuctions);
        }
        catch (Exception)
        {
            return RedirectToAction("Mine", "Auction");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Deactivate(string auctionId)
    {
        try
        {
            var userId = this.User.GetId()!;
            var userIsAdmin = this.User.IsAdmin();

            await this.auctionService.DeactivateAuctionByIdAsync(auctionId, userId, userIsAdmin);

            await this.auctionBackgroundJobService.CancelAuctionStartAndEnd(auctionId);

            this.TempData[WarningMessage] = AuctionDeactivated;

            if (userIsAdmin)
            {
                return RedirectToAction("AllDeactivated", "Auction", new { Area = AdminAreaName, auctionId });
            }

            return RedirectToAction("MineDeactivated", "Auction");
        }
        catch (InvalidOperationException)
        {
            this.TempData[ErrorMessage] = CannotDeactivateAuction;

            return RedirectToAction("Details", "Auction", new { auctionId });
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "Auction");
        }
    }

    [HttpPost]
    public IActionResult Reactivate(string auctionId)
        => RedirectToAction("Edit", "Auction", new { auctionId });


    private async Task<AuctionFormModel> ReloadForm(AuctionFormModel form)
    {
        form.Cities = this.memoryCache.Get<IEnumerable<CityViewModel>>(CitiesCacheKey);

        if (form.Cities == null || form.Cities.Any() == false)
        {
            form.Cities = await this.carService.GetBulgarianCitiesAsync();

            this.memoryCache.Set(CitiesCacheKey, form.Cities);
        }

        form.CarMakes = this.memoryCache.Get<IEnumerable<CarMakeViewModel>>(CarMakesCacheKey);

        if (form.CarMakes == null || form.CarMakes.Any() == false)
        {
            form.CarMakes = await this.carService.GetCarMakesAsync();

            this.memoryCache.Set(CarMakesCacheKey, form.CarMakes);
        }

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

    private async Task<AuctionEditFormModel> ReloadEditForm(AuctionEditFormModel editForm, string userId)
    {
        editForm = (await this.ReloadForm(editForm) as AuctionEditFormModel)!;

        var userIsAdmin = this.User.IsAdmin();

        editForm.UploadedImages = await this.auctionService.GetUploadedImagesForAuctionByIdAsync(editForm.Id, userId, userIsAdmin);

        return editForm;
    }
}