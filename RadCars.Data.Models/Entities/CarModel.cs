namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using Common.Models;
using static RadCars.Common.EntityValidationConstants.ModelConstants;

public class CarModel : BaseDeletableModel<int>
{
    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;
    
    public int CarMakeId { get; set; }

    public virtual CarMake CarMake { get; set; } = null!;
}