namespace RadCars.Data.Seeding;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Models.Entities;

using static CsvData.CarMakesModelsCsvReader;

internal class MakesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (await dbContext.CarMakes.AnyAsync())
        {
            return;
        }

        await SeedMakesAsync(dbContext);
    }

    private static async Task SeedMakesAsync(ApplicationDbContext dbContext)
    {
        var csvCarData = ReadMakesAndModels();

        var makes = new HashSet<CarMake>();

        foreach (var (make, models) in csvCarData)
        {
            var makeWithoutModels = new CarMake
            {
                Name = make,
            };

            makes.Add(makeWithoutModels);
        }

        await dbContext.CarMakes.AddRangeAsync(makes);
    }
}