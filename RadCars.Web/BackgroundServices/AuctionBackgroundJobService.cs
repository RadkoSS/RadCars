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

    public async Task<string> ScheduleAuctionStart(string auctionId)
    {
        var auctionToSchedule = await this.auctionsRepository.All().Where(a => a.Id.ToString() == auctionId).FirstAsync();

        var dateOfInvoke = auctionToSchedule.StartTime - auctionToSchedule.CreatedOn;

        var jobId = this.backgroundJobClient.Schedule(() => this.StartAuction(auctionId), dateOfInvoke);

        //auctionToSchedule.StartAuctionJobId = jobId;

        //await this.auctionsRepository.SaveChangesAsync();

        return jobId;
    }

    public async Task<string> ScheduleAuctionEnd(string auctionId)
    {
        var auctionToSchedule = await this.auctionsRepository.All().Where(a => a.Id.ToString() == auctionId).FirstAsync();

        var dateOfInvoke = auctionToSchedule.EndTime - auctionToSchedule.CreatedOn;

        var jobId = this.backgroundJobClient.Schedule(() => this.EndAuction(auctionId), dateOfInvoke);

        auctionToSchedule.EndAuctionJobId = jobId;

        await this.auctionsRepository.SaveChangesAsync();

        return jobId;
    }

    public async Task StartAuction(string auctionId)
    {
        var auction = await this.auctionsRepository.All()
            .Where(a => a.Id.ToString() == auctionId)
            .FirstAsync();

        auction.IsOver = false;

        await this.auctionsRepository.SaveChangesAsync();

        await this.auctionHub.Clients.All.AuctionStarted(auctionId);
    }

    public async Task EndAuction(string auctionId)
    {
        var auction = await this.auctionsRepository.All()
            .Where(a => a.Id.ToString() == auctionId)
            .FirstAsync();

        auction.IsOver = true;

        await this.auctionHub.Clients.All.AuctionEnded(auctionId);
    }
}