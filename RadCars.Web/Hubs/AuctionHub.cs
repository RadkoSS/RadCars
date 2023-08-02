namespace RadCars.Web.Hubs;

using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;

using Contracts;
using Data.Models.User;
using Services.Data.Contracts;

public class AuctionHub : Hub<IAuctionClient>
{
    private readonly IAuctionService auctionService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IBackgroundJobClient backgroundJobClient;

    public AuctionHub(UserManager<ApplicationUser> userManager, IBackgroundJobClient backgroundJobClient, IAuctionService auctionService)
    {
        this.userManager = userManager;
        this.auctionService = auctionService;
        this.backgroundJobClient = backgroundJobClient;
    }

    public async Task PlaceBid(string auctionId, decimal amount)
    {
        var userId = this.Context.UserIdentifier;

        var user = await this.userManager.FindByIdAsync(userId);

        var bidServiceModel = await this.auctionService.CreateBidAsync(auctionId, user.Id.ToString(), amount);

        await this.Clients.All.BidPlaced(auctionId, amount, user.FullName, user.UserName, bidServiceModel.CreatedOn);

        await this.Clients.All.AllPageBidPlaced(auctionId, amount);

        if (bidServiceModel.OverBlitzPrice)
        {
            this.CancelScheduledJob(bidServiceModel.EndAuctionJobId!);

            await this.Clients.All.AuctionEnded(auctionId, bidServiceModel.CreatedOn, amount, bidServiceModel.UserFullNameAndUserName);

            await this.Clients.All.AllPageAuctionEnded(auctionId);
        }
    }

    private void CancelScheduledJob(string jobId)
    => this.backgroundJobClient.Delete(jobId);
}