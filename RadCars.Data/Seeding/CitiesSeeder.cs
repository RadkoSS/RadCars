namespace RadCars.Data.Seeding;

using Models.Entities;

using static CsvData.BulgariaCitiesCsvReader;

internal class CitiesSeeder
{
    internal static City[] SeedCities()
    {
        var cities = new HashSet<City>();
        ushort idOfBulgaria = 1;
        ushort cityId = 1;

        var bulgarianCitiesCsvData = ReadBulgarianCities();

        foreach (var (cityName, (latitude, longitude)) in bulgarianCitiesCsvData)
        {
            var newCity = new City
            {
                Id = cityId,
                Name = cityName,
                Latitude = latitude,
                Longitude = longitude,
                CountryId = idOfBulgaria
            };

            cities.Add(newCity);
            cityId++;
        }

        return cities.ToArray();
    }
}