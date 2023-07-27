// ReSharper disable VirtualMemberCallInConstructor
namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using User;
using Common.Models;

using static RadCars.Common.EntityValidationConstants.ApplicationUser;
using static RadCars.Common.EntityValidationConstants.ListingConstants;

public class Auction : BaseDeletableModel<Guid>
{
    public Auction()
    {
        this.Id = Guid.NewGuid();

        this.Bids = new HashSet<UserAuctionBid>();
        this.Images = new HashSet<AuctionCarImage>();
        this.Favorites = new HashSet<UserFavoriteAuction>();
        this.AuctionFeatures = new HashSet<AuctionFeature>();
    }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public decimal StartingPrice { get; set; }

    [Required]
    public decimal CurrentPrice { get; set; }

    public decimal? BlitzPrice { get; set; }

    [Required]
    public int MinimumBid { get; set; }

    [Required]
    [StringLength(TitleMaximumLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaximumLength)]
    public string Description { get; set; } = null!;

    [Required]
    [StringLength(PhoneNumberMaximumLength)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public int Year { get; set; }

    [Required]
    public int Mileage { get; set; }

    [Required]
    [StringLength(EngineModelMaximumLength)]
    public string EngineModel { get; set; } = null!;

    [Required]
    public int EngineTypeId { get; set; }

    public virtual EngineType EngineType { get; set; } = null!;

    [Required]
    [StringLength(VinNumberMaximumLength)]
    public string VinNumber { get; set; } = null!;

    [Required]
    public Guid CreatorId { get; set; }

    public virtual ApplicationUser Creator { get; set; } = null!;

    [Required]
    public int CarMakeId { get; set; }

    public virtual CarMake CarMake { get; set; } = null!;

    [Required]
    public int CarModelId { get; set; }

    public virtual CarModel CarModel { get; set; } = null!;

    public Guid? ThumbnailId { get; set; }

    public virtual AuctionCarImage? Thumbnail { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<UserAuctionBid> Bids { get; set; }

    public virtual ICollection<AuctionFeature> AuctionFeatures { get; set; }

    public virtual ICollection<UserFavoriteAuction> Favorites { get; set; }

    public virtual ICollection<AuctionCarImage> Images { get; set; }

    //We will support more countries in the future, not only Bulgaria
    //public int CountryId { get; set; }

    //public virtual Country Country { get; set; } = null!;
}