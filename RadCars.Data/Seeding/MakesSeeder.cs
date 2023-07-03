namespace RadCars.Data.Seeding;

using Models.Entities;

using static CsvData.CarMakesModelsCsvReader;

internal static class MakesSeeder
{
    internal static CarMake[] SeedMakes()
    {
        var csvCarData = ReadMakesAndModels();

        var makes = new HashSet<CarMake>();
        ushort makeId = 1;

        foreach (var (make, models) in csvCarData)
        {
            var makeWithoutModels = new CarMake
            {
                Id = makeId,
                Name = make,
            };

            makes.Add(makeWithoutModels);

            makeId++;
        }

        return makes.ToArray();
    }

}