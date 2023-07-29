namespace RadCars.Web.Hubs;

using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Contracts;
using Data.Models.User;
using Data.Models.Entities;
using Data.Common.Contracts.Repositories;

public class AuctionHub : Hub<IAuctionClient>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IBackgroundJobClient backgroundJobClient;
    private readonly IDeletableEntityRepository<Auction> auctionsRepository;
    private readonly IDeletableEntityRepository<UserAuctionBid> userBidsRepository;

    public AuctionHub(UserManager<ApplicationUser> userManager, IDeletableEntityRepository<Auction> auctionsRepository, IDeletableEntityRepository<UserAuctionBid> userBidsRepository, IBackgroundJobClient backgroundJobClient)
    {
        this.userManager = userManager;
        this.auctionsRepository = auctionsRepository;
        this.userBidsRepository = userBidsRepository;
        this.backgroundJobClient = backgroundJobClient;
    }

    public async Task PlaceBid(string auctionId, decimal amount)
    {
        // Retrieve the user's ID from the HTTP context\
        var userId = this.Context.UserIdentifier;

        var user = await this.userManager.FindByIdAsync(userId);

        var auction = await this.auctionsRepository.All()
            .Where(a => a.Id.ToString() == auctionId && a.CreatorId.ToString() != userId)
            .FirstAsync();

        if (user != null)
        {
            // Retrieve the user's details

            // Logic to place a bid
            var bid = new UserAuctionBid
            {
                AuctionId = auction.Id,
                Amount = amount,
                BidderId = user.Id
            };

            await this.userBidsRepository.AddAsync(bid);
            await this.userBidsRepository.SaveChangesAsync();

            //Notify all clients about the new bid
            await Clients.All.BidPlaced(amount, user.FullName, user.UserName, bid.CreatedOn.ToString("G"));
        }
    }

    //public Task NotifyAuctionStartedAsync(string auctionId)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task NotifyAuctionEndedAsync(string auctionId)
    //{
    //    throw new NotImplementedException();
    //}

    //public void ScheduleAuctionStart(string auctionId, DateTime startTime)
    //{
    //    this.backgroundJobClient.Enqueue(() => this.NotifyAuctionStartedAsync(auctionId), startTime);
    //}

    //public string ScheduleAuctionEnd(string auctionId, DateTime endTime)
    //{
    //    var delay = endTime - DateTime.UtcNow;
    //    var jobId = this.backgroundJobClient.Schedule(() => this.NotifyAuctionEndedAsync(auctionId), delay);
    //    return jobId;
    //}

    //public Task CancelScheduledAuctionEnd(string jobId)
    //{
    //    this.backgroundJobClient.Delete(jobId);
    //}
}