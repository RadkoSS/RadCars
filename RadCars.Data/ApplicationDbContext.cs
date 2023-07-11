namespace RadCars.Data;

using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Models.User;
using Models.Entities;
using Common.Contracts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
        typeof(ApplicationDbContext).GetMethod(
            nameof(SetIsDeletedQueryFilter),
            BindingFlags.NonPublic | BindingFlags.Static)!;

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
        base.OnModelCreating(builder);

        EntityIndexesConfiguration.Configure(builder);

        var entityTypes = builder.Model.GetEntityTypes().ToList();

        // Set global query filter for not deleted entities only
        var deletableEntityTypes = entityTypes
            .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
        foreach (var deletableEntityType in deletableEntityTypes)
        {
            var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
            method.Invoke(null, new object[] { builder });
        }

        // Disable cascade delete
        var foreignKeys = entityTypes
            .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
        foreach (var foreignKey in foreignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }

        builder
            .ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)) ?? Assembly.GetExecutingAssembly());

    }

    public override int SaveChanges() => this.SaveChanges(true);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.ApplyAuditInfoRules();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        this.SaveChangesAsync(true, cancellationToken);

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        this.ApplyAuditInfoRules();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
        where T : class, IDeletableEntity
    {
        builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }

    private void ApplyAuditInfoRules()
    {
        var changedEntries = this.ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is IAuditInfo &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in changedEntries)
        {
            var entity = (IAuditInfo)entry.Entity;
            if (entry.State == EntityState.Added && entity.CreatedOn == default)
            {
                entity.CreatedOn = DateTime.UtcNow;
            }
            else
            {
                entity.ModifiedOn = DateTime.UtcNow;
            }
        }
    }
}