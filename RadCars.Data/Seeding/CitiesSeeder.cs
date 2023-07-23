namespace RadCars.Data.Seeding;

using Models.Entities;

using static CsvData.BulgariaCitiesCsvReader;

internal class CitiesSeeder
{
    internal static City[] SeedBulgarianCities()
    {
        var cities = new HashSet<City>();
        var idOfBulgaria = 1;
        var cityId = 1;

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