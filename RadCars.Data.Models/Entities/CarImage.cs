namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

public class CarImage
{
    public CarImage()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Url { get; set; } = null!;

    [Required]
    public Guid ListingId { get; set; }

    public virtual Listing Listing { get; set; } = null!;
}