namespace RadCars.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Models.User;
using Models.Entities;
using System.Reflection;

public class RadCarsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public RadCarsDbContext(DbContextOptions<RadCarsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Listing> Listings { get; set; } = null!;

    public DbSet<Make> Makes { get; set; } = null!;

    public DbSet<Model> Models { get; set; } = null!;

    public DbSet<FeatureCategory> FeatureCategories { get; set; } = null!;

    public DbSet<Feature> Features { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(RadCarsDbContext)) ??
                                                Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}