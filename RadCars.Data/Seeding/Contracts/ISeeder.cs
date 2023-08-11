namespace RadCars.Data.Seeding.Contracts;

public interface ISeeder
{
    Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
}