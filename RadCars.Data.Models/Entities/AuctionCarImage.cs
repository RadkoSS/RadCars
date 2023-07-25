namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using Common.Models;

public class AuctionCarImage : BaseDeletableModel<Guid>
{
    public AuctionCarImage()
    {
        this.Id = Guid.NewGuid();
    }

    [Required]
    public string Url { get; set; } = null!;

    [Required]
    public Guid AuctionId { get; set; }

    public virtual Auction Auction { get; set; } = null!;
}