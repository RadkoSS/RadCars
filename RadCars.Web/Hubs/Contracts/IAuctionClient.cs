namespace RadCars.Web.Hubs.Contracts;

public interface IAuctionClient
{
    //AuctionDetails
    Task BidPlaced(decimal amount, string userFullName, string userUserName, string timePlaced);

    Task AuctionStarted(string auctionId, string creatorId, DateTime endTime, decimal startingPrice, decimal minimumBid);

    Task AuctionEnded(string auctionId, string lastBidTime, decimal lastBidAmount, string winnerFullNameAndUserName);

    //AllAuctions
    Task AllPageBidPlaced(string auctionId, decimal amount);

    Task AllPageAuctionStarted(string auctionId, DateTime endTime, decimal startingPrice);

    Task AllPageAuctionEnded(string auctionId);
}