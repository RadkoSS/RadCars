namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Griesoft.AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Authorization;

using Common.Exceptions;
using ViewModels.Auction;
using ViewModels.Thumbnail;
using Services.Data.Contracts;
using Infrastructure.Extensions;
using BackgroundServices.Contracts;

using static Common.NotificationTypeConstants;
using static Common.ExceptionsAndNotificationsMessages;
using static Common.EntityValidationConstants.ListingConstants;

public class AuctionController : BaseController
{
    private readonly ICarService carService;
    private readonly IUserService userService;
    private readonly IAuctionService auctionService;
    private readonly IAuctionBackgroundJobService auctionBackgroundJobService;

    public AuctionController(IAuctionService auctionService, IUserService userService, ICarService carService, IAuctionBackgroundJobService auctionBackgroundJobService)
    {
        this.carService = carService;
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

            this.TempData[SuccessMessage] = AuctionWasUpdatedSuccessfully;

            return RedirectToAction("ChooseThumbnail", "Auction", new { auctionId });
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
        try
        {
            var serviceModel = await this.auctionService.GetAllAuctionsAsync(queryModel);

            queryModel.Auctions = serviceModel.Auctions;
            queryModel.TotalAuctions = serviceModel.TotalAuctionsCount;
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
    public async Task<IActionResult> Details(string auctionId)
    {
        try
        {
            var listingViewModel = await this.auctionService.GetAuctionDetailsAsync(auctionId);

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

            var deactivatedListingModel = await this.auctionService.GetDeactivatedAuctionDetailsAsync(listingId, userId, userIsAdmin);

            return View("Details", deactivatedListingModel);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    private async Task<AuctionFormModel> ReloadForm(AuctionFormModel form)
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

    private async Task<AuctionEditFormModel> ReloadEditForm(AuctionEditFormModel editForm, string userId)
    {
        editForm = (await this.ReloadForm(editForm) as AuctionEditFormModel)!;

        var userIsAdmin = this.User.IsAdmin();

        editForm.UploadedImages = await this.auctionService.GetUploadedImagesForAuctionByIdAsync(editForm.Id, userId, userIsAdmin);

        return editForm;
    }
}