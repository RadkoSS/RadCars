namespace RadCars.Data.Models.Entities;

using Common.Contracts;

public class ListingFeature : IDeletableEntity
{
    public Guid ListingId { get; set; }

    public virtual Listing Listing { get; set; } = null!;

    public int FeatureId { get; set; }

    public virtual Feature Feature { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}