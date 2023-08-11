namespace RadCars.Data.Seeding;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Contracts;
using Models.User;
using Models.Entities;

using static RadCars.Common.GeneralApplicationConstants;

internal class ListingsSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (await dbContext.Listings.AnyAsync())
        {
            return;
        }

        UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await SeedListingsAsync(dbContext, userManager);
    }

    private static async Task SeedListingsAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        var normalUserCreator = await userManager.FindByEmailAsync(DevelopmentUserEmail);

        if (normalUserCreator == null)
        {
            throw new Exception("Seed users first!");
        }

        Listing testListing;
        CarImage thumbnailImage;

        //First listing

        testListing = new Listing
        {
            Id = Guid.Parse("FA4455F6-AD49-4979-9C91-A67472A67211"),
            CarMakeId = 4,
            CarModelId = 83,
            Title = "Audi RS7 НА ПАКО РАБАНИ!!!",
            Description = @"Най-мощното RS7, наказва ги на старта! Представяме на Вашето внимание уникалното Audi RS7 на Пако Рабани! Колата е с доказан произход, реални километри, поддържана е перфектно от огромен ценител на марката! За сериозните клиенти предлагаме лизинг с одобрение до 20 минути без доказване на доходи!",
            Price = 190000.00m,
            Year = 2019,
            Mileage = 40000,
            EngineModel = "4.0 TFSI V8",
            EngineTypeId = 1,
            VinNumber = "WUAWAAFC6HN903012",
            Creator = normalUserCreator,
            CreatorId = normalUserCreator.Id,
            CityId = 197,
            PhoneNumber = "0886324212"
        };

        await dbContext.ListingFeatures.AddRangeAsync(CreateListingFeaturesAsync(testListing));

        thumbnailImage = new CarImage
        {
            Listing = testListing,
            ListingId = testListing.Id,
            Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689087221/fa4455f6-ad49-4979-9c91-a67472a67211/b4fe9322-700a-49d0-837d-28a73d256803.jpg"
        };

        await dbContext.CarImages.AddRangeAsync(new HashSet<CarImage>
        {
            thumbnailImage,
            new CarImage
            {
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689087220/fa4455f6-ad49-4979-9c91-a67472a67211/7b44b922-f6c8-4522-b0df-6c01f3289ccf.jpg"
            }
        });

        await dbContext.Listings.AddAsync(testListing);
        await dbContext.SaveChangesAsync();

        testListing.Thumbnail = thumbnailImage;
        testListing.ThumbnailId = thumbnailImage.Id;

        await dbContext.SaveChangesAsync();

        //Second Listing

        testListing = new Listing
        {
            Id = Guid.Parse("E8123F87-DA52-4419-A53C-5513AA3B5DCA"),
            CarMakeId = 64,
            CarModelId = 928,
            Title = "Mitsubishi Lancer EVO IX",
            Description = @"Напълно оригинална,
            НЕ МОДИФИЦИРАНА.
            НЕ Е КАРАНА ПО ПИСТИ.
            ПЪЛНА СЕРВИЗНА ИСТОРИЯ
            Като нова. Внос от САЩ. Рядък модел FQ360 с персонален номер 156.
            Обслужена с Motul 300, нов антифриз Barhdal, нови дискове и накладки Brembo, нова спирачна течност. Обработена против ръжда отдолу. Няма удари и пребоядисвани детайли. Два чифта джанти. Едните са с чисто нови семисликове Yokohama.
            Налични са и частите за обръщането й. Цената им е по договаряне.",
            Price = 69999.00m,
            Year = 2007,
            Mileage = 49000,
            EngineModel = "2.0 MIVEC FQ-360",
            EngineTypeId = 1,
            VinNumber = "SCFAC23302B500083",
            Creator = normalUserCreator,
            CreatorId = normalUserCreator.Id,
            CityId = 51,
            PhoneNumber = "0895631307"
        };

        await dbContext.ListingFeatures.AddRangeAsync(CreateListingFeaturesAsync(testListing));

        thumbnailImage = new CarImage
        {
            Id = Guid.Parse("A7F47343-D43F-4D00-93AA-91BC64691F2F"),
            Listing = testListing,
            ListingId = testListing.Id,
            Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689087555/e8123f87-da52-4419-a53c-5513aa3b5dca/a7f47343-d43f-4d00-93aa-91bc64691f2f.jpg"
        };

        await dbContext.CarImages.AddRangeAsync(new HashSet<CarImage>
        {
            thumbnailImage,
            new CarImage
            {
                Id = Guid.Parse("37ABEAE6-3C52-4F16-84A5-3B015FEE742E"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689087554/e8123f87-da52-4419-a53c-5513aa3b5dca/37abeae6-3c52-4f16-84a5-3b015fee742e.jpg"
            },
            new CarImage
            {
                Id = Guid.Parse("CC23DFBE-A76A-43B3-B77E-63706ED61F66"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689087558/e8123f87-da52-4419-a53c-5513aa3b5dca/cc23dfbe-a76a-43b3-b77e-63706ed61f66.jpg"
            },
            new CarImage
            {
                Id = Guid.Parse("5EACF7A2-C6F7-4814-87CA-827F3AE5E3A6"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689087556/e8123f87-da52-4419-a53c-5513aa3b5dca/5eacf7a2-c6f7-4814-87ca-827f3ae5e3a6.jpg"
            },
            new CarImage
            {
                Id = Guid.Parse("E859DE4D-A0C1-4C70-B6F0-EEBC441045D2"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689087557/e8123f87-da52-4419-a53c-5513aa3b5dca/e859de4d-a0c1-4c70-b6f0-eebc441045d2.jpg"
            }
        });

        await dbContext.Listings.AddAsync(testListing);
        await dbContext.SaveChangesAsync();

        testListing.Thumbnail = thumbnailImage;
        testListing.ThumbnailId = thumbnailImage.Id;

        await dbContext.SaveChangesAsync();

        //Third listing

        testListing = new Listing
        {
            Id = Guid.Parse("828B7B4A-8A1D-4B98-9C95-91117313625C"),
            CarMakeId = 4,
            CarModelId = 82,
            Title = "Audi RS6 Avant ПЕРФЕКТНА",
            Description = @"Пълен комплект заводски екстри, от които всички работят безупречно! Рядък екземпляр! Перфектният автомобил за любителите на високите скорости и самоковското злато. В багажника се побират поне 400 кг. компир без проблеми.",
            Price = 199999.99m,
            Year = 2021,
            Mileage = 10000,
            EngineModel = "4.0 TFSI V8",
            EngineTypeId = 1,
            VinNumber = "1GNEL19X73B130926",
            Creator = normalUserCreator,
            CreatorId = normalUserCreator.Id,
            CityId = 150,
            PhoneNumber = "0894231245"
        };

        await dbContext.ListingFeatures.AddRangeAsync(CreateListingFeaturesAsync(testListing));

        thumbnailImage = new CarImage
        {
            Id = Guid.Parse("83EB7F18-5126-40DF-9176-638F6DC248C6"),
            Listing = testListing,
            ListingId = testListing.Id,
            Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689288884/828b7b4a-8a1d-4b98-9c95-91117313625c/83eb7f18-5126-40df-9176-638f6dc248c6.jpg"
        };

        await dbContext.CarImages.AddRangeAsync(new HashSet<CarImage>
        {
            thumbnailImage,
            new CarImage
            {
                Id = Guid.Parse("E857B259-FADC-43AA-92BD-64F4154004DE"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689288885/828b7b4a-8a1d-4b98-9c95-91117313625c/e857b259-fadc-43aa-92bd-64f4154004de.jpg"
            }
        });

        await dbContext.Listings.AddAsync(testListing);
        await dbContext.SaveChangesAsync();

        testListing.Thumbnail = thumbnailImage;
        testListing.ThumbnailId = thumbnailImage.Id;

        await dbContext.SaveChangesAsync();

        //Fourth listing

        testListing = new Listing
        {
            Id = Guid.Parse("0A687AEC-49DA-477C-8453-DE2A54E53F70"),
            CarMakeId = 6,
            CarModelId = 124,
            Title = "BMW M5 E60 Supercharged",
            Description = @"M5 E60 с много доработки по зверския V10!! Автомобилът е качествено направен с най-скъпите и качествени части от топ марки! Гарантирано качество!",
            Price = 54000.69m,
            Year = 2007,
            Mileage = 60000,
            EngineModel = "S85 V10",
            EngineTypeId = 1,
            VinNumber = "WUACCAFC6HN903012",
            Creator = normalUserCreator,
            CreatorId = normalUserCreator.Id,
            CityId = 107,
            PhoneNumber = "0888356750"
        };

        await dbContext.ListingFeatures.AddRangeAsync(CreateListingFeaturesAsync(testListing));

        thumbnailImage = new CarImage
        {
            Id = Guid.Parse("301F201E-B11E-4A1B-A598-DA31B5880720"),
            Listing = testListing,
            ListingId = testListing.Id,
            Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/0a687aec-49da-477c-8453-de2a54e53f70/301f201e-b11e-4a1b-a598-da31b5880720.jpg"
        };

        await dbContext.CarImages.AddRangeAsync(new HashSet<CarImage>
        {
            thumbnailImage,
            new CarImage
            {
                Id = Guid.Parse("20758AAC-C72E-4A13-B29C-11742448A6EF"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/0a687aec-49da-477c-8453-de2a54e53f70/20758aac-c72e-4a13-b29c-11742448a6ef.jpg"
            },
            new CarImage
            {
                Id = Guid.Parse("014D1A55-A4F6-44C6-BE6A-A9BF74EA43F1"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/0a687aec-49da-477c-8453-de2a54e53f70/014d1a55-a4f6-44c6-be6a-a9bf74ea43f1.jpg"
            },
            new CarImage
            {
                Id = Guid.Parse("9203EAA7-E11B-49B5-B5C2-AFC8509E6E93"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/0a687aec-49da-477c-8453-de2a54e53f70/9203eaa7-e11b-49b5-b5c2-afc8509e6e93.jpg"
            },
            new CarImage
            {
                Id = Guid.Parse("627D94AA-3EBF-48A3-AB5D-B0003A69CBEC"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/0a687aec-49da-477c-8453-de2a54e53f70/627d94aa-3ebf-48a3-ab5d-b0003a69cbec.jpg"
            },
            new CarImage
            {
                Id = Guid.Parse("B8493B9D-30DA-45F6-A7C9-E337444A7ECF"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/0a687aec-49da-477c-8453-de2a54e53f70/b8493b9d-30da-45f6-a7c9-e337444a7ecf.jpg"
            },
        });

        await dbContext.Listings.AddAsync(testListing);
        await dbContext.SaveChangesAsync();

        testListing.Thumbnail = thumbnailImage;
        testListing.ThumbnailId = thumbnailImage.Id;

        await dbContext.SaveChangesAsync();

        //Fifth listing

        testListing = new Listing
        {
            Id = Guid.Parse("7BBC07DE-29A1-48F4-AE87-E4302440B311"),
            CarMakeId = 60,
            CarModelId = 850,
            Title = "Mercedes-Benz Е 63 AMG W211",
            Description = @"Перфектно състояние. Обслужена!! Възможен е бартер, за предпочитане джип! Ще помоля да ми звънят и идват на огледи само наистина заинтересовани хора с пари в джоба. Коментари на цената от по 5к лв. не правя!!! И тестови пилоти за снимки и клипове не ми трябват!!! Благодаря!!",
            Price = 28000.00m,
            Year = 2008,
            Mileage = 188000,
            EngineModel = "6.3 AMG V8",
            EngineTypeId = 1,
            VinNumber = "SCFAC23302B500083",
            Creator = normalUserCreator,
            CreatorId = normalUserCreator.Id,
            CityId = 41,
            PhoneNumber = "+359896741602"
        };

        await dbContext.ListingFeatures.AddRangeAsync(CreateListingFeaturesAsync(testListing));

        thumbnailImage = new CarImage
        {
            Id = Guid.Parse("B2A62351-13C5-4AB3-935D-D60BD37034FE"),
            Listing = testListing,
            ListingId = testListing.Id,
            Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689114479/7bbc07de-29a1-48f4-ae87-e4302440b311/b2a62351-13c5-4ab3-935d-d60bd37034fe.jpg"
        };

        await dbContext.CarImages.AddRangeAsync(new HashSet<CarImage>
        {
            thumbnailImage,
            new CarImage
            {
                Id = Guid.Parse("B2744959-22D8-46CF-A80D-49386D0D5706"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689114478/7bbc07de-29a1-48f4-ae87-e4302440b311/b2744959-22d8-46cf-a80d-49386d0d5706.jpg"
            },
            new CarImage
            {
                Id = Guid.Parse("D2874895-E075-4169-BD0E-D20833329565"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1689114477/7bbc07de-29a1-48f4-ae87-e4302440b311/d2874895-e075-4169-bd0e-d20833329565.webp"
            }
        });

        await dbContext.Listings.AddAsync(testListing);
        await dbContext.SaveChangesAsync();

        testListing.Thumbnail = thumbnailImage;
        testListing.ThumbnailId = thumbnailImage.Id;

        await dbContext.SaveChangesAsync();

        // Sixth listing

        testListing = new Listing
        {
            Id = Guid.Parse("5588B2E2-948C-40EC-8F99-EC6A9E37EE30"),
            CarMakeId = 70,
            CarModelId = 1053,
            Title = "Porsche 991 C4 GTS",
            Description = @"Автомобилът разполага с всички екстри за модела и е поддържан много добре в по време на своята кратка експлоатация. Ще получите пълна сервизна история и прекрасно возило, с което да впечатлявате всички мадами.",
            Price = 321000.00m,
            Year = 2021,
            Mileage = 11533,
            EngineModel = "3.8 (430 кс) PDK",
            EngineTypeId = 1,
            VinNumber = "WP0AA29995S085706",
            Creator = normalUserCreator,
            CreatorId = normalUserCreator.Id,
            CityId = 201,
            PhoneNumber = "0896969696"
        };

        await dbContext.ListingFeatures.AddRangeAsync(CreateListingFeaturesAsync(testListing));

        thumbnailImage = new CarImage
        {
            Id = Guid.Parse("6B34BF9A-8386-45FF-B30F-0FDB5FD4D95C"),
            Listing = testListing,
            ListingId = testListing.Id,
            Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/5588b2e2-948c-40ec-8f99-ec6a9e37ee30/6b34bf9a-8386-45ff-b30f-0fdb5fd4d95c.jpg"
        };

        await dbContext.CarImages.AddRangeAsync(new HashSet<CarImage>
        {
            thumbnailImage,
            new CarImage
            {
                Id = Guid.Parse("F09A579D-6FE8-4C20-AF93-0B2C52904AE5"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/5588b2e2-948c-40ec-8f99-ec6a9e37ee30/f09a579d-6fe8-4c20-af93-0b2c52904ae5.jpg"
            },
            new CarImage
            {
                Id = Guid.Parse("5192CB43-9183-4A36-898C-46018D22093C"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/5588b2e2-948c-40ec-8f99-ec6a9e37ee30/5192cb43-9183-4a36-898c-46018d22093c.jpg"
            },
            new CarImage
            {
                Id = Guid.Parse("C94F9608-4894-4496-9070-F17E1B187A03"),
                Listing = testListing,
                ListingId = testListing.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/5588b2e2-948c-40ec-8f99-ec6a9e37ee30/c94f9608-4894-4496-9070-f17e1b187a03.jpg"
            }
        });

        await dbContext.Listings.AddAsync(testListing);
        await dbContext.SaveChangesAsync();

        testListing.Thumbnail = thumbnailImage;
        testListing.ThumbnailId = thumbnailImage.Id;

        await dbContext.SaveChangesAsync();
    }

    private static ICollection<ListingFeature> CreateListingFeaturesAsync(Listing listing)
    {
        var features = new HashSet<ListingFeature>();

        ListingFeature lf;

        //Безопасност
        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 1
        };

        features.Add(lf);

        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 2
        };

        features.Add(lf);

        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 3
        };

        features.Add(lf);

        //Комфорт
        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 15
        };

        features.Add(lf);

        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 16
        };

        features.Add(lf);

        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 17
        };

        features.Add(lf);

        //Защита
        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 38
        };

        features.Add(lf);

        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 39
        };

        features.Add(lf);

        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 40
        };

        features.Add(lf);

        //Платени разходи
        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 42
        };

        features.Add(lf);

        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 43
        };

        features.Add(lf);

        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 44
        };

        features.Add(lf);

        //Вътрешни екстри
        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 47
        };

        features.Add(lf);

        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 48
        };

        features.Add(lf);

        lf = new ListingFeature
        {
            Listing = listing,
            ListingId = listing.Id,
            FeatureId = 49
        };

        features.Add(lf);

        return features;
    }
}