namespace RadCars.Web.BackgroundServices.Contracts;

public interface IAuctionBackgroundJobService
{
    Task ScheduleAuctionStart(string auctionId);

    Task ScheduleAuctionEnd(string auctionId);

    Task RescheduleEditedAuctionStartAndEndAsync(string auctionId);

    Task CancelAuctionStartAndEnd(string auctionId);
}