namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.EntityValidationConstants.CarEngineTypeConstants;

public class EngineTypes
{
    [Key]
    public byte Id { get; set; }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;
}