namespace RadCars.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Data.Contracts;

using static Common.NotificationTypeConstants;
using static Common.ExceptionsAndNotificationsMessages;

public class AuctionController : BaseAdminController
{
    private readonly IAuctionService auctionService;

    public AuctionController(IAuctionService auctionService)
    {
        this.auctionService = auctionService;
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string auctionId)
    {
        try
        {
            await this.auctionService.HardDeleteAuctionByIdAsync(auctionId);

            this.TempData[WarningMessage] = AuctionDeletedSuccessfully;

            return RedirectToAction("All", "Auction", new { Area = "" });
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Index", "Home");
        }
    }
}