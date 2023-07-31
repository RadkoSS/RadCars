namespace RadCars.Web.ViewModels.Auction;

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using CarImage;
using FeatureCategory;
using Data.Models.Entities;

public class AuctionDetailsViewModel : AllAuctionsViewModel
{
    public AuctionDetailsViewModel()
    {
        this.Images = new HashSet<ImageViewModel>();
        this.Bids = new HashSet<AuctionBidViewModel>();
        this.AuctionFeatures = new HashSet<FeatureCategoriesViewModel>();
    }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal? BlitzPrice { get; set; }

    public int MinimumBid { get; set; }

    public bool IsDeleted { get; set; }

    public bool? IsOver { get; set; }

    public string CreatorUserName { get; set; } = null!;

    public string CreatorFullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string VinNumber { get; set; } = null!;

    public int FavoritesCount { get; set; }

    public string? CurrentWinnerFullNameAndUserName { get; set; }

    public decimal CurrentHighestBid { get; set; }

    public string? CurrentHighestBidTimeOfCreate { get; set; }

    [Display(Name = "Наддавания:")]
    public ICollection<AuctionBidViewModel> Bids { get; set; }

    [Display(Name = "Екстри:")]
    public ICollection<FeatureCategoriesViewModel> AuctionFeatures { get; set; }

    public ICollection<ImageViewModel> Images { get; set; }

    public override void CreateMappings(IProfileExpression configuration)
    {
        configuration
            .CreateMap<Auction, AuctionDetailsViewModel>()
            .ForMember(destination => destination.AuctionFeatures, options => options.Ignore())
            .ForMember(destination => destination.Bids, options => options.MapFrom(source => source.Bids.OrderBy(a => a.CreatedOn)))
            .ForMember(destination => destination.CurrentWinnerFullNameAndUserName, options => options.MapFrom(source => source.Bids.Any() ? $"{source.Bids.OrderByDescending(b => b.Amount).First().Bidder.FullName} ({source.Bids.OrderByDescending(b => b.Amount).First().Bidder.UserName})" : string.Empty))
            .ForMember(destination => destination.CurrentHighestBid, options => options.MapFrom(source => source.Bids.Any() ? source.Bids.Max(b => b.Amount) : 0))
            .ForMember(destination => destination.StartTime, options => options.MapFrom(source => source.StartTime.ToLocalTime()))
            .ForMember(destination => destination.EndTime, options => options.MapFrom(source => source.EndTime.ToLocalTime()))
            .ForMember(destination => destination.CurrentHighestBidTimeOfCreate, options => options.MapFrom(source => source.Bids.Any() ? source.Bids.OrderByDescending(b => b.Amount).First().CreatedOn.ToLocalTime().ToString("g") : string.Empty));
    }
}