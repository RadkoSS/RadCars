namespace RadCars.Web.ViewModels.Thumbnail;

using System.ComponentModel.DataAnnotations;

using CarImage;

public class ChooseThumbnailFormModel
{
    public ChooseThumbnailFormModel()
    {
        this.Images = new HashSet<ImageViewModel>();
    }

    [Required]
    public string ListingId { get; set; } = null!;

    [Required(ErrorMessage = "Изберете една от снимките!")]
    public string SelectedImageId { get; set; } = null!;

    public ICollection<ImageViewModel> Images { get; set; }
}