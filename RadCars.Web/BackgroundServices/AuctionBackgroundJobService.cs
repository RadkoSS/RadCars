namespace RadCars.Web.BackgroundServices;

using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using Hubs;
using Contracts;
using Hubs.Contracts;
using Data.Models.Entities;
using Services.Messaging.Contracts;
using Data.Common.Contracts.Repositories;

using static Common.GeneralApplicationConstants;
using static Services.Messaging.Templates.EmailTemplates.AuctionTemplates;

public class AuctionBackgroundJobService : IAuctionBackgroundJobService
{
    private readonly IEmailSender emailSender;
    private readonly IBackgroundJobClient backgroundJobClient;
    private readonly IHubContext<AuctionHub, IAuctionClient> auctionHub;
    private readonly IDeletableEntityRepository<Auction> auctionsRepository;

    public AuctionBackgroundJobService(IBackgroundJobClient backgroundJobClient,
        IHubContext<AuctionHub, 
        IAuctionClient> auctionHub,
        IDeletableEntityRepository<Auction> auctionsRepository,
        IEmailSender emailSender)
    {
        this.auctionHub = auctionHub;
        this.emailSender = emailSender;
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

        await this.auctionHub.Clients.All
            .AuctionStarted(auctionId, auction.CreatorId.ToString(), auction.EndTime.ToLocalTime(), auction.StartingPrice, auction.MinimumBid);

        await this.auctionHub.Clients.All.AllPageAuctionStarted(auctionId, auction.EndTime.ToLocalTime(), auction.StartingPrice);
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

        var auctionEndedAnnounce = NoWinnerAnnounce;

        if (auction.Bids.Any())
        {
            var lastBid = auction.Bids.OrderByDescending(b => b.CreatedOn).First();

            lastBidTime = lastBid.CreatedOn.ToLocalTime().ToString("f");

            winnerFullNameAndUserName = $"{lastBid.Bidder.FullName} ({lastBid.Bidder.UserName})";

            lastBidAmount = lastBid.Amount;

            auctionEndedAnnounce = string.Format(WinnerAnnounce, winnerFullNameAndUserName, lastBidAmount, lastBidTime,
                lastBid.Bidder.Email, lastBid.Bidder.PhoneNumber);
        }

        await this.auctionHub.Clients.All.AuctionEnded(auctionId, lastBidTime, lastBidAmount, winnerFullNameAndUserName);
        await this.auctionHub.Clients.All.AllPageAuctionEnded(auctionId);

        var emailToCreatorHtmlContent = string.Format(AuctionEndedEmailToCreator, auction.Creator.FullName,
            auction.CarMake.Name, auction.CarModel.Name, auction.Thumbnail!.Url, auction.Title, auctionEndedAnnounce);

        await this.emailSender.SendEmailAsync(SendGridSenderEmail, SendGridSenderName, auction.Creator.Email,
            $"Край на търга за {auction.CarMake.Name} {auction.CarModel.Name}", emailToCreatorHtmlContent);
    }

    public async Task RescheduleEditedAuctionStartAndEndAsync(string auctionId)
    {
        var editedAuction = await this.auctionsRepository.AllWithDeleted()
            .Where(a => a.Id.ToString() == auctionId)
            .FirstAsync();

        if (string.IsNullOrWhiteSpace(editedAuction.StartAuctionJobId) == false
            || string.IsNullOrWhiteSpace(editedAuction.EndAuctionJobId) == false
            || editedAuction.IsOver.HasValue)
        {
            this.CancelBackgroundJob(editedAuction.StartAuctionJobId!);
            this.CancelBackgroundJob(editedAuction.EndAuctionJobId!);

            editedAuction.StartAuctionJobId = null;
            editedAuction.EndAuctionJobId = null;
            editedAuction.IsOver = null;

            await this.auctionsRepository.SaveChangesAsync();
        }

        var currentTime = DateTime.UtcNow;

        var startTimeDateOfInvoke = editedAuction.StartTime - currentTime;

        var startAuctionJobId = this.backgroundJobClient.Schedule(() => this.StartAuction(auctionId), startTimeDateOfInvoke);

        editedAuction.StartAuctionJobId = startAuctionJobId;

        var endTimeDateOfInvoke = editedAuction.EndTime - currentTime;

        var endAuctionJobId = this.backgroundJobClient.Schedule(() => this.EndAuction(auctionId), endTimeDateOfInvoke);

        editedAuction.EndAuctionJobId = endAuctionJobId;

        await this.auctionsRepository.SaveChangesAsync();

        await this.auctionHub.Clients.All.AllPageAuctionChanged(auctionId, editedAuction.StartTime, editedAuction.StartingPrice);
        await this.auctionHub.Clients.All.AuctionChangedOrDeleted(auctionId);
    }

    public async Task CancelAuctionStartAndEnd(string auctionId)
    {
        var auction = await this.auctionsRepository.AllWithDeleted()
            .Where(a => a.Id.ToString() == auctionId)
            .FirstAsync();

        this.CancelBackgroundJob(auction.StartAuctionJobId!);
        this.CancelBackgroundJob(auction.EndAuctionJobId!);

        auction.StartAuctionJobId = null;
        auction.EndAuctionJobId = null;
        auction.IsOver = null;

        await this.auctionsRepository.SaveChangesAsync();

        await this.auctionHub.Clients.All.AllPageAuctionDeleted(auctionId);
        await this.auctionHub.Clients.All.AuctionChangedOrDeleted(auctionId);
    }

    private void CancelBackgroundJob(string jobId) 
        => this.backgroundJobClient.Delete(jobId);
}