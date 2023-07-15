namespace RadCars.Web.ViewModels.CarImage;

using System.ComponentModel.DataAnnotations;

public class ImageInputModel
{
    [Required]
    public string ImageId { get; set; } = null!;

    [Required]
    public string ListingId { get; set; } = null!;
}