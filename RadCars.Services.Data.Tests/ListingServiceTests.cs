namespace RadCars.Services.Data.Tests;

using System.Reflection;

using Ganss.Xss;
using Microsoft.EntityFrameworkCore;

using Web.ViewModels.Home;
using Messaging.Contracts;
using Web.ViewModels.Listing;

using static ListingsSeeder;
using static ApplicationUsersSeeder;
using static Common.ExceptionsAndNotificationsMessages;

public class ListingServiceTests
{
    //Use the instance of AutoMapper with its mapping configuration.
    private static IMapper autoMapper = null!;

    private IListingService listingService = null!;

    private IEmailSender emailSender = null!;
    private IHtmlSanitizer htmlSanitizer = null!;

    private ICarService carService = null!;
    private IListingImageService listingImageService = null!;

    private IDeletableEntityRepository<Listing> listingsRepo = null!;
    private IDeletableEntityRepository<CarImage> listingCarImagesRepo = null!;
    private IDeletableEntityRepository<ListingFeature> listingFeaturesRepo = null!;
    private IDeletableEntityRepository<UserFavoriteListing> userFavoriteListingsRepo = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        autoMapper = MapperInstance;

        RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }

    [SetUp]
    public void Setup()
    {
        this.emailSender = new Mock<IEmailSender>().Object;
        this.htmlSanitizer = new Mock<IHtmlSanitizer>().Object;

        this.carService = new Mock<ICarService>().Object;
        this.listingImageService = new Mock<IListingImageService>().Object;

        this.listingsRepo = new Mock<IDeletableEntityRepository<Listing>>().Object;
        this.listingCarImagesRepo = new Mock<IDeletableEntityRepository<CarImage>>().Object;
        this.listingFeaturesRepo = new Mock<IDeletableEntityRepository<ListingFeature>>().Object;
        this.userFavoriteListingsRepo = new Mock<IDeletableEntityRepository<UserFavoriteListing>>().Object;
    }

    [Test]
    public async Task GetActiveListingDetailsAsyncReturnsTheCorrectDetails()
    {
        //Arrange
        var listingsArray = GetListings();
        var testListing = listingsArray[0];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.All()).Returns(listingsAsQueryable.Where(l => l.IsDeleted == false));
        listingRepoMock.Setup(r => r.AllAsNoTracking()).Returns(listingsAsQueryable.Where(l => l.IsDeleted == false));

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var listingDetails = await this.listingService.GetListingDetailsAsync(testListing.Id.ToString());

        //Assert
        var expectedSelectedFeaturesCount = testListing.ListingFeatures.Count;
        var actualSelectedFeaturesCount = listingDetails.ListingFeatures.SelectMany(f => f.Features).Count();

        var lastSelectedFeatureIdExpected = testListing.ListingFeatures.Last().FeatureId;
        var lastSelectedFeatureIdActual = listingDetails.ListingFeatures.SelectMany(f => f.Features).Last().Id;

        Assert.Multiple(() =>
        {
            Assert.That(listingDetails, Is.Not.Null);
            Assert.That(listingDetails.Thumbnail, Is.Not.Null);
            Assert.That(testListing.Title, Is.EqualTo(listingDetails.Title));
            Assert.That(testListing.Id.ToString(), Is.EqualTo(listingDetails.Id));
            Assert.That(testListing.Description, Is.EqualTo(listingDetails.Description));
            Assert.That(testListing.Thumbnail!.Url, Is.EqualTo(listingDetails.Thumbnail.Url));
            Assert.That(testListing.Creator.FullName, Is.EqualTo(listingDetails.CreatorFullName));
            Assert.That(testListing.ThumbnailId.ToString(), Is.EqualTo(listingDetails.Thumbnail.Id));
            Assert.That(expectedSelectedFeaturesCount, Is.EqualTo(actualSelectedFeaturesCount));
            Assert.That(lastSelectedFeatureIdExpected, Is.EqualTo(lastSelectedFeatureIdActual));
        });
    }

    [Test]
    public async Task GetUploadedImagesCountForListingByIdAsyncShouldReturnCorrectCount()
    {
        //Arrange
        var testListing = GetListings().First();
        var listingCarImages = GetCarImages();

        var listingImagesAsQueryable = listingCarImages.AsQueryable().BuildMock();

        var listingCarImagesRepoMock = new Mock<IDeletableEntityRepository<CarImage>>();
        listingCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingImagesAsQueryable);

        this.listingCarImagesRepo = listingCarImagesRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var imagesCount =
            await this.listingService.GetUploadedImagesCountForListingByIdAsync(testListing.Id.ToString());

        //Assert
        var expected = testListing.Images.Count;

        Assert.That(imagesCount, Is.Not.Zero);
        Assert.That(imagesCount, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetUploadedImagesForListingByIdAsyncShouldReturnCorrectViewModelsWhenListingExistsAndUserIsCreator()
    {
        //Arrange
        var testListing = GetListings().First();
        var listingCarImages = GetCarImages();

        var listingImagesAsQueryable = listingCarImages.AsQueryable().BuildMock();

        var listingCarImagesRepoMock = new Mock<IDeletableEntityRepository<CarImage>>();
        listingCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingImagesAsQueryable);

        this.listingCarImagesRepo = listingCarImagesRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var imagesViewModels =
            await this.listingService.GetUploadedImagesForListingByIdAsync(testListing.Id.ToString(), testListing.CreatorId.ToString(), false);

        //Assert
        var firstImageViewModel = imagesViewModels.First();

        var expectedFirstImageId = testListing.Images.First().Id.ToString();
        var actualFistImageId = firstImageViewModel.Id;

        var expectedFirstImageUrl = testListing.Images.First().Url;
        var actualFistImageUrl = firstImageViewModel.Url;

        Assert.Multiple(() =>
        {
            Assert.That(expectedFirstImageId, Is.EqualTo(actualFistImageId));
            Assert.That(expectedFirstImageUrl, Is.EqualTo(actualFistImageUrl));
        });
    }

    [Test]
    public async Task GetUploadedImagesForListingByIdAsyncShouldReturnCorrectViewModelsWhenListingExistsAndUserIsAdmin()
    {
        //Arrange
        var testListing = GetListings().First();
        var listingCarImages = GetCarImages();

        var listingImagesAsQueryable = listingCarImages.AsQueryable().BuildMock();

        var listingCarImagesRepoMock = new Mock<IDeletableEntityRepository<CarImage>>();
        listingCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingImagesAsQueryable);

        this.listingCarImagesRepo = listingCarImagesRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var imagesViewModels =
            await this.listingService.GetUploadedImagesForListingByIdAsync(testListing.Id.ToString(), "AdminId", true);

        //Assert
        var firstImageViewModel = imagesViewModels.First();

        var expectedFirstImageId = testListing.Images.First().Id.ToString();
        var actualFistImageId = firstImageViewModel.Id;

        var expectedFirstImageUrl = testListing.Images.First().Url;
        var actualFistImageUrl = firstImageViewModel.Url;

        Assert.Multiple(() =>
        {
            Assert.That(expectedFirstImageId, Is.EqualTo(actualFistImageId));
            Assert.That(expectedFirstImageUrl, Is.EqualTo(actualFistImageUrl));
        });
    }

    [Test]
    public async Task GetUploadedImagesForListingByIdAsyncShouldReturnEmptyCollectionIfListingDoesNotExist()
    {
        //Arrange
        var testListing = GetListings().First();
        var listingCarImages = GetCarImages();

        var listingImagesAsQueryable = listingCarImages.AsQueryable().BuildMock();

        var listingCarImagesRepoMock = new Mock<IDeletableEntityRepository<CarImage>>();
        listingCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingImagesAsQueryable);

        this.listingCarImagesRepo = listingCarImagesRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var imagesViewModels =
            await this.listingService.GetUploadedImagesForListingByIdAsync("NonExistingListingId", testListing.CreatorId.ToString(), false);

        //Assert
        Assert.That(imagesViewModels, Is.Empty);
    }

    [Test]
    public async Task GetUploadedImagesForListingByIdAsyncShouldReturnEmptyCollectionIfUserIsNotCreator()
    {
        //Arrange
        var testListing = GetListings().First();
        var listingCarImages = GetCarImages();

        var listingImagesAsQueryable = listingCarImages.AsQueryable().BuildMock();

        var listingCarImagesRepoMock = new Mock<IDeletableEntityRepository<CarImage>>();
        listingCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingImagesAsQueryable);

        this.listingCarImagesRepo = listingCarImagesRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var imagesViewModels =
            await this.listingService.GetUploadedImagesForListingByIdAsync(testListing.Id.ToString(), "NotTheCreatorId", false);

        //Assert
        Assert.That(imagesViewModels, Is.Empty);
    }

    [Test]
    public async Task GetMostRecentListingsAsyncShouldReturnTheThreeNewestListings()
    {
        //Arrange
        var listingsArray = GetListings();

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.All()).Returns(listingsAsQueryable.Where(l => l.IsDeleted == false));
        listingRepoMock.Setup(r => r.AllAsNoTracking()).Returns(listingsAsQueryable.Where(l => l.IsDeleted == false));

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var mostRecentListings = await this.listingService.GetMostRecentListingsAsync();

        var listingIndexViewModels = mostRecentListings.ToList();

        var expectedFirstListingId = listingsArray.Where(l => l.IsDeleted == false).OrderByDescending(l => l.CreatedOn).First().Id.ToString();
        var actualFirstListingId = listingIndexViewModels.First().Id;

        var expectedLastListingId = listingsArray.Where(l => l.IsDeleted == false).OrderByDescending(l => l.CreatedOn).Last().Id.ToString();
        var actualLastListingId = listingIndexViewModels.Last().Id;

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(listingIndexViewModels, Is.Not.Null);
            Assert.That(listingIndexViewModels.Count, Is.EqualTo(3));
            Assert.That(expectedFirstListingId, Is.EqualTo(actualFirstListingId));
            Assert.That(expectedLastListingId, Is.EqualTo(actualLastListingId));
        });
    }

    [Test]
    public async Task GetAllListingsByUserIdAsyncReturnsCorrectListingsForUser()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers().First();

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllAsNoTracking()).Returns(listingsAsQueryable.Where(x => x.IsDeleted == false));

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var userListingsViewModels = await this.listingService.GetAllListingsByUserIdAsync(testUser.Id.ToString());

        //Assert
        var userListingsAsList = userListingsViewModels.ToList();

        var expectedCountOfListings = listingsArray.Count(x => x.CreatorId == testUser.Id);
        var actualCountOfListings = userListingsAsList.Count;

        var expectedLastListingId = listingsArray.Where(x => x.CreatorId == testUser.Id && x.IsDeleted == false)
            .OrderByDescending(x => x.CreatedOn).Last().Id.ToString();
        var actualLastListingId = userListingsAsList.Last().Id;

        Assert.Multiple(() =>
        {
            Assert.That(userListingsAsList, Is.Not.Null);
            Assert.That(userListingsAsList, Is.Not.Empty);
            Assert.That(expectedCountOfListings, Is.EqualTo(actualCountOfListings));
            Assert.That(expectedLastListingId, Is.EqualTo(actualLastListingId));
        });
    }

    [Test]
    public async Task GetAllListingsByUserIdAsyncReturnsEmptyCollectionIfUserHasNoActiveListings()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[1];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllAsNoTracking()).Returns(listingsAsQueryable.Where(x => x.IsDeleted == false));

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var userListingsViewModels = await this.listingService.GetAllListingsByUserIdAsync(testUser.Id.ToString());

        //Assert
        var userListingsAsList = userListingsViewModels.ToList();

        Assert.Multiple(() =>
        {
            Assert.That(userListingsAsList, Is.Not.Null);
            Assert.That(userListingsAsList, Is.Empty);
        });
    }

    [Test]
    public async Task GetFavoriteListingsByUserIdAsyncReturnCorrectFavoriteListingsForUser()
    {
        //Arrange
        var userFavoriteListingsArray = GetUserFavoriteListings();
        var testUser = GetApplicationUsers()[1];

        var userFavoriteListingsAsQueryable = userFavoriteListingsArray.AsQueryable().BuildMock();

        var userFavoriteListingRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteListing>>();
        userFavoriteListingRepoMock.Setup(r => r.All()).Returns(userFavoriteListingsAsQueryable.Where(x => x.IsDeleted == false));

        this.userFavoriteListingsRepo = userFavoriteListingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var favoriteListings = await this.listingService.GetFavoriteListingsByUserIdAsync(testUser.Id.ToString());

        //Assert
        var favoriteListingsAsList = favoriteListings.ToList();

        var expectedCount = userFavoriteListingsArray.Count(x => x.UserId == testUser.Id && x.IsDeleted == false);
        var actualCount = favoriteListingsAsList.Count;

        var userIsCreatorOfFavoriteListing = favoriteListingsAsList.Any(x => x.CreatorId == testUser.Id.ToString());

        Assert.Multiple(() =>
        {
            Assert.That(favoriteListingsAsList, Is.Not.Null);
            Assert.That(favoriteListingsAsList, Is.Not.Empty);
            Assert.That(userIsCreatorOfFavoriteListing, Is.False);
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        });
    }

    [Test]
    public async Task IsListingInUserFavoritesByIdAsyncReturnsFalseIfListingIsStillNotInUserFavorites()
    {
        //Arrange
        var userFavoriteListingsArray = GetUserFavoriteListings();
        var testUser = GetApplicationUsers()[1];
        var testListing = GetListings()[2];

        var userFavoriteListingsAsQueryable = userFavoriteListingsArray.AsQueryable().BuildMock();

        var userFavoriteListingRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteListing>>();
        userFavoriteListingRepoMock.Setup(r => r.All()).Returns(userFavoriteListingsAsQueryable.Where(x => x.IsDeleted == false));

        this.userFavoriteListingsRepo = userFavoriteListingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var result = await this.listingService.IsListingInUserFavoritesByIdAsync(testListing.Id.ToString(), testUser.Id.ToString());

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsListingInUserFavoritesByIdAsyncReturnsTrueIfListingIsInUserFavorites()
    {
        //Arrange
        var userFavoriteListingsArray = GetUserFavoriteListings();
        var testUser = GetApplicationUsers()[1];
        var testListing = GetListings()[1];

        var userFavoriteListingsAsQueryable = userFavoriteListingsArray.AsQueryable().BuildMock();

        var userFavoriteListingRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteListing>>();
        userFavoriteListingRepoMock.Setup(r => r.All()).Returns(userFavoriteListingsAsQueryable.Where(x => x.IsDeleted == false));

        this.userFavoriteListingsRepo = userFavoriteListingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var result = await this.listingService.IsListingInUserFavoritesByIdAsync(testListing.Id.ToString(), testUser.Id.ToString());

        Assert.That(result, Is.True);
    }

    [Test]
    public void FavoriteListingByIdAsyncThrowsInvalidOperationExceptionIfListingIsInUserFavorites()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[1];
        var testListing = GetListings()[1];
        var favoriteListings = GetUserFavoriteListings();

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();
        var favoriteListingsAsQueryable = favoriteListings.AsQueryable().BuildMock();

        var listingsRepoMock =
            new Mock<IDeletableEntityRepository<Listing>>();
        listingsRepoMock.Setup(r => r.All()).Returns(listingsAsQueryable.Where(x => x.IsDeleted == false));

        var favoriteListingsRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteListing>>();
        favoriteListingsRepoMock.Setup(r => r.All()).Returns(favoriteListingsAsQueryable);

        this.listingsRepo = listingsRepoMock.Object;
        this.userFavoriteListingsRepo = favoriteListingsRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.listingService.FavoriteListingByIdAsync(testListing.Id.ToString(), testUser.Id.ToString());
        }, ListingIsAlreadyInCurrentUserFavorites);
    }

    [Test]
    public async Task FavoriteListingByIdAsyncAddsListingToUserFavoritesIfListingIsNotInUserFavorites()
    {
        //Arrange
        var listingsArray = GetListings();
        var userFavoriteListingsList = GetUserFavoriteListings().ToList();
        var testUser = GetApplicationUsers()[1];
        var testListing = GetListings()[2];

        var userFavoriteListingsAsQueryable = userFavoriteListingsList.AsQueryable().BuildMock();

        var userFavoriteListingRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteListing>>();

        userFavoriteListingRepoMock.Setup(r => r.All()).Returns(userFavoriteListingsAsQueryable.Where(x => x.IsDeleted == false));

        userFavoriteListingRepoMock.Setup(r => r.AddAsync(It.IsAny<UserFavoriteListing>()))
            .Callback((UserFavoriteListing x) => userFavoriteListingsList.Add(x));

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingsRepoMock =
            new Mock<IDeletableEntityRepository<Listing>>();
        listingsRepoMock.Setup(r => r.All()).Returns(listingsAsQueryable.Where(x => x.IsDeleted == false));

        this.listingsRepo = listingsRepoMock.Object;

        this.userFavoriteListingsRepo = userFavoriteListingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var userFavoriteCount = userFavoriteListingsList.Count(x => x.UserId == testUser.Id && x.IsDeleted == false);

        await this.listingService.FavoriteListingByIdAsync(testListing.Id.ToString(), testUser.Id.ToString());

        //Assert
        var expectedCount = userFavoriteCount + 1;
        var actualCount = userFavoriteListingsList.Count;

        Assert.That(expectedCount, Is.EqualTo(actualCount));
    }

    [Test]
    public void FavoriteListingByIdAsyncThrowsExceptionIfListingIsNotExisting()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[1];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingsRepoMock =
            new Mock<IDeletableEntityRepository<Listing>>();
        listingsRepoMock.Setup(r => r.All()).Returns(listingsAsQueryable.Where(x => x.IsDeleted == false));

        this.listingsRepo = listingsRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.listingService.FavoriteListingByIdAsync("notExistingIdOfListing", testUser.Id.ToString());
        });
    }

    [Test]
    public async Task GetFavoritesCountForListingByIdAsyncReturnsCorrectCount()
    {
        //Arrange
        var userFavoriteListingsList = GetUserFavoriteListings().ToList();
        var testListing = GetListings()[1];

        var userFavoriteListingsAsQueryable = userFavoriteListingsList.AsQueryable().BuildMock();

        var userFavoriteListingRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteListing>>();

        userFavoriteListingRepoMock.Setup(r => r.AllAsNoTracking()).Returns(userFavoriteListingsAsQueryable.Where(x => x.IsDeleted == false));

        this.userFavoriteListingsRepo = userFavoriteListingRepoMock.Object;

        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Act
        var count = await this.listingService.GetFavoritesCountForListingByIdAsync(testListing.Id.ToString());

        //Assert
        var expectedCount = userFavoriteListingsList.Count(x => x.ListingId == testListing.Id);

        Assert.Multiple(() =>
        {
            Assert.That(count, Is.Not.Zero);
            Assert.That(expectedCount, Is.EqualTo(count));
        });
    }

    [Test]
    public async Task UnFavoriteListingByIdAsyncRemovesListingFromUserFavoritesIfItExists()
    {
        //Arrange
        var userFavoriteListingsList = GetUserFavoriteListings().ToList();
        var testUser = GetApplicationUsers()[1];
        var testListing = GetListings()[1];

        var userFavoriteListingsAsQueryable = userFavoriteListingsList.AsQueryable().BuildMock();

        var userFavoriteListingRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteListing>>();

        userFavoriteListingRepoMock.Setup(r => r.All()).Returns(userFavoriteListingsAsQueryable.Where(x => x.IsDeleted == false));

        userFavoriteListingRepoMock.Setup(r => r.HardDelete(It.IsAny<UserFavoriteListing>()))
            .Callback((UserFavoriteListing x) => userFavoriteListingsList.Remove(x));

        this.userFavoriteListingsRepo = userFavoriteListingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var userFavoriteCount = userFavoriteListingsList.Count(x => x.UserId == testUser.Id);

        await this.listingService.UnFavoriteListingByIdAsync(testListing.Id.ToString(), testUser.Id.ToString());

        //Assert
        var expectedCount = userFavoriteCount - 1;
        var actualCount = userFavoriteListingsList.Count;

        var listingExistsAfterRemove = userFavoriteListingsList.Any(x => x.ListingId == testListing.Id);

        Assert.Multiple(() =>
        {
            Assert.That(actualCount, Is.Not.Zero);
            Assert.That(listingExistsAfterRemove, Is.False);
            Assert.That(expectedCount, Is.EqualTo(actualCount));
        });
    }

    [Test]
    public async Task GetAllDeactivatedListingsByUserIdAsyncReturnsCorrectDeactivatedListings()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[1];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllAsNoTrackingWithDeleted()).Returns(listingsAsQueryable.Where(l => l.IsDeleted));

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        var deactivatedListings = await this.listingService.GetAllDeactivatedListingsByUserIdAsync(testUser.Id.ToString());

        //Assert
        var resultAsList = deactivatedListings.ToList();

        var expectedCount = listingsArray.Count(x => x.IsDeleted && x.CreatorId == testUser.Id);
        var actualCount = resultAsList.Count;

        Assert.That(resultAsList, Is.Not.Null);
        Assert.That(resultAsList, Is.Not.Empty);
        Assert.That(actualCount, Is.EqualTo(expectedCount));
    }

    [Test]
    public void GetListingEditAsyncThrowsInvalidOperationExceptionWhenListingDoesNotExist()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[1];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.listingService.GetListingEditAsync("ListingIdDoesNotExist", testUser.Id.ToString(), false);
        });
    }

    [Test]
    public void GetListingEditAsyncThrowsInvalidOperationExceptionWhenUserIsNotCreatorNorAdmin()
    {
        //Arrange
        var listingsArray = GetListings();
        var testListing = listingsArray[0];
        var testUser = GetApplicationUsers()[1];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.listingService.GetListingEditAsync(testListing.Id.ToString(), testUser.Id.ToString(), false);
        });
    }

    [Test]
    public void EditListingAsyncThrowsExceptionWhenFormIsEmpty()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[1];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            var emptyForm = new ListingEditFormModel();
            await this.listingService.EditListingAsync(emptyForm, testUser.Id.ToString(), false);
        });
    }

    [Test]
    public async Task GetDeactivatedListingDetailsAsyncReturnsCorrectDeactivatedListingWhenUserIsCreator()
    {
        //Arrange
        var listingsArray = GetListings();
        var testListing = listingsArray[3];
        var testUser = GetApplicationUsers()[1];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Assert
        var deactivatedListing = await this.listingService.GetDeactivatedListingDetailsAsync(testListing.Id.ToString(), testUser.Id.ToString(), false);

        var expectedListingId = listingsArray.First(x => x.Id == testListing.Id && x.IsDeleted).Id.ToString();
        var actualListingId = deactivatedListing.Id;

        Assert.Multiple(() =>
        {
            Assert.That(deactivatedListing, Is.Not.Null);
            Assert.That(expectedListingId, Is.EqualTo(actualListingId));
        });
    }

    [Test]
    public async Task GetDeactivatedListingDetailsAsyncReturnsCorrectDeactivatedListingWhenUserIsAdmin()
    {
        //Arrange
        var listingsArray = GetListings();
        var testListing = listingsArray[3];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Assert
        var deactivatedListing = await this.listingService.GetDeactivatedListingDetailsAsync(testListing.Id.ToString(), "AdminId", true);

        var expectedListingId = listingsArray.First(x => x.Id == testListing.Id && x.IsDeleted).Id.ToString();
        var actualListingId = deactivatedListing.Id;

        Assert.Multiple(() =>
        {
            Assert.That(deactivatedListing, Is.Not.Null);
            Assert.That(expectedListingId, Is.EqualTo(actualListingId));
        });
    }

    [Test]
    public void GetDeactivatedListingDetailsAsyncThrowsExceptionWhenListingDoesNotExist()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[1];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.listingService.GetDeactivatedListingDetailsAsync("ListingIdDoesNotExist", testUser.Id.ToString(), false);
        });
    }

    [Test]
    public async Task GetChooseThumbnailAsyncGetsListingImagesInAChooseThumbnailFormModelIfListingExistsAndUserIsCreator()
    {
        //Arrange
        var listingsArray = GetListings();
        var testListing = listingsArray[1];
        var testUser = GetApplicationUsers()[0];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Assert
        var chooseThumbnailViewModel =
            await this.listingService.GetChooseThumbnailAsync(testListing.Id.ToString(), testUser.Id.ToString(), false);

        var expectedFirstImageId = testListing.Images.First().Id.ToString();
        var actualFirstImageId = chooseThumbnailViewModel.Images.First().Id;

        var expectedFirstImageUrl = testListing.Images.First().Url;
        var actualFirstImageUrl = chooseThumbnailViewModel.Images.First().Url;

        Assert.Multiple(() =>
        {
            Assert.That(chooseThumbnailViewModel, Is.Not.Null);
            Assert.That(expectedFirstImageId, Is.EqualTo(actualFirstImageId));
            Assert.That(expectedFirstImageUrl, Is.EqualTo(actualFirstImageUrl));
        });
    }

    [Test]
    public void GetChooseThumbnailAsyncThrowsExceptionWhenListingDoesNotExist()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[0];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.listingService.GetChooseThumbnailAsync("ListingIdDoesNotExist", testUser.Id.ToString(), false);
        });
    }

    [Test]
    public void GetChooseThumbnailAsyncThrowsExceptionWhenTheUserIsNotCreatorNorAdmin()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[1];
        var testListing = listingsArray[0];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.listingService.GetChooseThumbnailAsync(testListing.Id.ToString(), testUser.Id.ToString(), false);
        });
    }

    [Test]
    public async Task DeactivateListingByIdAsyncSetsIsDeletedToTrueIfListingExistsAndUserIsCreator()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[0];
        var testListing = listingsArray[0];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();

        listingRepoMock.Setup(r => r.All()).Returns(listingsAsQueryable.Where(x => x.IsDeleted == false));

        listingRepoMock.Setup(r => r.Delete(It.IsAny<Listing>())).Callback((Listing listing) =>
        {
            var listingToSoftDelete = listingsArray.First(x => x.Id == listing.Id);

            listingToSoftDelete.IsDeleted = true;
            listingToSoftDelete.DeletedOn = DateTime.UtcNow;
        });

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        await this.listingService.DeactivateListingByIdAsync(testListing.Id.ToString(), testUser.Id.ToString(),
            false);

        //Assert
        var softDeletionStateOfListing = testListing.IsDeleted;

        Assert.Multiple(() =>
        {
            Assert.That(softDeletionStateOfListing, Is.True);
            Assert.That(testListing.DeletedOn, Is.Not.Null);
        });
    }

    [Test]
    public async Task DeactivateListingByIdAsyncSetsIsDeletedToTrueIfListingExistsAndUserIsAdmin()
    {
        //Arrange
        var listingsArray = GetListings();
        var testListing = listingsArray[0];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();

        listingRepoMock.Setup(r => r.All()).Returns(listingsAsQueryable.Where(x => x.IsDeleted == false));

        listingRepoMock.Setup(r => r.Delete(It.IsAny<Listing>())).Callback((Listing listing) =>
        {
            var listingToSoftDelete = listingsArray.First(x => x.Id == listing.Id);

            listingToSoftDelete.IsDeleted = true;
            listingToSoftDelete.DeletedOn = DateTime.UtcNow;
        });

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        await this.listingService.DeactivateListingByIdAsync(testListing.Id.ToString(), "AdminId",
            true);

        //Assert
        var softDeletionStateOfListing = testListing.IsDeleted;

        Assert.Multiple(() =>
        {
            Assert.That(softDeletionStateOfListing, Is.True);
            Assert.That(testListing.DeletedOn, Is.Not.Null);
        });
    }

    [Test]
    public void DeactivateListingByIdAsyncThrowsExceptionWhenListingDoesNotExistOrIsSoftDeleted()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[1];
        var testListing = listingsArray[3];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();

        listingRepoMock.Setup(r => r.All()).Returns(listingsAsQueryable.Where(x => x.IsDeleted == false));

        this.listingsRepo = listingRepoMock.Object;

        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Act & Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.listingService.DeactivateListingByIdAsync(testListing.Id.ToString(), testUser.Id.ToString(),
                false);
        });
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.listingService.DeactivateListingByIdAsync("ListingIdDoesNotExist", testUser.Id.ToString(),
                false);
        });
    }

    [Test]
    public async Task ReactivateListingByIdAsyncUnDeletesTheListingIfItExistsAndTheUserIsCreator()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[0];
        var testListing = listingsArray[0];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();

        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        listingRepoMock.Setup(r => r.Undelete(It.IsAny<Listing>())).Callback((Listing listing) =>
        {
            var listingToSoftDelete = listingsArray.First(x => x.Id == listing.Id);

            listingToSoftDelete.IsDeleted = false;
            listingToSoftDelete.DeletedOn = null;
        });

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        await this.listingService.ReactivateListingByIdAsync(testListing.Id.ToString(), testUser.Id.ToString(),
            false);

        //Assert
        var softDeletionStateOfListing = testListing.IsDeleted;

        Assert.Multiple(() =>
        {
            Assert.That(softDeletionStateOfListing, Is.False);
            Assert.That(testListing.DeletedOn, Is.Null);
        });
    }

    [Test]
    public async Task ReactivateListingByIdAsyncUnDeletesTheListingIfItExistsAndTheUserIsAdmin()
    {
        //Arrange
        var listingsArray = GetListings();
        var testListing = listingsArray[0];

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();

        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        listingRepoMock.Setup(r => r.Undelete(It.IsAny<Listing>())).Callback((Listing listing) =>
        {
            var listingToSoftDelete = listingsArray.First(x => x.Id == listing.Id);

            listingToSoftDelete.IsDeleted = false;
            listingToSoftDelete.DeletedOn = null;
        });

        this.listingsRepo = listingRepoMock.Object;

        //Act
        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        await this.listingService.ReactivateListingByIdAsync(testListing.Id.ToString(), "AdminId",
            true);

        //Assert
        var softDeletionStateOfListing = testListing.IsDeleted;

        Assert.Multiple(() =>
        {
            Assert.That(softDeletionStateOfListing, Is.False);
            Assert.That(testListing.DeletedOn, Is.Null);
        });
    }

    [Test]
    public void AddThumbnailToListingByIdAsyncThrowsExceptionWhenListingDoesNotExist()
    {
        //Arrange
        var testUser = GetApplicationUsers()[0];
        var testListing = GetListings().First();
        var newThumbnailId = testListing.Images.Last().Id.ToString();
        var listingCarImages = GetCarImages();

        var listingImagesAsQueryable = listingCarImages.AsQueryable().BuildMock();

        var listingCarImagesRepoMock = new Mock<IDeletableEntityRepository<CarImage>>();
        listingCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingImagesAsQueryable);

        this.listingCarImagesRepo = listingCarImagesRepoMock.Object;

        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.listingService.AddThumbnailToListingByIdAsync("NotExistingListing", newThumbnailId, testUser.Id.ToString(), false);
        }, InvalidImageForThumbnailProvided);
    }

    [Test]
    public void AddThumbnailToListingByIdAsyncThrowsExceptionWhenImageIdDoesNotExist()
    {
        //Arrange
        var testUser = GetApplicationUsers()[0];
        var testListing = GetListings().First();
        var listingCarImages = GetCarImages();

        var listingImagesAsQueryable = listingCarImages.AsQueryable().BuildMock();

        var listingCarImagesRepoMock = new Mock<IDeletableEntityRepository<CarImage>>();
        listingCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingImagesAsQueryable);

        this.listingCarImagesRepo = listingCarImagesRepoMock.Object;

        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.listingService.AddThumbnailToListingByIdAsync(testListing.Id.ToString(), "notExisting", testUser.Id.ToString(), false);
        }, InvalidImageForThumbnailProvided);
    }

    [Test]
    public void AddThumbnailToListingByIdAsyncThrowsExceptionWhenUserIsNotCreatorNorAdmin()
    {
        //Arrange
        var testUser = GetApplicationUsers()[1];
        var testListing = GetListings().First();
        var newThumbnailId = testListing.Images.Last().Id.ToString();
        var listingCarImages = GetCarImages();

        var listingImagesAsQueryable = listingCarImages.AsQueryable().BuildMock();

        var listingCarImagesRepoMock = new Mock<IDeletableEntityRepository<CarImage>>();
        listingCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingImagesAsQueryable);

        this.listingCarImagesRepo = listingCarImagesRepoMock.Object;

        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.listingService.AddThumbnailToListingByIdAsync(testListing.Id.ToString(), newThumbnailId, testUser.Id.ToString(), false);
        }, InvalidImageForThumbnailProvided);
    }

    [Test]
    public async Task AddThumbnailToListingByIdAsyncChangesToThumbnailWhenUserIsCreator()
    {
        //Arrange
        var listingsArray = GetListings();
        var testUser = GetApplicationUsers()[0];
        var testListing = GetListings().First();
        var newThumbnailId = testListing.Images.Last().Id.ToString();
        var listingCarImages = GetCarImages();

        var listingImagesAsQueryable = listingCarImages.AsQueryable().BuildMock();

        var listingCarImagesRepoMock = new Mock<IDeletableEntityRepository<CarImage>>();
        listingCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingImagesAsQueryable);

        var listingsAsQueryable = listingsArray.AsQueryable().BuildMock();

        var listingRepoMock = new Mock<IDeletableEntityRepository<Listing>>();
        listingRepoMock.Setup(r => r.AllWithDeleted()).Returns(listingsAsQueryable);

        this.listingsRepo = listingRepoMock.Object;

        this.listingCarImagesRepo = listingCarImagesRepoMock.Object;

        this.listingService = new ListingService(this.listingImageService, this.carService, this.listingsRepo, this.listingCarImagesRepo,
            autoMapper, this.listingFeaturesRepo, this.userFavoriteListingsRepo, this.htmlSanitizer, this.emailSender);

        //Act
        await this.listingService.AddThumbnailToListingByIdAsync(testListing.Id.ToString(), newThumbnailId, testUser.Id.ToString(), false);

        //Assert
        var changedListing = await listingsAsQueryable.FirstAsync(x => x.Id == testListing.Id);

        var expectedThumbnailId = newThumbnailId;
        var actualThumbnailId = changedListing.ThumbnailId.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(testListing.Thumbnail, Is.Not.Null);
            Assert.That(testListing.ThumbnailId, Is.Not.Null);
            Assert.That(expectedThumbnailId, Is.EqualTo(actualThumbnailId));
        });
    }
}