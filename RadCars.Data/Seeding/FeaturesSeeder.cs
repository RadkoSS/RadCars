namespace RadCars.Data.Seeding;

using Models.Entities;

internal static class FeaturesSeeder
{
    internal static Feature[] SeedFeatures()
    {
        var features = new HashSet<Feature>();

        Feature feature;

        //Системи за безопасност
        feature = new Feature
        {
            Id = 1,
            CategoryId = 1,
            Name = "Автоматичен контрол на стабилността"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 2,
            CategoryId = 1,
            Name = "ABS - система против блокиране на колелата"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 3,
            CategoryId = 1,
            Name = "Предни въздушни възглавници"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 4,
            CategoryId = 1,
            Name = "Задни въздушни възглавници"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 5,
            CategoryId = 1,
            Name = "Странични въздушни възглавници"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 6,
            CategoryId = 1,
            Name = "EBD - разпределяне на спирачното усилие"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 7,
            CategoryId = 1,
            Name = "Контрол на налягането на гумите"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 8,
            CategoryId = 1,
            Name = "Парктроник"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 9,
            CategoryId = 1,
            Name = "Система за защита от пробуксуване"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 10,
            CategoryId = 1,
            Name = "Distronic - система за контрол на дистанцията"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 11,
            CategoryId = 1,
            Name = "BAS - система за подпомагане на спирането"
        };
        features.Add(feature);
        
        feature = new Feature
        {
            Id = 12,
            CategoryId = 1,
            Name = "Електронна програма за стабилизиране"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 13,
            CategoryId = 1,
            Name = "Блокаж на диференциала"
        };
        features.Add(feature);

        //Системи за комфорт
        feature = new Feature
        {
            Id = 14,
            CategoryId = 2,
            Name = "Старт/Стоп система"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 15,
            CategoryId = 2,
            Name = "USB, audio/video, IN/AUX изводи"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 16,
            CategoryId = 2,
            Name = "Ел. огледала"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 17,
            CategoryId = 2,
            Name = "Ел. стъкла"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 18,
            CategoryId = 2,
            Name = "Автоматични чистачки"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 19,
            CategoryId = 2,
            Name = "Автоматични фарове"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 20,
            CategoryId = 2,
            Name = "Ел. регулиране на окачването"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 21,
            CategoryId = 2,
            Name = "Ел. регулиране на седалките"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 22,
            CategoryId = 2,
            Name = "Ел. усилвател на волана"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 23,
            CategoryId = 2,
            Name = "Климатик"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 24,
            CategoryId = 2,
            Name = "Мултифункционален волан"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 25,
            CategoryId = 2,
            Name = "Навигация"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 26,
            CategoryId = 2,
            Name = "Подгев на предните седалките"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 27,
            CategoryId = 2,
            Name = "Подгев на задните седалките"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 28,
            CategoryId = 2,
            Name = "Регулиране на волана"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 29,
            CategoryId = 2,
            Name = "Серво усилвател на волана"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 30,
            CategoryId = 2,
            Name = "Система за измиване на фаровете"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 31,
            CategoryId = 2,
            Name = "Круиз контрол"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 32,
            CategoryId = 2,
            Name = "Хладилна жабка"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 33,
            CategoryId = 2,
            Name = "Безключово палене"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 34,
            CategoryId = 2,
            Name = "Bluetooth мултимедия"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 35,
            CategoryId = 2,
            Name = "Stereo уредба"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 40,
            CategoryId = 2,
            Name = "Ел. багажник"
        };
        features.Add(feature);

        //Системи за защита
        feature = new Feature
        {
            Id = 36,
            CategoryId = 3,
            Name = "Аларма"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 37,
            CategoryId = 3,
            Name = "Автокаско"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 38,
            CategoryId = 3,
            Name = "Централно заключване"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 39,
            CategoryId = 3,
            Name = "GPS проследяване"
        };
        features.Add(feature);

        //Платени разходи
        feature = new Feature
        {
            Id = 41,
            CategoryId = 4,
            Name = "Годишен данък"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 42,
            CategoryId = 4,
            Name = "Гражданска отговорност"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 43,
            CategoryId = 4,
            Name = "Платен и преминат ГТП"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 44,
            CategoryId = 4,
            Name = "Обслужени базови консумативи"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 45,
            CategoryId = 4,
            Name = "Винетка"
        };
        features.Add(feature);

        //Вътрешни екстри
        feature = new Feature
        {
            Id = 46,
            CategoryId = 5,
            Name = "Кожен салон"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 47,
            CategoryId = 5,
            Name = "Алкантара салон"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 48,
            CategoryId = 5,
            Name = "Тапициран таван"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 49,
            CategoryId = 5,
            Name = "Кожени стелки"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 50,
            CategoryId = 5,
            Name = "Гумени стелки"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 51,
            CategoryId = 5,
            Name = "DVD"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 52,
            CategoryId = 5,
            Name = "Поставки за чаши"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 53,
            CategoryId = 5,
            Name = "Подлакатник с поставки"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 54,
            CategoryId = 2,
            Name = "Масаж на предните седалки"
        };
        features.Add(feature);

        feature = new Feature
        {
            Id = 55,
            CategoryId = 2,
            Name = "Масаж на задните седалки"
        };
        features.Add(feature);

        return features.ToArray();
    }
}