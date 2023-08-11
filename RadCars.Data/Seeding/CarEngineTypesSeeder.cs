namespace RadCars.Data.Seeding;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Models.Entities;

internal class CarEngineTypesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (await dbContext.EngineTypes.AnyAsync())
        {
            return;
        }

        await SeedEngineTypesAsync(dbContext);
    }

    private static async Task SeedEngineTypesAsync(ApplicationDbContext dbContext)
    {
        var engineTypes = new HashSet<EngineType>
        {
            new EngineType
            {
                Name = "Бензин"
            },
            new EngineType
            {
                Name = "Дизел"
            },
            new EngineType
            {
                Name = "Газ / Бензин"
            },
            new EngineType
            {
                Name = "Метан / Бензин"
            },
            new EngineType
            {
                Name = "Електрически"
            },
            new EngineType
            {
                Name = "Хибрид"
            }
        };

        await dbContext.EngineTypes.AddRangeAsync(engineTypes);
    }
}