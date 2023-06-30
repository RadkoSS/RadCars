namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

public class CarPicture
{
    public CarPicture()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Url { get; set; } = null!;

    public Guid ListingId { get; set; }

    public virtual Listing Listing { get; set; } = null!;
}