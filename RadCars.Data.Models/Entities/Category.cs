namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using Common.Models;
using static RadCars.Common.EntityValidationConstants.FeatureCategoryConstants;

public class Category : BaseDeletableModel<int>
{
    public Category()
    {
        this.Features = new HashSet<Feature>();
    }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Feature> Features { get; set; }
}