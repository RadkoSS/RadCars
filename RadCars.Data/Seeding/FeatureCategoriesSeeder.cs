namespace RadCars.Data.Seeding;

using Models.Entities;

internal static class FeatureCategoriesSeeder
{
    internal static Category[] SeedFeatureCategories()
    {
        var featureCategories = new HashSet<Category>();

        Category category;

        category = new Category
        {
            Id = 1,
            Name = "Системи за безопасност"
        };
        featureCategories.Add(category);

        category = new Category
        {
            Id = 2,
            Name = "Системи за комфорт"
        };
        featureCategories.Add(category);

        category = new Category
        {
            Id = 3,
            Name = "Системи за защита"
        };
        featureCategories.Add(category);

        category = new Category
        {
            Id = 4,
            Name = "Платени разходи"
        };
        featureCategories.Add(category);

        category = new Category
        {
            Id = 5,
            Name = "Вътрешни екстри"
        };
        featureCategories.Add(category);

        return featureCategories.ToArray();
    }
}