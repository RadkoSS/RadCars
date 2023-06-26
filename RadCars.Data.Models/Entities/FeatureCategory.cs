namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

public class FeatureCategory
{
    public FeatureCategory()
    {
        this.Features = new HashSet<Feature>();
    }

    [Key]
    public ushort Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public virtual ICollection<Feature> Features { get; set; }
}