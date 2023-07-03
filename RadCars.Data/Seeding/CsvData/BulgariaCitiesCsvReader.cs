namespace RadCars.Data.Seeding.CsvData;

using System.Globalization;

using CsvHelper;

internal class BulgariaCitiesCsvReader
{
    internal static SortedDictionary<string, (decimal, decimal)> ReadBulgarianCities()
    {
        string currentDir = Environment.CurrentDirectory;

        string fullDirectory = new DirectoryInfo(currentDir).FullName;

        string fullFilePath = fullDirectory + @"\bin\Debug\net6.0\Seeding\CsvData\BulgarianCities.csv";

        using var reader = new StreamReader(fullFilePath);

        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

        var cities = new SortedDictionary<string, (decimal, decimal)>();

        while (csvReader.Read())
        {
            var cityName = csvReader.GetField<string>(0)!;
            var latitude = csvReader.GetField<decimal>(1);
            var longitude = csvReader.GetField<decimal>(2);

            if (!cities.ContainsKey(cityName))
            {
                cities.Add(cityName, (latitude, longitude));
            }
            else
            {
                cities[cityName] = (latitude, longitude);
            }
        }

        return cities;
    }
}