namespace RadCars.Data.Seeding;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Models.Entities;

internal class CountriesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (await dbContext.Countries.AnyAsync())
        {
            return;
        }

        await SeedCountriesAsync(dbContext);
    }

    private static async Task SeedCountriesAsync(ApplicationDbContext dbContext)
    {
        var countries = new HashSet<Country>
        {
            new Country
            {
                Name = "Bulgaria"
            }
        };

        await dbContext.Countries.AddRangeAsync(countries);
    }
}