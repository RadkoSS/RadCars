namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using Common.Models;
using static RadCars.Common.EntityValidationConstants.CountryConstants;

public class Country : BaseDeletableModel<int>
{
    public Country()
    {
        this.Cities = new HashSet<City>();
    }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; }
}