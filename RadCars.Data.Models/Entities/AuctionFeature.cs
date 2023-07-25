namespace RadCars.Data.Models.Entities;

using System;

using Common.Contracts;

public class AuctionFeature : IDeletableEntity
{
    public Guid AuctionId { get; set; }

    public virtual Auction Auction { get; set; } = null!;

    public int FeatureId { get; set; }

    public virtual Feature Feature { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}