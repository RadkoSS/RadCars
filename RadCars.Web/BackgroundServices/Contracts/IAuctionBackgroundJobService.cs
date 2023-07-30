namespace RadCars.Web.BackgroundServices.Contracts;

public interface IAuctionBackgroundJobService
{
    Task<string> ScheduleAuctionStart(string auctionId);

    Task<string> ScheduleAuctionEnd(string auctionId);

    Task StartAuction(string auctionId);

    Task EndAuction(string auctionId);
}