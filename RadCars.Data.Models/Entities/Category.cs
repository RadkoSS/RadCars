namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.FeatureCategoryConstants;

public class Category
{
    public Category()
    {
        this.Features = new HashSet<Feature>();
    }

    [Key]
    public ushort Id { get; set; }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Feature> Features { get; set; }
}