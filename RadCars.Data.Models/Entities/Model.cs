namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Model
{
    [Key]
    public ushort Id { get; set; }

    [Required]
    [StringLength(1)]
    public string Name { get; set; } = null!;

    [ForeignKey(nameof(Make))]
    public ushort MakeId { get; set; }

    public virtual Make Make { get; set; } = null!;
}