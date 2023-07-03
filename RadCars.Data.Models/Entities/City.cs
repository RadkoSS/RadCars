namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.CityConstants;

public class City
{
    [Key]
    public ushort Id { get; set; }

    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    [Required]
    public ushort CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;
}