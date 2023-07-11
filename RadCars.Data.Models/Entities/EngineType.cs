namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using Common.Models;
using static RadCars.Common.EntityValidationConstants.CarEngineTypeConstants;

public class EngineType : BaseDeletableModel<int>
{
    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;
}