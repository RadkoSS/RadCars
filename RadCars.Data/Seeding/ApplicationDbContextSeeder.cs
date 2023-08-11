namespace RadCars.Data.Seeding;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Contracts;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContextSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        if (serviceProvider == null)
        {
            throw new ArgumentNullException(nameof(serviceProvider));
        }

        //If there is any data in the Database, seeding is skipped for everything!
        if (await dbContext.CarMakes.AnyAsync())
        {
            return;
        }

        var logger = serviceProvider.GetService<ILoggerFactory>()!.CreateLogger(typeof(ApplicationDbContextSeeder));

        var seeders = new HashSet<ISeeder>
        {
            new RolesSeeder(),
            new UsersSeeder(),
            new CarEngineTypesSeeder(),
            new CountriesSeeder(),
            new CitiesSeeder(),
            new FeatureCategoriesSeeder(),
            new FeaturesSeeder(),
            new MakesSeeder(),
            new ModelsSeeder(),
            new ListingsSeeder(),
            new AuctionsSeeder()
        };

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(dbContext, serviceProvider);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
        }
    }
}