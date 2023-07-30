namespace RadCars.Web.BackgroundServices.Contracts;

public interface IAuctionBackgroundJobService
{
    Task ScheduleAuctionStart(string auctionId);

    Task ScheduleAuctionEnd(string auctionId);

    Task StartAuction(string auctionId);

    Task EndAuction(string auctionId);
}