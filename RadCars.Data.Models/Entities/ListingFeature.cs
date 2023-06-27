namespace RadCars.Data.Models.Entities;

public class ListingFeature
{
    public Guid ListingId { get; set; }
    public Listing Listing { get; set; } = null!;

    public ushort FeatureId { get; set; }
    public Feature Feature { get; set; } = null!;
}