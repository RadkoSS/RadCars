namespace RadCars.Web.Hubs.Contracts;

public interface IAuctionClient
{
    Task BidPlaced(decimal amount, string userFullName, string userUserName, string timePlaced);

    Task AuctionStarted(string auctionId);

    Task AuctionEnded(string auctionId);
}