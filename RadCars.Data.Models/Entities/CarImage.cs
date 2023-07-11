namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using Common.Models;

public class CarImage : BaseDeletableModel<Guid>
{
    public CarImage()
    {
        this.Id = Guid.NewGuid();
    }

    [Required]
    public string Url { get; set; } = null!;

    [Required]
    public Guid ListingId { get; set; }

    public virtual Listing Listing { get; set; } = null!;
}