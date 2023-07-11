namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using Common.Models;
using static RadCars.Common.EntityValidationConstants.FeatureConstants;

public class Feature : BaseDeletableModel<int>
{
    public Feature()
    {
        this.ListingFeatures = new HashSet<ListingFeature>();
    }
    
    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;
    
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ListingFeature> ListingFeatures { get; set; }
}