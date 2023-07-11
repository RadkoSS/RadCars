namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using Common.Models;
using static RadCars.Common.EntityValidationConstants.MakeConstants;

public class CarMake : BaseDeletableModel<int>
{
    public CarMake()
    {
        this.Models = new HashSet<CarModel>();
    }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;
    
    public virtual ICollection<CarModel> Models { get; set; }
}