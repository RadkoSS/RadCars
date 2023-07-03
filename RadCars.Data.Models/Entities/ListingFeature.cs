namespace RadCars.Data.Models.Entities;

public class ListingFeature
{
    public Guid ListingId { get; set; }
    public virtual Listing Listing { get; set; } = null!;

    public ushort FeatureId { get; set; }
    public virtual Feature Feature { get; set; } = null!;
}