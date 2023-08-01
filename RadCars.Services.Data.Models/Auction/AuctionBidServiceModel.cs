namespace RadCars.Services.Data.Models.Auction;

public class AuctionBidServiceModel
{
    public string UserFullNameAndUserName { get; set; } = null!;
    
    public string CreatedOn { get; set; } = null!;

    public bool OverBlitzPrice { get; set; }

    public string? EndAuctionJobId { get; set; }
}