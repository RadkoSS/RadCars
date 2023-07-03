namespace RadCars.Data.Seeding;

using Models.Entities;

using static CsvData.CarMakesModelsCsvReader;

internal static class ModelsSeeder
{
    internal static CarModel[] SeedModels()
    {
        var modelEntities = new HashSet<CarModel>();

        var makesAndModels = ReadMakesAndModels();

        ushort makeId = 1;
        ushort modelId = 1;

        foreach (var (makeName, models) in makesAndModels)
        {
            foreach (var modelName in models)
            {
                var newModel = new CarModel
                {
                    Id = modelId,
                    Name = modelName,
                    CarMakeId = makeId
                };

                modelEntities.Add(newModel);

                modelId++;
            }
            makeId++;
        }

        return modelEntities.ToArray();
    }
}