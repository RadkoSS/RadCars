namespace RadCars.Web.BackgroundServices;

using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using Hubs;
using Contracts;
using Hubs.Contracts;
using Data.Models.Entities;
using Data.Common.Contracts.Repositories;

public class AuctionBackgroundJobService : IAuctionBackgroundJobService
{
    private readonly IBackgroundJobClient backgroundJobClient;
    private readonly IHubContext<AuctionHub, IAuctionClient> auctionHub;
    private readonly IDeletableEntityRepository<Auction> auctionsRepository;

    public AuctionBackgroundJobService(IBackgroundJobClient backgroundJobClient, IHubContext<AuctionHub, IAuctionClient> auctionHub, IDeletableEntityRepository<Auction> auctionsRepository)
    {
        this.auctionHub = auctionHub;
        this.auctionsRepository = auctionsRepository;
        this.backgroundJobClient = backgroundJobClient;
    }

    public async Task ScheduleAuctionStart(string auctionId)
    {
        var auctionToSchedule = await this.auctionsRepository.All().Where(a => a.Id.ToString() == auctionId).FirstAsync();

        var dateOfInvoke = auctionToSchedule.StartTime - auctionToSchedule.CreatedOn;

        var jobId = this.backgroundJobClient.Schedule(() => this.StartAuction(auctionId), dateOfInvoke);

        auctionToSchedule.StartAuctionJobId = jobId;

        await this.auctionsRepository.SaveChangesAsync();
    }

    public async Task ScheduleAuctionEnd(string auctionId)
    {
        var auctionToSchedule = await this.auctionsRepository.All().Where(a => a.Id.ToString() == auctionId).FirstAsync();

        var dateOfInvoke = auctionToSchedule.EndTime - auctionToSchedule.CreatedOn;

        var jobId = this.backgroundJobClient.Schedule(() => this.EndAuction(auctionId), dateOfInvoke);

        auctionToSchedule.EndAuctionJobId = jobId;

        await this.auctionsRepository.SaveChangesAsync();
    }

    public async Task StartAuction(string auctionId)
    {
        var auction = await this.auctionsRepository.All()
            .Where(a => a.Id.ToString() == auctionId)
            .FirstAsync();

        auction.IsOver = false;

        await this.auctionsRepository.SaveChangesAsync();

        await this.auctionHub.Clients.All.AuctionStarted(auctionId, auction.CreatorId.ToString(), auction.EndTime.ToLocalTime().ToString("f"), auction.StartingPrice, auction.MinimumBid);
    }

    public async Task EndAuction(string auctionId)
    {
        var auction = await this.auctionsRepository.All()
            .Where(a => a.Id.ToString() == auctionId)
            .FirstAsync();

        auction.IsOver = true;

        await this.auctionsRepository.SaveChangesAsync();

        var lastBidAmount = 0m;
        var lastBidTime = string.Empty;
        var winnerFullNameAndUserName = string.Empty;

        if (auction.Bids.Any())
        {
            lastBidTime = auction.Bids.OrderByDescending(b => b.CreatedOn).First().CreatedOn.ToLocalTime().ToString("f");

            winnerFullNameAndUserName = $"{auction.Bids.OrderByDescending(b => b.CreatedOn).First().Bidder.FullName} ({auction.Bids.OrderByDescending(b => b.CreatedOn).First().Bidder.UserName})";

            lastBidAmount = auction.Bids.OrderByDescending(b => b.CreatedOn).First().Amount;
        }

        await this.auctionHub.Clients.All.AuctionEnded(auctionId, lastBidTime, lastBidAmount, winnerFullNameAndUserName);
    }

    public async Task CancelAuction(string auctionId)
    {
        var auction = await this.auctionsRepository.All()
            .Where(a => a.Id.ToString() == auctionId)
            .FirstAsync();

        this.CancelBackgroundJob(auction.StartAuctionJobId!);
        this.CancelBackgroundJob(auction.EndAuctionJobId!);

        auction.StartAuctionJobId = null;
        auction.EndAuctionJobId = null;
        auction.IsOver = null;

        await this.auctionsRepository.SaveChangesAsync();
    }

    private void CancelBackgroundJob(string jobId) 
        => this.backgroundJobClient.Delete(jobId);
}