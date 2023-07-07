namespace RadCars.Data;

using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Models.User;
using Models.Entities;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Listing> Listings { get; set; } = null!;

    public DbSet<CarMake> CarMakes { get; set; } = null!;

    public DbSet<CarModel> CarModels { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Feature> Features { get; set; } = null!;

    public DbSet<ListingFeature> ListingFeatures { get; set; } = null!;

    public DbSet<UserFavoriteListing> UserFavoriteListings { get; set; } = null!;

    public DbSet<CarImage> CarImages { get; set; } = null!;

    public DbSet<Country> Countries { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<EngineType> EngineTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)) ?? Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}