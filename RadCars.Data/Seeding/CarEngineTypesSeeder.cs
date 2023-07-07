namespace RadCars.Data.Seeding;

using Models.Entities;

internal static class CarEngineTypesSeeder
{
    internal static EngineType[] SeedEngineTypes()
    {
        var engineTypes = new HashSet<EngineType>
        {
            new EngineType
            {
                Id = 1,
                Name = "Бензин"
            },
            new EngineType
            {
                Id = 2,
                Name = "Дизел"
            },
            new EngineType
            {
                Id = 3,
                Name = "Газ / Бензин"
            },
            new EngineType
            {
                Id = 4,
                Name = "Метан / Бензин"
            },
            new EngineType
            {
                Id = 5,
                Name = "Електрически"
            },
            new EngineType
            {
                Id = 6,
                Name = "Хибрид"
            }
        };

        return engineTypes.ToArray();
    }
}