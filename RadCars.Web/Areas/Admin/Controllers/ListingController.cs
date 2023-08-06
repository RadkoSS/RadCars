namespace RadCars.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using Web.ViewModels.Listing;
using Services.Data.Contracts;
using RadCars.Web.ViewModels.City;
using RadCars.Web.ViewModels.CarMake;

using static Common.NotificationTypeConstants;
using static Common.GeneralApplicationConstants;
using static Common.ExceptionsAndNotificationsMessages;

public class ListingController : BaseAdminController
{
    private readonly ICarService carService;
    private readonly IMemoryCache memoryCache;
    private readonly IListingService listingService;

    public ListingController(IListingService listingService, ICarService carService, IMemoryCache memoryCache)
    {
        this.carService = carService;
        this.memoryCache = memoryCache;
        this.listingService = listingService;
    }

    public async Task<IActionResult> AllActive([FromQuery] AllListingsQueryModel queryModel)
    {
        var withDeletedListings = false;

        try
        {
            var serviceModel = await this.listingService.GetAllListingsAsync(queryModel, withDeletedListings);

            queryModel.Listings = serviceModel.Listings;
            queryModel.TotalListings = serviceModel.TotalListingsCount;

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

    public async Task<IActionResult> AllDeactivated([FromQuery] AllListingsQueryModel queryModel)
    {
        var withDeletedListings = true;

        try
        {
            var serviceModel = await this.listingService.GetAllListingsAsync(queryModel, withDeletedListings);

            queryModel.Listings = serviceModel.Listings;
            queryModel.TotalListings = serviceModel.TotalListingsCount;

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
}