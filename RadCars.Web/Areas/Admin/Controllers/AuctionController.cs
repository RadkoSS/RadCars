namespace RadCars.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using ViewModels.City;
using ViewModels.Auction;
using ViewModels.CarMake;
using Services.Data.Contracts;

using static Common.NotificationTypeConstants;
using static Common.GeneralApplicationConstants;
using static Common.ExceptionsAndNotificationsMessages;

public class AuctionController : BaseAdminController
{
    private readonly ICarService carService;
    private readonly IMemoryCache memoryCache;
    private readonly IAuctionService auctionService;

    public AuctionController(IAuctionService auctionService, ICarService carService, IMemoryCache memoryCache)
    {
        this.carService = carService;
        this.memoryCache = memoryCache;
        this.auctionService = auctionService;
    }

    public async Task<IActionResult> AllActive([FromQuery] AllAuctionsQueryModel queryModel)
    {
        var withDeletedAuctions = false;

        try
        {
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

    public async Task<IActionResult> AllDeactivated([FromQuery] AllAuctionsQueryModel queryModel)
    {
        var withDeletedAuctions = true;

        try
        {
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

    [HttpPost]
    public async Task<IActionResult> Delete(string auctionId)
    {
        try
        {
            await this.auctionService.HardDeleteAuctionByIdAsync(auctionId);

            this.TempData[WarningMessage] = AuctionDeletedSuccessfully;

            return RedirectToAction("AllDeactivated", "Auction", new { Area = AdminAreaName });
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Index", "Home");
        }
    }
}