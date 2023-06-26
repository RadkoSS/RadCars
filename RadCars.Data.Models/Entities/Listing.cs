namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using User;
using Enums;

public class Listing
{
    public Listing()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public DateTime Year { get; set; }

    [Required]
    public uint Mileage { get; set; }

    [Required]
    public string EngineModel { get; set; } = null!;

    [Required]
    public EngineType EngineType { get; set; }

    public string? EngineCode { get; set; }

    [Required]
    public string VinNumber { get; set; } = null!;

    [Required]
    public Guid CreatorId { get; set; }

    public virtual ApplicationUser Creator { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Make))]
    public ushort MakeId { get; set; }

    public virtual Make Make { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Model))]
    public ushort ModelId { get; set; }

    public virtual Model Model { get; set; } = null!;

    public virtual ICollection<FeatureCategory> CategoriesWithFeatures { get; set; }
}