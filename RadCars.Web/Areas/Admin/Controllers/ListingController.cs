namespace RadCars.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Web.ViewModels.Listing;
using Services.Data.Contracts;

using static Common.NotificationTypeConstants;
using static Common.ExceptionsAndNotificationsMessages;

public class ListingController : BaseAdminController
{
    private readonly ICarService carService;
    private readonly IListingService listingService;

    public ListingController(IListingService listingService, ICarService carService)
    {
        this.listingService = listingService;
        this.carService = carService;
    }

    public async Task<IActionResult> AllActive([FromQuery] AllListingsQueryModel queryModel)
    {
        var withDeletedListings = false;

        try
        {
            var serviceModel = await this.listingService.GetAllListingsAsync(queryModel, withDeletedListings);

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

    public async Task<IActionResult> AllDeactivated([FromQuery] AllListingsQueryModel queryModel)
    {
        var withDeletedListings = true;

        try
        {
            var serviceModel = await this.listingService.GetAllListingsAsync(queryModel, withDeletedListings);

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
}