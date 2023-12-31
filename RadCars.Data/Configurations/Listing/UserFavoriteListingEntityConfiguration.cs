﻿namespace RadCars.Data.Configurations.Listing;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Entities;

internal class UserFavoriteListingEntityConfiguration : IEntityTypeConfiguration<UserFavoriteListing>
{
    public void Configure(EntityTypeBuilder<UserFavoriteListing> builder)
    {
        builder
            .HasKey(f => new { f.UserId, f.ListingId });

        builder
            .HasOne(f => f.User)
            .WithMany(u => u.FavoriteListings)
            .HasForeignKey(f => f.UserId);

        builder
            .HasOne(f => f.Listing)
            .WithMany(l => l.Favorites)
            .HasForeignKey(f => f.ListingId);
    }
}