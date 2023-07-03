namespace RadCars.Data.Seeding.CsvData;

using System.Globalization;

using CsvHelper;

internal static class CarMakeModelCsvReader
{
    internal static SortedDictionary<string, HashSet<string>> ReadMakesAndModels()
    {
        string currentDir = Environment.CurrentDirectory;

        string fullDirectory = new DirectoryInfo(currentDir).FullName;

        string fullFilePath = fullDirectory + @"\bin\Debug\net6.0\Seeding\CsvData\MakesModelsDb.csv";

        using var reader = new StreamReader(fullFilePath);

        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

        var makesAndModels = new SortedDictionary<string, HashSet<string>>();

        while (csvReader.Read())
        {
            var line = csvReader.GetField<string>(0)!.Split(';', StringSplitOptions.RemoveEmptyEntries);
            var make = line[0];
            var model = line[1];

            if (!makesAndModels.ContainsKey(make))
            {
                makesAndModels[make] = new HashSet<string>
                {
                    model
                };
            }
            else
            {
                makesAndModels[make].Add(model);
            }
        }

        return makesAndModels;
    }
}