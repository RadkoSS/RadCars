namespace RadCars.Data.Seeding;

using Models.Entities;

internal class CountriesSeeder
{
    internal static Country[] SeedCountries()
    {
        var countries = new HashSet<Country>
        {
            new Country
            {
                Id = 1,
                Name = "Bulgaria"
            }
        };

        return countries.ToArray();
    }
}