namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using User;
using Enums;

using static Common.EntityValidationConstants.ListingConstants;

public class Listing
{
    public Listing()
    {
        this.Id = Guid.NewGuid();
        this.CreatedOn = DateTime.UtcNow;

        this.ListingFeatures = new HashSet<ListingFeature>();
        this.Favorites = new HashSet<UserFavoriteListing>();
        this.Pictures = new HashSet<CarPicture>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    public DateTime? EditedOn { get; set; }

    [Required]
    [StringLength(TitleMaximumLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaximumLength)]
    public string Description { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public ushort Year { get; set; }

    [Required]
    public uint Mileage { get; set; }

    [Required]
    [StringLength(EngineModelMaximumLength)]
    public string EngineModel { get; set; } = null!;

    [Required]
    public EngineType EngineType { get; set; }

    [Required]
    [StringLength(VinNumberMaximumLength)]
    public string VinNumber { get; set; } = null!;

    [Required]
    public Guid CreatorId { get; set; }

    public virtual ApplicationUser Creator { get; set; } = null!;

    [Required]
    public ushort CarMakeId { get; set; }

    public virtual CarMake CarMake { get; set; } = null!;

    [Required]
    public ushort CarModelId { get; set; }

    public virtual CarModel CarModel { get; set; } = null!;
    
    public Guid? ThumbnailId { get; set; }

    public virtual CarPicture? Thumbnail { get; set; }

    public ushort CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<ListingFeature> ListingFeatures { get; set; }

    public virtual ICollection<UserFavoriteListing> Favorites { get; set; }

    public virtual ICollection<CarPicture> Pictures { get; set; }

    //We will support more countries in the future, not only Bulgaria
    //public ushort CountryId { get; set; }

    //public virtual Country Country { get; set; } = null!;
}