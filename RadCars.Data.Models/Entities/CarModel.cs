namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.ModelConstants;

public class CarModel
{
    [Key]
    public ushort Id { get; set; }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;
    
    public ushort CarMakeId { get; set; }

    public virtual CarMake CarMake { get; set; } = null!;
}