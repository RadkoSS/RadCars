namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using Common.Models;
using static RadCars.Common.EntityValidationConstants.CityConstants;

public class City : BaseDeletableModel<int>
{
    [Required]
    [StringLength(NameMaximumLength)]
    public string Name { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    [Required]
    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;
}