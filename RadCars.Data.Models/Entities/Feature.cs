namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

public class Feature
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual FeatureCategory Category { get; set; } = null!;
}