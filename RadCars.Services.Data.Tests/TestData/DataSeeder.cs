namespace RadCars.Services.Data.Tests.TestData;

using RadCars.Data.Models.User;
using RadCars.Data.Models.Entities;

public static class DataSeeder
{
    public static class ApplicationUsersSeeder
    {
        public static ApplicationUser[] GetApplicationUsers()
        {
            return new ApplicationUser[]
            {
                new ApplicationUser
                {
                    Id = Guid.Parse("645b548c-3c1e-4712-8187-0c80b2dd1db3"),
                    FirstName = "Mnogo tegav test",
                    LastName = "Testov",
                    PhoneNumber = "0896543212",
                    UserName = "TegavTest",
                    Email = "test@tegav.bg"
                },
                new ApplicationUser
                {
                    Id = Guid.Parse("fce6bd25-cd68-449f-94fc-6297237f8b0e"),
                    FirstName = "Po-lesen test",
                    LastName = "Testovski",
                    PhoneNumber = "0896543269",
                    UserName = "PoLesenTest",
                    Email = "test@lesen.bg"
                }
            };
        }
    }

    public static class AuctionsSeeder
    {
        public static Auction[] GetAuctions()
        {
            return new Auction[]
            {
                new Auction
                {
                    Id = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154"),
                    CreatedOn = DateTime.UtcNow.AddSeconds(new Random().Next(100)),
                    IsDeleted = false,
                    Title = "Testtttttttt",
                    Description = "Test123434254345345666363441231235",
                    PhoneNumber = "0895335678",
                    StartTime = DateTime.UtcNow.AddMinutes(new Random().Next(30, 300)),
                    EndTime = DateTime.UtcNow.AddHours(new Random().Next(6, 24)),
                    StartAuctionJobId = "1",
                    EndAuctionJobId = "2",
                    CurrentPrice = 15000,
                    BlitzPrice = 35000,
                    MinimumBid = 500,
                    CarMakeId = 1,
                    CarMake = new CarMake
                    {
                        Id = 1,
                        Name = "Audi"
                    },
                    CarModelId = 1,
                    CarModel = new CarModel
                    {
                        Id = 1,
                        Name = "A6",
                        CarMakeId = 1
                    },
                    StartingPrice = 15000,
                    Year = 2016,
                    Mileage = 100000,
                    EngineModel = "3.0TDI",
                    EngineTypeId = 1,
                    EngineType = new EngineType
                    {
                        Id = 1,
                        Name = "Дизел"
                    },
                    VinNumber = "WVGBV75N19W507096",
                    Creator = ApplicationUsersSeeder.GetApplicationUsers().First(),
                    CreatorId = ApplicationUsersSeeder.GetApplicationUsers().First().Id,
                    CityId = 1,
                    City = new City
                    {
                        Id = 1,
                        Name = "Pernik"
                    },
                    AuctionFeatures = new HashSet<AuctionFeature>
                    {
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 1,
                                Name = "Test",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 2,
                                Name = "Test2",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 3,
                                Name = "Test3",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 4,
                                Name = "Test4",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        }
                    },
                    Images = new HashSet<AuctionCarImage>
                    {
                        new AuctionCarImage
                        {
                            Id = Guid.Parse("13344399-b6e3-429d-a923-4a4e9cee9e4d"),
                            Url = "De da znam",
                            AuctionId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154")
                        },
                        new AuctionCarImage
                        {
                            Id = Guid.Parse("e4ab390b-5302-4d5c-b55f-ffc48f6aee82"),
                            Url = "De znam5",
                            AuctionId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154")
                        }
                    },
                    ThumbnailId = Guid.Parse("13344399-b6e3-429d-a923-4a4e9cee9e4d"),
                    Thumbnail = new AuctionCarImage
                    {
                        Id = Guid.Parse("13344399-b6e3-429d-a923-4a4e9cee9e4d"),
                        Url = "De da znam",
                        AuctionId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154")
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789"),
                    CreatedOn = DateTime.UtcNow.AddSeconds(new Random().Next(100)),
                    IsDeleted = false,
                    Title = "Testtttttttt2",
                    Description = "Test17868786868sdfsdf",
                    PhoneNumber = "0895995999",
                    StartTime = DateTime.UtcNow.AddMinutes(new Random().Next(30, 300)),
                    EndTime = DateTime.UtcNow.AddHours(new Random().Next(6, 24)),
                    StartAuctionJobId = "3",
                    EndAuctionJobId = "4",
                    CurrentPrice = 20000,
                    BlitzPrice = 90000,
                    MinimumBid = 700,
                    CarMakeId = 1,
                    CarMake = new CarMake
                    {
                        Id = 1,
                        Name = "Audi"
                    },
                    CarModelId = 2,
                    CarModel = new CarModel
                    {
                        Id = 2,
                        Name = "RS6",
                        CarMakeId = 1
                    },
                    StartingPrice = 20000,
                    Year = 2015,
                    Mileage = 700000,
                    EngineModel = "4.2 TFSI V8",
                    EngineTypeId = 2,
                    EngineType = new EngineType
                    {
                        Id = 2,
                        Name = "Бензин"
                    },
                    VinNumber = "FVCBV75N19W507096",
                    Creator = ApplicationUsersSeeder.GetApplicationUsers().First(),
                    CreatorId = ApplicationUsersSeeder.GetApplicationUsers().First().Id,
                    CityId = 1,
                    City = new City
                    {
                        Id = 1,
                        Name = "Pernik"
                    },
                    AuctionFeatures = new HashSet<AuctionFeature>
                    {
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 1,
                                Name = "Test",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 2,
                                Name = "Test2",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 3,
                                Name = "Test3",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 4,
                                Name = "Test4",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        }
                    },
                    Images = new HashSet<AuctionCarImage>
                    {
                        new AuctionCarImage
                        {
                            Id = Guid.Parse("3a767678-beab-4554-b7d5-22cd430e90dc"),
                            Url = "De da znam2",
                            AuctionId = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789")
                        }
                    },
                    ThumbnailId = Guid.Parse("13344399-b6e3-429d-a923-4a4e9cee9e4d"),
                    Thumbnail = new AuctionCarImage
                    {
                        Id = Guid.Parse("13344399-b6e3-429d-a923-4a4e9cee9e4d"),
                        Url = "De da znam2",
                        AuctionId = Guid.Parse("3a767678-beab-4554-b7d5-22cd430e90dc")
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57"),
                    CreatedOn = DateTime.UtcNow.AddSeconds(new Random().Next(100)),
                    IsDeleted = false,
                    Title = "Testtttttttt3",
                    Description = "Test178687fscasdasdqade1`23",
                    PhoneNumber = "0895998888",
                    StartTime = DateTime.UtcNow.AddMinutes(new Random().Next(30, 300)),
                    EndTime = DateTime.UtcNow.AddHours(new Random().Next(6, 24)),
                    StartAuctionJobId = "5",
                    EndAuctionJobId = "6",
                    CurrentPrice = 80000,
                    BlitzPrice = 160000,
                    MinimumBid = 1000,
                    CarMakeId = 1,
                    CarMake = new CarMake
                    {
                        Id = 1,
                        Name = "Audi"
                    },
                    CarModelId = 3,
                    CarModel = new CarModel
                    {
                        Id = 3,
                        Name = "RS7",
                        CarMakeId = 1
                    },
                    StartingPrice = 80000,
                    Year = 2016,
                    Mileage = 800000,
                    EngineModel = "4.2 TFSI V8",
                    EngineTypeId = 2,
                    EngineType = new EngineType
                    {
                        Id = 2,
                        Name = "Бензин"
                    },
                    VinNumber = "FVCBV75N19W507096",
                    Creator = ApplicationUsersSeeder.GetApplicationUsers().First(),
                    CreatorId = ApplicationUsersSeeder.GetApplicationUsers().First().Id,
                    CityId = 1,
                    City = new City
                    {
                        Id = 1,
                        Name = "Pernik"
                    },
                    AuctionFeatures = new HashSet<AuctionFeature>
                    {
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 1,
                                Name = "Test",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 2,
                                Name = "Test2",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 3,
                                Name = "Test3",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 4,
                                Name = "Test4",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        }
                    },
                    Images = new HashSet<AuctionCarImage>
                    {
                        new AuctionCarImage
                        {
                            Id = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                            Url = "De da znam3",
                            AuctionId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57")
                        }
                    },
                    ThumbnailId = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                    Thumbnail = new AuctionCarImage
                    {
                        Id = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                        Url = "De da znam3",
                        AuctionId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57")
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("5bc6e47d-cdea-44f7-8114-c01c2d3f3e7c"),
                    CreatedOn = DateTime.UtcNow.AddSeconds(new Random().Next(100)),
                    IsDeleted = true,
                    Title = "Testtttttttt4",
                    Description = "Test178687fscasasdsada`2233",
                    PhoneNumber = "08959986666",
                    StartTime = DateTime.UtcNow.AddMinutes(new Random().Next(30, 300)),
                    EndTime = DateTime.UtcNow.AddHours(new Random().Next(6, 24)),
                    StartAuctionJobId = "7",
                    EndAuctionJobId = "8",
                    CurrentPrice = 30000,
                    BlitzPrice = 135000,
                    MinimumBid = 1500,
                    CarMakeId = 2,
                    CarMake = new CarMake
                    {
                        Id = 2,
                        Name = "BMW"
                    },
                    CarModelId = 4,
                    CarModel = new CarModel
                    {
                        Id = 4,
                        Name = "M3",
                        CarMakeId = 2
                    },
                    StartingPrice = 30000,
                    Year = 2022,
                    Mileage = 1000,
                    EngineModel = "Twin-Turbo 3.0 I6",
                    EngineTypeId = 2,
                    EngineType = new EngineType
                    {
                        Id = 2,
                        Name = "Бензин"
                    },
                    VinNumber = "FFCBV75N19W507096",
                    Creator = ApplicationUsersSeeder.GetApplicationUsers()[1],
                    CreatorId = ApplicationUsersSeeder.GetApplicationUsers()[1].Id,
                    CityId = 2,
                    City = new City
                    {
                        Id = 2,
                        Name = "Sofia"
                    },
                    AuctionFeatures = new HashSet<AuctionFeature>
                    {
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("5bc6e47d-cdea-44f7-8114-c01c2d3f3e7c"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 1,
                                Name = "Test",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("5bc6e47d-cdea-44f7-8114-c01c2d3f3e7c"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 2,
                                Name = "Test2",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("5bc6e47d-cdea-44f7-8114-c01c2d3f3e7c"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 3,
                                Name = "Test3",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new AuctionFeature
                        {
                            AuctionId = Guid.Parse("5bc6e47d-cdea-44f7-8114-c01c2d3f3e7c"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 4,
                                Name = "Test4",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        }
                    },
                    Images = new HashSet<AuctionCarImage>
                    {
                        new AuctionCarImage
                        {
                            Id = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                            Url = "De da znam4",
                            AuctionId = Guid.Parse("98386ed7-71db-4665-b127-7765741ff06e")
                        }
                    },
                    ThumbnailId = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                    Thumbnail = new AuctionCarImage
                    {
                        Id = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                        Url = "De da znam4",
                        AuctionId = Guid.Parse("98386ed7-71db-4665-b127-7765741ff06e")
                    }
                },
            };
        }

        public static AuctionCarImage[] GetAuctionCarImages()
        {
            return new AuctionCarImage[]
            {
                new AuctionCarImage
                {
                    Id = GetAuctions().First().Images.First().Id,
                    Url = "De da znam",
                    AuctionId = GetAuctions().First().Id,
                    Auction = GetAuctions().First()
                },
                new AuctionCarImage
                {
                    Id = GetAuctions()[1].Images.First().Id,
                    Url = "De da znam2",
                    AuctionId = GetAuctions()[1].Id,
                    Auction = GetAuctions()[1]
                },
                new AuctionCarImage
                {
                    Id = GetAuctions()[2].Images.First().Id,
                    Url = "De da znam3",
                    AuctionId = GetAuctions()[2].Id,
                    Auction = GetAuctions()[2]
                },
                new AuctionCarImage
                {
                    Id = GetAuctions()[3].Images.First().Id,
                    Url = "De da znam4",
                    AuctionId = GetAuctions()[3].Id,
                    Auction = GetAuctions()[3]
                },
                new AuctionCarImage
                {
                    Id = Guid.Parse("e4ab390b-5302-4d5c-b55f-ffc48f6aee82"),
                    Url = "De znam5",
                    AuctionId = GetAuctions()[0].Id,
                    Auction = GetAuctions()[0]
                }
            };
        }

        public static UserFavoriteAuction[] GetUserFavoriteAuctions()
        {
            return new UserFavoriteAuction[]
            {
                new UserFavoriteAuction
                {
                    User = ApplicationUsersSeeder.GetApplicationUsers()[1],
                    UserId = ApplicationUsersSeeder.GetApplicationUsers()[1].Id,
                    Auction = GetAuctions().First(),
                    AuctionId = GetAuctions().First().Id
                },
                new UserFavoriteAuction
                {
                    User = ApplicationUsersSeeder.GetApplicationUsers()[1],
                    UserId = ApplicationUsersSeeder.GetApplicationUsers()[1].Id,
                    Auction = GetAuctions()[1],
                    AuctionId = GetAuctions()[1].Id
                }
            };
        }
    }

    public static class ListingsSeeder
    {
        public static Listing[] GetListings()
        {
            return new Listing[]
            {
                new Listing
                {
                    Id = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154"),
                    CreatedOn = DateTime.UtcNow.AddSeconds(new Random().Next(100)),
                    IsDeleted = false,
                    Title = "Testtttttttt",
                    Description = "Test123434254345345666363441231235",
                    PhoneNumber = "0895335678",
                    CarMakeId = 1,
                    CarMake = new CarMake
                    {
                        Id = 1,
                        Name = "Audi"
                    },
                    CarModelId = 1,
                    CarModel = new CarModel
                    {
                        Id = 1,
                        Name = "A6",
                        CarMakeId = 1
                    },
                    Price = 15000,
                    Year = 2016,
                    Mileage = 100000,
                    EngineModel = "3.0TDI",
                    EngineTypeId = 1,
                    EngineType = new EngineType
                    {
                        Id = 1,
                        Name = "Дизел"
                    },
                    VinNumber = "WVGBV75N19W507096",
                    Creator = ApplicationUsersSeeder.GetApplicationUsers().First(),
                    CreatorId = ApplicationUsersSeeder.GetApplicationUsers().First().Id,
                    CityId = 1,
                    City = new City
                    {
                        Id = 1,
                        Name = "Pernik"
                    },
                    ListingFeatures = new HashSet<ListingFeature>
                    {
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 1,
                                Name = "Test",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 2,
                                Name = "Test2",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 3,
                                Name = "Test3",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 4,
                                Name = "Test4",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        }
                    },
                    Images = new HashSet<CarImage>
                    {
                        new CarImage
                        {
                            Id = Guid.Parse("13344399-b6e3-429d-a923-4a4e9cee9e4d"),
                            Url = "De da znam",
                            ListingId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154")
                        },
                        new CarImage
                        {
                            Id = Guid.Parse("e4ab390b-5302-4d5c-b55f-ffc48f6aee82"),
                            Url = "De znam5",
                            ListingId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154")
                        }
                    },
                    ThumbnailId = Guid.Parse("13344399-b6e3-429d-a923-4a4e9cee9e4d"),
                    Thumbnail = new CarImage
                    {
                        Id = Guid.Parse("13344399-b6e3-429d-a923-4a4e9cee9e4d"),
                        Url = "De da znam",
                        ListingId = Guid.Parse("1adde4a7-4da1-47de-8660-ea7dd90d1154")
                    }
                },
                new Listing
                {
                    Id = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789"),
                    CreatedOn = DateTime.UtcNow.AddSeconds(new Random().Next(100)),
                    IsDeleted = false,
                    Title = "Testtttttttt2",
                    Description = "Test17868786868sdfsdf",
                    PhoneNumber = "0895995999",
                    CarMakeId = 1,
                    CarMake = new CarMake
                    {
                        Id = 1,
                        Name = "Audi"
                    },
                    CarModelId = 2,
                    CarModel = new CarModel
                    {
                        Id = 2,
                        Name = "RS6",
                        CarMakeId = 1
                    },
                    Price = 90000,
                    Year = 2015,
                    Mileage = 700000,
                    EngineModel = "4.2 TFSI V8",
                    EngineTypeId = 2,
                    EngineType = new EngineType
                    {
                        Id = 2,
                        Name = "Бензин"
                    },
                    VinNumber = "FVCBV75N19W507096",
                    Creator = ApplicationUsersSeeder.GetApplicationUsers().First(),
                    CreatorId = ApplicationUsersSeeder.GetApplicationUsers().First().Id,
                    CityId = 1,
                    City = new City
                    {
                        Id = 1,
                        Name = "Pernik"
                    },
                    ListingFeatures = new HashSet<ListingFeature>
                    {
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 1,
                                Name = "Test",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 2,
                                Name = "Test2",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 3,
                                Name = "Test3",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 4,
                                Name = "Test4",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        }
                    },
                    Images = new HashSet<CarImage>
                    {
                        new CarImage
                        {
                            Id = Guid.Parse("3a767678-beab-4554-b7d5-22cd430e90dc"),
                            Url = "De da znam2",
                            ListingId = Guid.Parse("d33701a6-165d-4341-bab5-572877e0a789")
                        }
                    },
                    ThumbnailId = Guid.Parse("13344399-b6e3-429d-a923-4a4e9cee9e4d"),
                    Thumbnail = new CarImage
                    {
                        Id = Guid.Parse("13344399-b6e3-429d-a923-4a4e9cee9e4d"),
                        Url = "De da znam2",
                        ListingId = Guid.Parse("3a767678-beab-4554-b7d5-22cd430e90dc")
                    }
                },
                new Listing
                {
                    Id = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57"),
                    CreatedOn = DateTime.UtcNow.AddSeconds(new Random().Next(100)),
                    IsDeleted = false,
                    Title = "Testtttttttt3",
                    Description = "Test178687fscasdasdqade1`23",
                    PhoneNumber = "0895998888",
                    CarMakeId = 1,
                    CarMake = new CarMake
                    {
                        Id = 1,
                        Name = "Audi"
                    },
                    CarModelId = 3,
                    CarModel = new CarModel
                    {
                        Id = 3,
                        Name = "RS7",
                        CarMakeId = 1
                    },
                    Price = 130000,
                    Year = 2016,
                    Mileage = 800000,
                    EngineModel = "4.2 TFSI V8",
                    EngineTypeId = 2,
                    EngineType = new EngineType
                    {
                        Id = 2,
                        Name = "Бензин"
                    },
                    VinNumber = "FVCBV75N19W507096",
                    Creator = ApplicationUsersSeeder.GetApplicationUsers().First(),
                    CreatorId = ApplicationUsersSeeder.GetApplicationUsers().First().Id,
                    CityId = 1,
                    City = new City
                    {
                        Id = 1,
                        Name = "Pernik"
                    },
                    ListingFeatures = new HashSet<ListingFeature>
                    {
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 1,
                                Name = "Test",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 2,
                                Name = "Test2",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 3,
                                Name = "Test3",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 4,
                                Name = "Test4",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        }
                    },
                    Images = new HashSet<CarImage>
                    {
                        new CarImage
                        {
                            Id = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                            Url = "De da znam3",
                            ListingId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57")
                        }
                    },
                    ThumbnailId = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                    Thumbnail = new CarImage
                    {
                        Id = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                        Url = "De da znam3",
                        ListingId = Guid.Parse("75045d55-26dc-4cb0-b03f-28a61634ac57")
                    }
                },
                new Listing
                {
                    Id = Guid.Parse("5bc6e47d-cdea-44f7-8114-c01c2d3f3e7c"),
                    CreatedOn = DateTime.UtcNow.AddSeconds(new Random().Next(100)),
                    IsDeleted = true,
                    Title = "Testtttttttt4",
                    Description = "Test178687fscasasdsada`2233",
                    PhoneNumber = "08959986666",
                    CarMakeId = 2,
                    CarMake = new CarMake
                    {
                        Id = 2,
                        Name = "BMW"
                    },
                    CarModelId = 4,
                    CarModel = new CarModel
                    {
                        Id = 4,
                        Name = "M3",
                        CarMakeId = 2
                    },
                    Price = 130000,
                    Year = 2022,
                    Mileage = 1000,
                    EngineModel = "Twin-Turbo 3.0 I6",
                    EngineTypeId = 2,
                    EngineType = new EngineType
                    {
                        Id = 2,
                        Name = "Бензин"
                    },
                    VinNumber = "FFCBV75N19W507096",
                    Creator = ApplicationUsersSeeder.GetApplicationUsers()[1],
                    CreatorId = ApplicationUsersSeeder.GetApplicationUsers()[1].Id,
                    CityId = 2,
                    City = new City
                    {
                        Id = 2,
                        Name = "Sofia"
                    },
                    ListingFeatures = new HashSet<ListingFeature>
                    {
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("5bc6e47d-cdea-44f7-8114-c01c2d3f3e7c"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 1,
                                Name = "Test",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("5bc6e47d-cdea-44f7-8114-c01c2d3f3e7c"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 2,
                                Name = "Test2",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("5bc6e47d-cdea-44f7-8114-c01c2d3f3e7c"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 3,
                                Name = "Test3",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        },
                        new ListingFeature
                        {
                            ListingId = Guid.Parse("5bc6e47d-cdea-44f7-8114-c01c2d3f3e7c"),
                            FeatureId = 1,
                            Feature = new Feature
                            {
                                Id = 4,
                                Name = "Test4",
                                CategoryId = 1,
                                Category = new Category
                                {
                                    Id = 1,
                                    Name = "TestCategory"
                                }
                            }
                        }
                    },
                    Images = new HashSet<CarImage>
                    {
                        new CarImage
                        {
                            Id = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                            Url = "De da znam4",
                            ListingId = Guid.Parse("98386ed7-71db-4665-b127-7765741ff06e")
                        }
                    },
                    ThumbnailId = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                    Thumbnail = new CarImage
                    {
                        Id = Guid.Parse("a410866b-bf0d-4db4-bee9-0a130288b858"),
                        Url = "De da znam4",
                        ListingId = Guid.Parse("98386ed7-71db-4665-b127-7765741ff06e")
                    }
                },
            };
        }

        public static CarImage[] GetCarImages()
        {
            return new CarImage[]
            {
                new CarImage
                {
                    Id = GetListings().First().Images.First().Id,
                    Url = "De da znam",
                    ListingId = GetListings().First().Id,
                    Listing = GetListings().First()
                },
                new CarImage
                {
                    Id = GetListings()[1].Images.First().Id,
                    Url = "De da znam2",
                    ListingId = GetListings()[1].Id,
                    Listing = GetListings()[1]
                },
                new CarImage
                {
                    Id = GetListings()[2].Images.First().Id,
                    Url = "De da znam3",
                    ListingId = GetListings()[2].Id,
                    Listing = GetListings()[2]
                },
                new CarImage
                {
                    Id = GetListings()[3].Images.First().Id,
                    Url = "De da znam4",
                    ListingId = GetListings()[3].Id,
                    Listing = GetListings()[3]
                },
                new CarImage
                {
                    Id = Guid.Parse("e4ab390b-5302-4d5c-b55f-ffc48f6aee82"),
                    Url = "De znam5",
                    ListingId = GetListings()[0].Id,
                    Listing = GetListings()[0]
                }
            };
        }

        public static UserFavoriteListing[] GetUserFavoriteListings()
        {
            return new UserFavoriteListing[]
            {
                new UserFavoriteListing
                {
                    User = ApplicationUsersSeeder.GetApplicationUsers()[1],
                    UserId = ApplicationUsersSeeder.GetApplicationUsers()[1].Id,
                    Listing = GetListings().First(),
                    ListingId = GetListings().First().Id
                },
                new UserFavoriteListing
                {
                    User = ApplicationUsersSeeder.GetApplicationUsers()[1],
                    UserId = ApplicationUsersSeeder.GetApplicationUsers()[1].Id,
                    Listing = GetListings()[1],
                    ListingId = GetListings()[1].Id
                }
            };
        }
    }
}