namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.CountryConstants;

public class Country
{
    public Country()
    {
        this.Cities = new HashSet<City>();
    }

    [Key]
    public ushort Id { get; set; }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; }
}