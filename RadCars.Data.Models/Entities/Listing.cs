namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using User;
using Common.Models;
using static RadCars.Common.EntityValidationConstants.ListingConstants;

public class Listing : BaseDeletableModel<Guid>
{
    public Listing()
    {
        this.Id = Guid.NewGuid();
        //this.CreatedOn = DateTime.UtcNow;

        this.ListingFeatures = new HashSet<ListingFeature>();
        this.Favorites = new HashSet<UserFavoriteListing>();
        this.Images = new HashSet<CarImage>();
    }

    [Required]
    [StringLength(TitleMaximumLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaximumLength)]
    public string Description { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

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

    public virtual CarImage? Thumbnail { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<ListingFeature> ListingFeatures { get; set; }

    public virtual ICollection<UserFavoriteListing> Favorites { get; set; }

    public virtual ICollection<CarImage> Images { get; set; }

    //We will support more countries in the future, not only Bulgaria
    //public int CountryId { get; set; }

    //public virtual Country Country { get; set; } = null!;
}