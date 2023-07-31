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
        var userId = this.Context.UserIdentifier;

        var user = await this.userManager.FindByIdAsync(userId);

        var auction = await this.auctionsRepository.All()
            .Where(a => a.Id.ToString() == auctionId && a.CreatorId.ToString() != user.Id.ToString())
            .FirstAsync();
        
        if (auction.Bids.Any() == false)
        {
            if (amount < auction.StartingPrice)
            {
                throw new InvalidOperationException();
            }
        }
        else
        {
            var highestBid = auction.Bids.OrderByDescending(b => b.CreatedOn).First().Amount;

            if (amount < highestBid + auction.MinimumBid)
            {
                throw new InvalidOperationException();
            }
        }

        if (auction.IsOver.HasValue == false || auction.IsOver is true)
        {
            throw new InvalidOperationException();
        }

        var bid = new UserAuctionBid
        {
            AuctionId = auction.Id,
            Amount = amount,
            BidderId = user.Id
        };

        await this.userBidsRepository.AddAsync(bid);
        await this.userBidsRepository.SaveChangesAsync();

        await this.Clients.All.BidPlaced(amount, user.FullName, user.UserName, bid.CreatedOn.ToLocalTime().ToString("f"));

        if (auction.BlitzPrice.HasValue && bid.Amount >= auction.BlitzPrice.Value)
        {
            this.CancelScheduledJob(auction.EndAuctionJobId!);

            auction.IsOver = true;
            await this.auctionsRepository.SaveChangesAsync();

            var lastBidTime = auction.Bids.OrderByDescending(b => b.CreatedOn).First().CreatedOn.ToLocalTime().ToString("f");

            var winnerFullNameAndUserName = $"{user.FullName} ({user.UserName})";

            await this.Clients.All.AuctionEnded(auctionId, lastBidTime, amount, winnerFullNameAndUserName);
        }
    }

    private void CancelScheduledJob(string jobId)
    => this.backgroundJobClient.Delete(jobId);
}