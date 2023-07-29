﻿namespace RadCars.Web.Hubs.Contracts;

public interface IAuctionClient
{
    Task PlaceBid(string auctionId, decimal amount);

    Task BidPlaced(decimal amount, string userFullName, string userUserName, string timePlaced);

    Task NotifyAuctionStartedAsync(string auctionId);

    Task NotifyAuctionEndedAsync(string auctionId);

    Task CancelScheduledAuctionEnd(string jobId);

    public void ScheduleAuctionStart(string auctionId, DateTime startTime);

    public string ScheduleAuctionEnd(string auctionId, DateTime endTime);
}