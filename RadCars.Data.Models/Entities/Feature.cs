namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Common.EntityValidationConstants.FeatureConstants;

public class Feature
{
    public Feature()
    {
        this.ListingFeatures = new HashSet<ListingFeature>();
    }

    [Key]
    public ushort Id { get; set; }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;

    //[ForeignKey(nameof(Category))]
    public ushort CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ListingFeature> ListingFeatures { get; set; }
}