namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.MakeConstants;

public class CarMake
{
    public CarMake()
    {
        this.Models = new HashSet<CarModel>();
    }

    [Key]
    public ushort Id { get; set; }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;
    
    public virtual ICollection<CarModel> Models { get; set; }
}