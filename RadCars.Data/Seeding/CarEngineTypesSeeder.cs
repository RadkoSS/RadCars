namespace RadCars.Data.Seeding;

using Models.Entities;

internal static class CarEngineTypesSeeder
{
    internal static CarEngineType[] SeedEngineTypes()
    {
        var engineTypes = new HashSet<CarEngineType>
        {
            new CarEngineType
            {
                Id = 1,
                Name = "Бензин"
            },
            new CarEngineType
            {
                Id = 2,
                Name = "Дизел"
            },
            new CarEngineType
            {
                Id = 3,
                Name = "Газ / Бензин"
            },
            new CarEngineType
            {
                Id = 4,
                Name = "Метан / Бензин"
            },
            new CarEngineType
            {
                Id = 5,
                Name = "Електрически"
            },
            new CarEngineType
            {
                Id = 6,
                Name = "Хибрид"
            }
        };

        return engineTypes.ToArray();
    }
}