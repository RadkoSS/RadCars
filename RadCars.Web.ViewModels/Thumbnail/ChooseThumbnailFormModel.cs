namespace RadCars.Web.ViewModels.Thumbnail;

using System.ComponentModel.DataAnnotations;

using CarImage;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class ChooseThumbnailFormModel : IMapFrom<Listing>, IMapFrom<Auction>
{
    public ChooseThumbnailFormModel()
    {
        this.Images = new HashSet<ImageViewModel>();
    }

    [Required]
    public string Id { get; set; } = null!;

    [Required(ErrorMessage = "Изберете една от снимките!")]
    public string ThumbnailId { get; set; } = null!;

    public ICollection<ImageViewModel> Images { get; set; }
}