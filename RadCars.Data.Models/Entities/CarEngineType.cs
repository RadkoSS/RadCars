namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.CarEngineTypeConstants;

public class CarEngineType
{
    [Key]
    public byte Id { get; set; }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;
}