namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public DateTime Year { get; set; }

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
    //[ForeignKey(nameof(Creator))]
    public Guid CreatorId { get; set; }

    public virtual ApplicationUser Creator { get; set; } = null!;

    [Required]
    //[ForeignKey(nameof(CarMake))]
    public ushort CarMakeId { get; set; }

    public virtual CarMake CarMake { get; set; } = null!;

    [Required]
    //[ForeignKey(nameof(CarModel))]
    public ushort CarModelId { get; set; }

    public virtual CarModel CarModel { get; set; } = null!;

    public virtual ICollection<ListingFeature> ListingFeatures { get; set; }

    public virtual ICollection<UserFavoriteListing> Favorites { get; set; }
}