namespace RadCars.Data.Seeding;

using System;
using System.Reflection;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Contracts;
using Models.User;
using Models.Entities;

using static RadCars.Common.GeneralApplicationConstants;

internal class AuctionsSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (await dbContext.Auctions.AnyAsync())
        {
            return;
        }

        UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Retrieve the service instance using the type - this will be removed from the code once the project gets evaluated. It is just to make the seeding for the Auctions possible. This is overall a bad practice and I know it.
        var serviceType = Assembly.Load("RadCars.Web").GetType("RadCars.Web.BackgroundServices.Contracts.IAuctionBackgroundJobService");

        var backgroundJobService = serviceProvider.GetRequiredService(serviceType!);

        await SeedAuctionsAsync(dbContext, userManager, backgroundJobService, serviceType!);
    }

    private static async Task SeedAuctionsAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, object backgroundJobService, Type serviceType)
    {
        var normalUserCreator = await userManager.FindByEmailAsync(DevelopmentUserEmail);

        if (normalUserCreator == null)
        {
            throw new Exception("Seed users first!");
        }

        // Get the methods for scheduling Auctions using reflection
        MethodInfo scheduleAuctionStartAsyncMethod = serviceType.GetMethod("ScheduleAuctionStart")!;

        MethodInfo scheduleAuctionEndAsyncMethod = serviceType.GetMethod("ScheduleAuctionEnd")!;

        Auction testAuction;
        AuctionCarImage thumbnailImage;

        //First Auction
        testAuction = new Auction
        {
            Id = Guid.Parse("8541825C-2CC8-4F9F-B70C-047F76639EB4"),
            StartTime = DateTime.UtcNow.AddMinutes(5),
            EndTime = DateTime.UtcNow.AddMinutes(20),
            StartingPrice = 40000.00m,
            CurrentPrice = 40000.00m,
            BlitzPrice = null,
            MinimumBid = 2000,
            Title = "ТЪРГ Mercedes-Benz S600 V12 Exclusive Edition",
            Description = @"На Вашето внимание представяме търг за уникална бройка бял Mercedes-Benz S600 с абсолютно пълен пакет екстри в       перфектно състояние и нисък пробег от само 7000 km. 
                Направено е пълно гаранционно обслужване от Mercedes Stuttgart и колата е готова за новия си собственик.",
            PhoneNumber = "0896752305",
            Year = 2018,
            Mileage = 7000,
            EngineModel = "V12 BiTurbo",
            EngineTypeId = 1,
            VinNumber = "WDBGA51E4TA328716",
            Creator = normalUserCreator,
            CreatorId = normalUserCreator.Id,
            CarMakeId = 60,
            CarModelId = 876,
            CityId = 2
        };

        await dbContext.AuctionFeatures.AddRangeAsync(CreateAuctionFeaturesAsync(testAuction));

        thumbnailImage = new AuctionCarImage
        {
            Id = Guid.Parse("83790824-DED7-4C8A-BE7B-8C3B0EFA588A"),
            Auction = testAuction,
            AuctionId = testAuction.Id,
            Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/8541825c-2cc8-4f9f-b70c-047f76639eb4/83790824-ded7-4c8a-be7b-8c3b0efa588a.jpg"
        };

        await dbContext.AuctionCarImages.AddRangeAsync(new HashSet<AuctionCarImage>
        {
            thumbnailImage,
            new AuctionCarImage
            {
                Id = Guid.Parse("DF4B9205-04AB-48C9-97B6-2EF3E12A570E"),
                Auction = testAuction,
                AuctionId = testAuction.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/8541825c-2cc8-4f9f-b70c-047f76639eb4/df4b9205-04ab-48c9-97b6-2ef3e12a570e.jpg"
            },
            new AuctionCarImage
            {
                Id = Guid.Parse("FFE8108F-045B-448B-9B25-A87536782483"),
                Auction = testAuction,
                AuctionId = testAuction.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/8541825c-2cc8-4f9f-b70c-047f76639eb4/ffe8108f-045b-448b-9b25-a87536782483.jpg"
            }
        });

        await dbContext.Auctions.AddAsync(testAuction);

        await dbContext.SaveChangesAsync();

        testAuction.Thumbnail = thumbnailImage;
        testAuction.ThumbnailId = thumbnailImage.Id;

        await dbContext.SaveChangesAsync();

        // Invoke the ScheduleAuctionStart method
        await (Task)scheduleAuctionStartAsyncMethod.Invoke(backgroundJobService, new object[] { testAuction.Id.ToString() })!;

        // Invoke the ScheduleAuctionEnd method
        await (Task)scheduleAuctionEndAsyncMethod.Invoke(backgroundJobService, new object[] { testAuction.Id.ToString() })!;

        //Second Auction
        testAuction = new Auction
        {
            Id = Guid.Parse("450445DE-3CC2-4B55-964E-5097B2B22625"),
            StartTime = DateTime.UtcNow.AddMinutes(8),
            EndTime = DateTime.UtcNow.AddMinutes(37),
            StartingPrice = 5000.00m,
            CurrentPrice = 5000.00m,
            BlitzPrice = 134000.00m,
            MinimumBid = 500,
            Title = "ТЪРГ Mercedes-Benz G500 Yellow Edition",
            Description = @"На Вашето внимание представяме търг за много красив Mercedes G-Class 500 с абсолютно пълен пакет екстри в перфектно състояние и пробег от 70 000 km. 
                Направено е пълно обслужване от оторизиран сервиз (на Ваше разположение е пълната сервизна история на автомобила) е готов да покорява трудно достъпни места с новия си собственик.",
            PhoneNumber = "0896769699",
            Year = 2014,
            Mileage = 70000,
            EngineModel = "V8 BiTurbo",
            EngineTypeId = 1,
            VinNumber = "WDBGA5F45TA328716",
            Creator = normalUserCreator,
            CreatorId = normalUserCreator.Id,
            CarMakeId = 60,
            CarModelId = 860,
            CityId = 3
        };

        await dbContext.AuctionFeatures.AddRangeAsync(CreateAuctionFeaturesAsync(testAuction));

        thumbnailImage = new AuctionCarImage
        {
            Id = Guid.Parse("E36ECE42-6C65-4E5F-B509-CBFE444A2223"),
            Auction = testAuction,
            AuctionId = testAuction.Id,
            Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/450445de-3cc2-4b55-964e-5097b2b22625/e36ece42-6c65-4e5f-b509-cbfe444a2223.jpg"
        };

        await dbContext.AuctionCarImages.AddRangeAsync(new HashSet<AuctionCarImage>
        {
            thumbnailImage,
            new AuctionCarImage
            {
                Id = Guid.Parse("FBDA1B54-337A-4760-B7D6-9B3DAE1AB43D"),
                Auction = testAuction,
                AuctionId = testAuction.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/450445de-3cc2-4b55-964e-5097b2b22625/fbda1b54-337a-4760-b7d6-9b3dae1ab43d.jpg"
            },
            new AuctionCarImage
            {
                Id = Guid.Parse("3205B177-2C51-499F-986F-2FEC3971CF4E"),
                Auction = testAuction,
                AuctionId = testAuction.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/450445de-3cc2-4b55-964e-5097b2b22625/3205b177-2c51-499f-986f-2fec3971cf4e.jpg"
            }
        });

        await dbContext.Auctions.AddAsync(testAuction);

        await dbContext.SaveChangesAsync();

        testAuction.Thumbnail = thumbnailImage;
        testAuction.ThumbnailId = thumbnailImage.Id;

        await dbContext.SaveChangesAsync();

        // Invoke the ScheduleAuctionStart method
        await (Task)scheduleAuctionStartAsyncMethod.Invoke(backgroundJobService, new object[] { testAuction.Id.ToString() })!;

        // Invoke the ScheduleAuctionEnd method
        await (Task)scheduleAuctionEndAsyncMethod.Invoke(backgroundJobService, new object[] { testAuction.Id.ToString() })!;

        //Third Auction
        testAuction = new Auction
        {
            Id = Guid.Parse("4D25C8E9-F1B7-4573-85C7-EB8A626C89E4"),
            StartTime = DateTime.UtcNow.AddMinutes(12),
            EndTime = DateTime.UtcNow.AddMinutes(50),
            StartingPrice = 35000.00m,
            CurrentPrice = 35000.00m,
            BlitzPrice = 130000.00m,
            MinimumBid = 1000,
            Title = "ТЪРГ ЗА ВЕЧНА КЛАСИКА - BMW M5 E28",
            Description = @"На Вашето внимание представяме търг за една невероятна класика - BMW M5 E28 - много красив, бърз автомобил с дизайн, който никога не би омръзнал на новия му собственик.
            Състоянието на автомобила е безупречно. Колекционерска бройка с пълна сервизна история, перфектна и навременна поддръжка и напълно оригинални сменени части по двигателя.
            Няма никакъв тунинг и е почти идентична с момента, в който е излязла от фабриката. Победителят от този търг ще получи не само отличен автомобил, но и късче от автомобилната история.",
            PhoneNumber = "0896799999",
            Year = 1988,
            Mileage = 50000,
            EngineModel = "M88/3",
            EngineTypeId = 1,
            VinNumber = "WDBGA5F45TA696969",
            Creator = normalUserCreator,
            CreatorId = normalUserCreator.Id,
            CarMakeId = 6,
            CarModelId = 124,
            CityId = 19
        };

        await dbContext.AuctionFeatures.AddRangeAsync(CreateAuctionFeaturesAsync(testAuction));

        thumbnailImage = new AuctionCarImage
        {
            Id = Guid.Parse("B582E187-2A69-4F23-A348-3BD98E049B81"),
            Auction = testAuction,
            AuctionId = testAuction.Id,
            Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/4d25c8e9-f1b7-4573-85c7-eb8a626c89e4/b582e187-2a69-4f23-a348-3bd98e049b81.jpg"
        };

        await dbContext.AuctionCarImages.AddRangeAsync(new HashSet<AuctionCarImage>
        {
            thumbnailImage,
            new AuctionCarImage
            {
                Id = Guid.Parse("4A71CEED-353A-4DA9-8082-4824AFC9B38B"),
                Auction = testAuction,
                AuctionId = testAuction.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/4d25c8e9-f1b7-4573-85c7-eb8a626c89e4/4a71ceed-353a-4da9-8082-4824afc9b38b.jpg"
            },
            new AuctionCarImage
            {
                Id = Guid.Parse("A31AF354-65C4-4B73-99E2-76DF0E8C084E"),
                Auction = testAuction,
                AuctionId = testAuction.Id,
                Url = "https://res.cloudinary.com/dyjqdkzt3/image/upload/v1/4d25c8e9-f1b7-4573-85c7-eb8a626c89e4/a31af354-65c4-4b73-99e2-76df0e8c084e.jpg"
            }
        });

        await dbContext.Auctions.AddAsync(testAuction);

        await dbContext.SaveChangesAsync();

        testAuction.Thumbnail = thumbnailImage;
        testAuction.ThumbnailId = thumbnailImage.Id;

        await dbContext.SaveChangesAsync();

        // Invoke the ScheduleAuctionStart method
        await (Task)scheduleAuctionStartAsyncMethod.Invoke(backgroundJobService, new object[] { testAuction.Id.ToString() })!;

        // Invoke the ScheduleAuctionEnd method
        await (Task)scheduleAuctionEndAsyncMethod.Invoke(backgroundJobService, new object[] { testAuction.Id.ToString() })!;
    }

    private static ICollection<AuctionFeature> CreateAuctionFeaturesAsync(Auction auction)
    {
        var features = new HashSet<AuctionFeature>();

        AuctionFeature af;

        //Безопасност
        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 1
        };

        features.Add(af);

        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 2
        };

        features.Add(af);

        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 3
        };

        features.Add(af);

        //Комфорт
        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 15
        };

        features.Add(af);

        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 16
        };

        features.Add(af);

        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 17
        };

        features.Add(af);

        //Защита
        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 38
        };

        features.Add(af);

        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 39
        };

        features.Add(af);

        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 40
        };

        features.Add(af);

        //Платени разходи
        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 42
        };

        features.Add(af);

        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 43
        };

        features.Add(af);

        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 44
        };

        features.Add(af);

        //Вътрешни екстри
        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 47
        };

        features.Add(af);

        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 48
        };

        features.Add(af);

        af = new AuctionFeature
        {
            Auction = auction,
            AuctionId = auction.Id,
            FeatureId = 49
        };

        features.Add(af);

        return features;
    }
}