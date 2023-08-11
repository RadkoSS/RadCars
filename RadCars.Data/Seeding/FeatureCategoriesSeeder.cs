namespace RadCars.Data.Seeding;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Models.Entities;

internal class FeatureCategoriesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (await dbContext.Categories.AnyAsync())
        {
            return;
        }

        await SeedFeatureCategoriesAsync(dbContext);
    }

    private static async Task SeedFeatureCategoriesAsync(ApplicationDbContext dbContext)
    {
        var featureCategories = new HashSet<Category>();

        Category category;

        category = new Category
        {
            Name = "Системи за безопасност"
        };
        featureCategories.Add(category);

        category = new Category
        {
            Name = "Системи за комфорт"
        };
        featureCategories.Add(category);

        category = new Category
        {
            Name = "Системи за защита"
        };
        featureCategories.Add(category);

        category = new Category
        {
            Name = "Платени разходи"
        };
        featureCategories.Add(category);

        category = new Category
        {
            Name = "Вътрешни екстри"
        };
        featureCategories.Add(category);

        await dbContext.Categories.AddRangeAsync(featureCategories);
    }
}