namespace RadCars.Data.Seeding;

using Models.Entities;

internal static class CarEngineTypesSeeder
{
    internal static EngineTypes[] SeedEngineTypes()
    {
        var engineTypes = new HashSet<EngineTypes>
        {
            new EngineTypes
            {
                Id = 1,
                Name = "Бензин"
            },
            new EngineTypes
            {
                Id = 2,
                Name = "Дизел"
            },
            new EngineTypes
            {
                Id = 3,
                Name = "Газ / Бензин"
            },
            new EngineTypes
            {
                Id = 4,
                Name = "Метан / Бензин"
            },
            new EngineTypes
            {
                Id = 5,
                Name = "Електрически"
            },
            new EngineTypes
            {
                Id = 6,
                Name = "Хибрид"
            }
        };

        return engineTypes.ToArray();
    }
}