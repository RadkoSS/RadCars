namespace RadCars.Data.Seeding;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Models.Entities;

using static CsvData.BulgariaCitiesCsvReader;

internal class CitiesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (await dbContext.Cities.AnyAsync())
        {
            return;
        }

        await SeedBulgarianCitiesAsync(dbContext);
    }

    private static async Task SeedBulgarianCitiesAsync(ApplicationDbContext dbContext)
    {
        var cities = new HashSet<City>();
        var idOfBulgaria = 1;

        var bulgarianCitiesCsvData = ReadBulgarianCities();

        foreach (var (cityName, (latitude, longitude)) in bulgarianCitiesCsvData)
        {
            var newCity = new City
            {
                Name = cityName,
                Latitude = latitude,
                Longitude = longitude,
                CountryId = idOfBulgaria
            };

            cities.Add(newCity);
        }

        await dbContext.AddRangeAsync(cities);
    }
}