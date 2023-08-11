namespace RadCars.Data.Seeding;

using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Models.Entities;

using static CsvData.CarMakesModelsCsvReader;

internal class ModelsSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (await dbContext.CarModels.AnyAsync())
        {
            return;
        }

        await SeedModelsAsync(dbContext);
    }

    private static async Task SeedModelsAsync(ApplicationDbContext dbContext)
    {
        var modelEntities = new HashSet<CarModel>();

        var makesAndModels = ReadMakesAndModels();

        var makeId = 1;

        foreach (var (makeName, models) in makesAndModels)
        {
            foreach (var modelName in models)
            {
                var newModel = new CarModel
                {
                    Name = modelName,
                    CarMakeId = makeId
                };

                modelEntities.Add(newModel);
            }
            makeId++;
        }

        await dbContext.CarModels.AddRangeAsync(modelEntities);
    }
}