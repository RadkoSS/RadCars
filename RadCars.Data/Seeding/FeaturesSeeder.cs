namespace RadCars.Data.Seeding;

using System;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Models.Entities;

internal class FeaturesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (await dbContext.Features.AnyAsync())
        {
            return;
        }

        await SeedFeaturesAsync(dbContext);
    }

    private static async Task SeedFeaturesAsync(ApplicationDbContext dbContext)
    {
        var features = new HashSet<Feature>();

        Feature feature;

        //Системи за безопасност
        feature = new Feature
        {
            CategoryId = 1,
            Name = "Автоматичен контрол на стабилността"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "ABS - система против блокиране на колелата"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "Предни въздушни възглавници"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "Задни въздушни възглавници"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "Странични въздушни възглавници"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "EBD - разпределяне на спирачното усилие"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "Контрол на налягането на гумите"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "Парктроник"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "Система за защита от пробуксуване"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "Distronic - система за контрол на дистанцията"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "BAS - система за подпомагане на спирането"
        };
        features.Add(feature);
        
        feature = new Feature
        {
            CategoryId = 1,
            Name = "Електронна програма за стабилизиране"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 1,
            Name = "Блокаж на диференциала"
        };
        features.Add(feature);

        //Системи за комфорт
        feature = new Feature
        {
            CategoryId = 2,
            Name = "Старт/Стоп система"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "USB, audio/video, IN/AUX изводи"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Ел. огледала"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Ел. стъкла"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Автоматични чистачки"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Автоматични фарове"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Ел. регулиране на окачването"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Ел. регулиране на седалките"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Ел. усилвател на волана"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Климатик"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Мултифункционален волан"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Навигация"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Подгев на предните седалките"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Подгев на задните седалките"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Регулиране на волана"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Серво усилвател на волана"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Система за измиване на фаровете"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Круиз контрол"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Хладилна жабка"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Безключово палене"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Bluetooth мултимедия"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Stereo уредба"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Ел. багажник"
        };
        features.Add(feature);

        //Системи за защита
        feature = new Feature
        {
            CategoryId = 3,
            Name = "Аларма"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 3,
            Name = "Автокаско"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 3,
            Name = "Централно заключване"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 3,
            Name = "GPS проследяване"
        };
        features.Add(feature);

        //Платени разходи
        feature = new Feature
        {
            CategoryId = 4,
            Name = "Годишен данък"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 4,
            Name = "Гражданска отговорност"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 4,
            Name = "Платен и преминат ГТП"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 4,
            Name = "Обслужени базови консумативи"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 4,
            Name = "Винетка"
        };
        features.Add(feature);

        //Вътрешни екстри
        feature = new Feature
        {
            CategoryId = 5,
            Name = "Кожен салон"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 5,
            Name = "Алкантара салон"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 5,
            Name = "Тапициран таван"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 5,
            Name = "Кожени стелки"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 5,
            Name = "Гумени стелки"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 5,
            Name = "DVD"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 5,
            Name = "Поставки за чаши"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 5,
            Name = "Подлакатник с поставки"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Масаж на предните седалки"
        };
        features.Add(feature);

        feature = new Feature
        {
            CategoryId = 2,
            Name = "Масаж на задните седалки"
        };
        features.Add(feature);

        await dbContext.Features.AddRangeAsync(features);
    }
}