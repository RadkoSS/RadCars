namespace RadCars.Services.Data.Tests;

using System.Reflection;

using Ganss.Xss;
using Microsoft.EntityFrameworkCore;

using Web.ViewModels.Home;
using Messaging.Contracts;
using Web.ViewModels.Auction;

using static TestData.DataSeeder.AuctionsSeeder;
using static Common.ExceptionsAndNotificationsMessages;
using static TestData.DataSeeder.ApplicationUsersSeeder;

public class AuctionServiceTests
{
    //Use the instance of AutoMapper with its mapping configuration.
    private static IMapper autoMapper = null!;

    private IAuctionService auctionService = null!;

    private IEmailSender emailSender = null!;
    private IHtmlSanitizer htmlSanitizer = null!;

    private ICarService carService = null!;
    private IAuctionImageService auctionImageService = null!;

    private IDeletableEntityRepository<Auction> auctionsRepo = null!;
    private IDeletableEntityRepository<UserAuctionBid> bidsRepo = null!;
    private IDeletableEntityRepository<AuctionFeature> auctionFeaturesRepo = null!;
    private IDeletableEntityRepository<AuctionCarImage> auctionCarImagesRepo = null!;
    private IDeletableEntityRepository<UserFavoriteAuction> userFavoriteAuctionsRepo = null!;

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
        this.auctionImageService = new Mock<IAuctionImageService>().Object;

        this.auctionsRepo = new Mock<IDeletableEntityRepository<Auction>>().Object;
        this.bidsRepo = new Mock<IDeletableEntityRepository<UserAuctionBid>>().Object;
        this.auctionFeaturesRepo = new Mock<IDeletableEntityRepository<AuctionFeature>>().Object;
        this.auctionCarImagesRepo = new Mock<IDeletableEntityRepository<AuctionCarImage>>().Object;
        this.userFavoriteAuctionsRepo = new Mock<IDeletableEntityRepository<UserFavoriteAuction>>().Object;
    }

    [Test]
    public async Task GetAuctionDetailsAsyncReturnsTheCorrectDetailsForUser()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var user = GetApplicationUsers()[1];
        var testAuction = auctionsArray[0];

        var auctionsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionsRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionsRepoMock.Setup(r => r.All()).Returns(auctionsQueryable.Where(l => l.IsDeleted == false));
        auctionsRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsQueryable);
        auctionsRepoMock.Setup(r => r.AllAsNoTracking()).Returns(auctionsQueryable.Where(l => l.IsDeleted == false));

        this.auctionsRepo = auctionsRepoMock.Object;

        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Act
        var auctionDetails = await this.auctionService.GetAuctionDetailsAsync(testAuction.Id.ToString(), user.Id.ToString(), false);

        //Assert
        var expectedSelectedFeaturesCount = testAuction.AuctionFeatures.Count;
        var actualSelectedFeaturesCount = auctionDetails.AuctionFeatures.SelectMany(f => f.Features).Count();

        var lastSelectedFeatureIdExpected = testAuction.AuctionFeatures.Last().FeatureId;
        var lastSelectedFeatureIdActual = auctionDetails.AuctionFeatures.SelectMany(f => f.Features).Last().Id;

        Assert.Multiple(() =>
        {
            Assert.That(auctionDetails, Is.Not.Null);
            Assert.That(auctionDetails.Thumbnail, Is.Not.Null);
            Assert.That(testAuction.Title, Is.EqualTo(auctionDetails.Title));
            Assert.That(testAuction.Id.ToString(), Is.EqualTo(auctionDetails.Id));
            Assert.That(testAuction.Description, Is.EqualTo(auctionDetails.Description));
            Assert.That(testAuction.Thumbnail!.Url, Is.EqualTo(auctionDetails.Thumbnail.Url));
            Assert.That(testAuction.Creator.FullName, Is.EqualTo(auctionDetails.CreatorFullName));
            Assert.That(testAuction.ThumbnailId.ToString(), Is.EqualTo(auctionDetails.Thumbnail.Id));
            Assert.That(expectedSelectedFeaturesCount, Is.EqualTo(actualSelectedFeaturesCount));
            Assert.That(lastSelectedFeatureIdExpected, Is.EqualTo(lastSelectedFeatureIdActual));
        });
    }

    [Test]
    public async Task GetUploadedImagesCountForAuctionByIdAsyncShouldReturnCorrectCount()
    {
        //Arrange
        var testAuction = GetAuctions().First();
        var auctionCarImages = GetAuctionCarImages();

        var auctionImagesAsQueryable = auctionCarImages.AsQueryable().BuildMock();

        var auctionImagesRepoMock = new Mock<IDeletableEntityRepository<AuctionCarImage>>();
        auctionImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionImagesAsQueryable);

        this.auctionCarImagesRepo = auctionImagesRepoMock.Object;

        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Act
        var imagesCount =
            await this.auctionService.GetUploadedImagesCountForAuctionByIdAsync(testAuction.Id.ToString());

        //Assert
        var expected = testAuction.Images.Count;

        Assert.That(imagesCount, Is.Not.Zero);
        Assert.That(imagesCount, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetUploadedImagesForAuctionByIdAsyncShouldReturnCorrectViewModelsWhenAuctionExistsAndUserIsCreator()
    {
        //Arrange
        var testAuction = GetAuctions().First();
        var auctionCarImages = GetAuctionCarImages();

        var auctionImagesAsQueryable = auctionCarImages.AsQueryable().BuildMock();

        var auctionCarImagesRepoMock = new Mock<IDeletableEntityRepository<AuctionCarImage>>();
        auctionCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionImagesAsQueryable);

        this.auctionCarImagesRepo = auctionCarImagesRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var imagesViewModels =
            await this.auctionService.GetUploadedImagesForAuctionByIdAsync(testAuction.Id.ToString(), testAuction.CreatorId.ToString(), false);

        //Assert
        var firstImageViewModel = imagesViewModels.First();

        var expectedFirstImageId = testAuction.Images.First().Id.ToString();
        var actualFistImageId = firstImageViewModel.Id;

        var expectedFirstImageUrl = testAuction.Images.First().Url;
        var actualFistImageUrl = firstImageViewModel.Url;

        Assert.Multiple(() =>
        {
            Assert.That(expectedFirstImageId, Is.EqualTo(actualFistImageId));
            Assert.That(expectedFirstImageUrl, Is.EqualTo(actualFistImageUrl));
        });
    }

    [Test]
    public async Task GetUploadedImagesForAuctionByIdAsyncShouldReturnCorrectViewModelsWhenAuctionExistsAndUserIsAdmin()
    {
        //Arrange
        var testAuction = GetAuctions().First();
        var auctionCarImages = GetAuctionCarImages();

        var auctionImagesAsQueryable = auctionCarImages.AsQueryable().BuildMock();

        var auctionCarImagesRepoMock = new Mock<IDeletableEntityRepository<AuctionCarImage>>();
        auctionCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionImagesAsQueryable);

        this.auctionCarImagesRepo = auctionCarImagesRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var imagesViewModels =
            await this.auctionService.GetUploadedImagesForAuctionByIdAsync(testAuction.Id.ToString(), "AdminId", true);

        //Assert
        var firstImageViewModel = imagesViewModels.First();

        var expectedFirstImageId = testAuction.Images.First().Id.ToString();
        var actualFistImageId = firstImageViewModel.Id;

        var expectedFirstImageUrl = testAuction.Images.First().Url;
        var actualFistImageUrl = firstImageViewModel.Url;

        Assert.Multiple(() =>
        {
            Assert.That(expectedFirstImageId, Is.EqualTo(actualFistImageId));
            Assert.That(expectedFirstImageUrl, Is.EqualTo(actualFistImageUrl));
        });
    }

    [Test]
    public async Task GetUploadedImagesForAuctionByIdAsyncShouldReturnEmptyCollectionIfAuctionDoesNotExist()
    {
        //Arrange
        var testAuction = GetAuctions().First();
        var auctionCarImages = GetAuctionCarImages();

        var auctionImagesAsQueryable = auctionCarImages.AsQueryable().BuildMock();

        var auctionCarImagesRepoMock = new Mock<IDeletableEntityRepository<AuctionCarImage>>();
        auctionCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionImagesAsQueryable);

        this.auctionCarImagesRepo = auctionCarImagesRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var imagesViewModels =
            await this.auctionService.GetUploadedImagesForAuctionByIdAsync("NonExistingAuctionId", testAuction.CreatorId.ToString(), false);

        //Assert
        Assert.That(imagesViewModels, Is.Empty);
    }

    [Test]
    public async Task GetUploadedImagesForAuctionByIdAsyncShouldReturnEmptyCollectionIfUserIsNotCreator()
    {
        //Arrange
        var testAuction = GetAuctions().First();
        var auctionCarImages = GetAuctionCarImages();

        var auctionImagesAsQueryable = auctionCarImages.AsQueryable().BuildMock();

        var auctionCarImagesRepoMock = new Mock<IDeletableEntityRepository<AuctionCarImage>>();
        auctionCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionImagesAsQueryable);

        this.auctionCarImagesRepo = auctionCarImagesRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var imagesViewModels =
            await this.auctionService.GetUploadedImagesForAuctionByIdAsync(testAuction.Id.ToString(), "NotTheCreatorId", false);

        //Assert
        Assert.That(imagesViewModels, Is.Empty);
    }

    [Test]
    public async Task GetMostRecentAuctionsAsyncShouldReturnTheThreeNewestAuctions()
    {
        //Arrange
        var auctionsArray = GetAuctions();

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllAsNoTracking()).Returns(auctionsAsQueryable.Where(l => l.IsDeleted == false));

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var mostRecentAuctions = await this.auctionService.GetMostRecentAuctionsAsync();

        var auctionIndexViewModels = mostRecentAuctions.ToList();

        var expectedFirstAuctionId = auctionsArray.Where(l => l.IsDeleted == false).OrderBy(a => a.IsOver == false ? 0 : a.IsOver == null ? 1 : 2)
            .ThenBy(a => a.IsOver == false ? (a.EndTime - DateTime.UtcNow).TotalSeconds : 0)
            .ThenBy(a => a.IsOver == null ? (a.StartTime - DateTime.UtcNow).TotalSeconds : 0).First().Id.ToString();

        var actualFirstAuctionId = auctionIndexViewModels.First().Id;

        var expectedLastAuctionId = auctionsArray.Where(l => l.IsDeleted == false).OrderBy(a => a.IsOver == false ? 0 : a.IsOver == null ? 1 : 2)
            .ThenBy(a => a.IsOver == false ? (a.EndTime - DateTime.UtcNow).TotalSeconds : 0)
            .ThenBy(a => a.IsOver == null ? (a.StartTime - DateTime.UtcNow).TotalSeconds : 0).Last().Id.ToString();

        var actualLastAuctionId = auctionIndexViewModels.Last().Id;

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(auctionIndexViewModels, Is.Not.Null);
            Assert.That(auctionIndexViewModels, Has.Count.EqualTo(3));
            Assert.That(expectedFirstAuctionId, Is.EqualTo(actualFirstAuctionId));
            Assert.That(expectedLastAuctionId, Is.EqualTo(actualLastAuctionId));
        });
    }

    [Test]
    public async Task GetAllAuctionsByUserIdAsyncReturnsEmptyCollectionIfUserHasNoActiveAuctionsThatAreStarted()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers().First();

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionsRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionsRepoMock.Setup(r => r.All()).Returns(auctionsAsQueryable.Where(x => x.IsDeleted == false));

        this.auctionsRepo = auctionsRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var userActiveAuctionsViewModels = await this.auctionService.GetAllAuctionsByUserIdAsync(testUser.Id.ToString());

        //Assert
        var userActiveAuctionsAsList = userActiveAuctionsViewModels.ToList();

        Assert.Multiple(() =>
        {
            Assert.That(userActiveAuctionsAsList, Is.Not.Null);
            Assert.That(userActiveAuctionsAsList, Is.Empty);
        });
    }

    [Test]
    public async Task GetFavoriteAuctionsByUserIdAsyncReturnCorrectFavoriteAuctionsForUser()
    {
        //Arrange
        var userFavoriteAuctionsArray = GetUserFavoriteAuctions();
        var testUser = GetApplicationUsers()[1];

        var userFavoriteAuctionsAsQueryable = userFavoriteAuctionsArray.AsQueryable().BuildMock();

        var userFavoriteAuctionsRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteAuction>>();
        userFavoriteAuctionsRepoMock.Setup(r => r.All()).Returns(userFavoriteAuctionsAsQueryable.Where(x => x.IsDeleted == false));

        this.userFavoriteAuctionsRepo = userFavoriteAuctionsRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var favoriteAuctions = await this.auctionService.GetFavoriteAuctionsByUserIdAsync(testUser.Id.ToString());

        //Assert
        var favoriteAuctionsAsList = favoriteAuctions.ToList();

        var expectedCount = userFavoriteAuctionsArray.Count(x => x.UserId == testUser.Id);
        var actualCount = favoriteAuctionsAsList.Count;

        var userIsCreatorOfFavoriteAuction = favoriteAuctionsAsList.Any(x => x.CreatorId == testUser.Id.ToString());

        Assert.Multiple(() =>
        {
            Assert.That(favoriteAuctionsAsList, Is.Not.Null);
            Assert.That(favoriteAuctionsAsList, Is.Not.Empty);
            Assert.That(userIsCreatorOfFavoriteAuction, Is.False);
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        });
    }

    [Test]
    public async Task IsAuctionInUserFavoritesByIdAsyncReturnsFalseIfAuctionIsStillNotInUserFavorites()
    {
        //Arrange
        var userFavoriteAuctionsArray = GetUserFavoriteAuctions();
        var testUser = GetApplicationUsers()[1];
        var testAuction = GetAuctions()[2];

        var userFavoriteAuctionsAsQueryable = userFavoriteAuctionsArray.AsQueryable().BuildMock();

        var userFavoriteAuctionsRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteAuction>>();
        userFavoriteAuctionsRepoMock.Setup(r => r.All()).Returns(userFavoriteAuctionsAsQueryable.Where(x => x.IsDeleted == false));

        this.userFavoriteAuctionsRepo = userFavoriteAuctionsRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var result = await this.auctionService.IsAuctionInUserFavoritesByIdAsync(testAuction.Id.ToString(), testUser.Id.ToString());

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsAuctionInUserFavoritesByIdAsyncReturnsTrueIfAuctionIsInUserFavorites()
    {
        //Arrange
        var userFavoriteAuctionsArray = GetUserFavoriteAuctions();
        var testUser = GetApplicationUsers()[1];
        var testAuction = GetAuctions()[1];

        var userFavoriteAuctionsAsQueryable = userFavoriteAuctionsArray.AsQueryable().BuildMock();

        var userFavoriteAuctionsRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteAuction>>();
        userFavoriteAuctionsRepoMock.Setup(r => r.All()).Returns(userFavoriteAuctionsAsQueryable.Where(x => x.IsDeleted == false));

        this.userFavoriteAuctionsRepo = userFavoriteAuctionsRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var result = await this.auctionService.IsAuctionInUserFavoritesByIdAsync(testAuction.Id.ToString(), testUser.Id.ToString());

        Assert.That(result, Is.True);
    }

    [Test]
    public void FavoriteAuctionByIdAsyncThrowsInvalidOperationExceptionIfAuctionIsInUserFavorites()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers()[1];
        var testAuction = GetAuctions()[1];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionsRepoMock =
            new Mock<IDeletableEntityRepository<Auction>>();
        auctionsRepoMock.Setup(r => r.All()).Returns(auctionsAsQueryable.Where(x => x.IsDeleted == false));

        this.auctionsRepo = auctionsRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.auctionService.FavoriteAuctionByIdAsync(testAuction.Id.ToString(), testUser.Id.ToString());
        }, AuctionIsAlreadyInCurrentUserFavorites);
    }

    [Test]
    public async Task FavoriteAuctionByIdAsyncAddsAuctionToUserFavoritesIfAuctionIsNotInUserFavorites()
    {
        //Arrange
        var auctionArray = GetAuctions();
        var userFavoriteAuctionsList = GetUserFavoriteAuctions().ToList();
        var testUser = GetApplicationUsers()[1];
        var testAuction = GetAuctions()[2];

        var userFavoriteAuctionsAsQueryable = userFavoriteAuctionsList.AsQueryable().BuildMock();

        var userFavoriteAuctionsRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteAuction>>();

        userFavoriteAuctionsRepoMock.Setup(r => r.All()).Returns(userFavoriteAuctionsAsQueryable.Where(x => x.IsDeleted == false));

        userFavoriteAuctionsRepoMock.Setup(r => r.AddAsync(It.IsAny<UserFavoriteAuction>()))
            .Callback((UserFavoriteAuction x) => userFavoriteAuctionsList.Add(x));

        var auctionsAsQueryable = auctionArray.AsQueryable().BuildMock();

        var auctionsRepoMock =
            new Mock<IDeletableEntityRepository<Auction>>();
        auctionsRepoMock.Setup(r => r.All()).Returns(auctionsAsQueryable.Where(x => x.IsDeleted == false));

        this.auctionsRepo = auctionsRepoMock.Object;

        this.userFavoriteAuctionsRepo = userFavoriteAuctionsRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var userFavoriteCount = userFavoriteAuctionsList.Count(x => x.UserId == testUser.Id && x.IsDeleted == false);

        await this.auctionService.FavoriteAuctionByIdAsync(testAuction.Id.ToString(), testUser.Id.ToString());

        //Assert
        var expectedCount = userFavoriteCount + 1;
        var actualCount = userFavoriteAuctionsList.Count;

        Assert.That(expectedCount, Is.EqualTo(actualCount));
    }

    [Test]
    public void FavoriteAuctionByIdAsyncThrowsExceptionIfAuctionIsNotExisting()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers()[1];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionsRepoMock =
            new Mock<IDeletableEntityRepository<Auction>>();
        auctionsRepoMock.Setup(r => r.All()).Returns(auctionsAsQueryable.Where(x => x.IsDeleted == false));

        this.auctionsRepo = auctionsRepoMock.Object;

        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Act & Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.auctionService.FavoriteAuctionByIdAsync("notExistingIdOfAuction", testUser.Id.ToString());
        });
    }

    [Test]
    public async Task GetFavoritesCountForAuctionByIdAsyncReturnsCorrectCount()
    {
        //Arrange
        var userFavoriteAuctionsList = GetUserFavoriteAuctions().ToList();
        var testAuction = GetAuctions()[1];

        var userFavoriteAuctionsAsQueryable = userFavoriteAuctionsList.AsQueryable().BuildMock();

        var userFavoriteAuctionRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteAuction>>();

        userFavoriteAuctionRepoMock.Setup(r => r.AllAsNoTracking()).Returns(userFavoriteAuctionsAsQueryable.Where(x => x.IsDeleted == false));

        this.userFavoriteAuctionsRepo = userFavoriteAuctionRepoMock.Object;

        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Act
        var count = await this.auctionService.GetFavoritesCountForAuctionByIdAsync(testAuction.Id.ToString());

        //Assert
        var expectedCount = userFavoriteAuctionsList.Count(x => x.AuctionId == testAuction.Id);

        Assert.Multiple(() =>
        {
            Assert.That(count, Is.Not.Zero);
            Assert.That(expectedCount, Is.EqualTo(count));
        });
    }

    [Test]
    public async Task UnFavoriteAuctionByIdAsyncRemovesAuctionFromUserFavoritesIfItExists()
    {
        //Arrange
        var userFavoriteAuctionList = GetUserFavoriteAuctions().ToList();
        var testUser = GetApplicationUsers()[1];
        var testAuction = GetAuctions()[1];

        var userFavoriteAuctionsAsQueryable = userFavoriteAuctionList.AsQueryable().BuildMock();

        var userFavoriteAuctionsRepoMock = new Mock<IDeletableEntityRepository<UserFavoriteAuction>>();

        userFavoriteAuctionsRepoMock.Setup(r => r.All()).Returns(userFavoriteAuctionsAsQueryable.Where(x => x.IsDeleted == false));

        userFavoriteAuctionsRepoMock.Setup(r => r.HardDelete(It.IsAny<UserFavoriteAuction>()))
            .Callback((UserFavoriteAuction x) => userFavoriteAuctionList.Remove(x));

        this.userFavoriteAuctionsRepo = userFavoriteAuctionsRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var userFavoriteCount = userFavoriteAuctionList.Count(x => x.UserId == testUser.Id);

        await this.auctionService.UnFavoriteAuctionByIdAsync(testAuction.Id.ToString(), testUser.Id.ToString());

        //Assert
        var expectedCount = userFavoriteCount - 1;
        var actualCount = userFavoriteAuctionList.Count;

        var auctionExistsAfterRemove = userFavoriteAuctionList.Any(x => x.AuctionId == testAuction.Id);

        Assert.Multiple(() =>
        {
            Assert.That(actualCount, Is.Not.Zero);
            Assert.That(auctionExistsAfterRemove, Is.False);
            Assert.That(expectedCount, Is.EqualTo(actualCount));
        });
    }

    [Test]
    public async Task GetAllDeactivatedAuctionsByUserIdAsyncReturnsCorrectDeactivatedAuctions()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers()[1];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsAsQueryable.Where(l => l.IsDeleted));

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        var deactivatedAuctions = await this.auctionService.GetAllDeactivatedAuctionsByUserIdAsync(testUser.Id.ToString());

        //Assert
        var resultAsList = deactivatedAuctions.ToList();

        var expectedCount = auctionsArray.Count(x => x.IsDeleted && x.CreatorId == testUser.Id);
        var actualCount = resultAsList.Count;

        Assert.That(resultAsList, Is.Not.Null);
        Assert.That(resultAsList, Is.Not.Empty);
        Assert.That(actualCount, Is.EqualTo(expectedCount));
    }

    [Test]
    public void GetAuctionEditAsyncThrowsInvalidDataExceptionWhenAuctionDoesNotExist()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers()[1];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsAsQueryable);

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        Assert.ThrowsAsync<InvalidDataException>(async () =>
        {
            await this.auctionService.GetAuctionEditAsync("AuctionIdDoesNotExist", testUser.Id.ToString(), false);
        }, AuctionDoesNotExistError);
    }

    [Test]
    public void GetAuctionEditAsyncThrowsInvalidDataExceptionWhenUserIsNotCreatorNorAdmin()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testAuction = auctionsArray[0];
        var testUser = GetApplicationUsers()[1];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsAsQueryable);

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Assert
        Assert.ThrowsAsync<InvalidDataException>(async () =>
        {
            await this.auctionService.GetAuctionEditAsync(testAuction.Id.ToString(), testUser.Id.ToString(), false);
        }, AuctionDoesNotExistError);
    }

    [Test]
    public void EditAuctionAsyncThrowsExceptionWhenFormIsEmpty()
    {
        var testUser = GetApplicationUsers()[0];

        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            var emptyForm = new AuctionEditFormModel();
            await this.auctionService.EditAuctionAsync(emptyForm, testUser.Id.ToString(), false);
        });
    }

    [Test]
    public async Task GetAuctionDetailsAsyncReturnsCorrectDeactivatedAuctionWhenUserIsCreator()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testAuction = auctionsArray[3];
        var testUser = GetApplicationUsers()[1];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsAsQueryable);

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Assert
        var deactivatedAuction = await this.auctionService.GetAuctionDetailsAsync(testAuction.Id.ToString(), testUser.Id.ToString(), false);

        var expectedAuctionId = auctionsArray.First(x => x.Id == testAuction.Id && x.IsDeleted).Id.ToString();
        var actualAuctionId = deactivatedAuction.Id;

        Assert.Multiple(() =>
        {
            Assert.That(deactivatedAuction, Is.Not.Null);
            Assert.That(expectedAuctionId, Is.EqualTo(actualAuctionId));
        });
    }

    [Test]
    public async Task GetAuctionDetailsAsyncReturnsCorrectDeactivatedAuctionWhenUserIsAdmin()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testAuction = auctionsArray[3];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsAsQueryable);

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Assert
        var deactivatedAuction = await this.auctionService.GetAuctionDetailsAsync(testAuction.Id.ToString(), "AdminId", true);

        var expectedAuctionId = auctionsArray.First(x => x.Id == testAuction.Id && x.IsDeleted).Id.ToString();
        var actualAuctionId = deactivatedAuction.Id;

        Assert.Multiple(() =>
        {
            Assert.That(deactivatedAuction, Is.Not.Null);
            Assert.That(expectedAuctionId, Is.EqualTo(actualAuctionId));
        });
    }

    [Test]
    public void GetAuctionDetailsAsyncThrowsExceptionWhenAuctionDoesNotExist()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers()[1];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsAsQueryable);

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.auctionService.GetAuctionDetailsAsync("AuctionIdDoesNotExist", testUser.Id.ToString(), false);
        });
    }

    [Test]
    public async Task GetChooseThumbnailAsyncGetsAuctionImagesInAChooseThumbnailFormModelIfAuctionExistsAndUserIsCreator()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testAuction = auctionsArray[1];
        var testUser = GetApplicationUsers()[0];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsAsQueryable);

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Assert
        var chooseThumbnailViewModel =
            await this.auctionService.GetChooseThumbnailAsync(testAuction.Id.ToString(), testUser.Id.ToString(), false);

        var expectedFirstImageId = testAuction.Images.First().Id.ToString();
        var actualFirstImageId = chooseThumbnailViewModel.Images.First().Id;

        var expectedFirstImageUrl = testAuction.Images.First().Url;
        var actualFirstImageUrl = chooseThumbnailViewModel.Images.First().Url;

        Assert.Multiple(() =>
        {
            Assert.That(chooseThumbnailViewModel, Is.Not.Null);
            Assert.That(expectedFirstImageId, Is.EqualTo(actualFirstImageId));
            Assert.That(expectedFirstImageUrl, Is.EqualTo(actualFirstImageUrl));
        });
    }

    [Test]
    public void GetChooseThumbnailAsyncThrowsExceptionWhenAuctionDoesNotExist()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers()[0];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsAsQueryable);

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.auctionService.GetChooseThumbnailAsync("ListingIdDoesNotExist", testUser.Id.ToString(), false);
        });
    }

    [Test]
    public void GetChooseThumbnailAsyncThrowsExceptionWhenTheUserIsNotCreatorNorAdmin()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers()[1];
        var testAuction = auctionsArray[0];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsAsQueryable);

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.auctionService.GetChooseThumbnailAsync(testAuction.Id.ToString(), testUser.Id.ToString(), false);
        });
    }

    [Test]
    public async Task DeactivateAuctionByIdAsyncSetsIsDeletedToTrueIfAuctionExistsHasNotStartedAndUserIsCreator()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers()[0];
        var testUpcomingAuction = auctionsArray[0];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();

        auctionRepoMock.Setup(r => r.All()).Returns(auctionsAsQueryable.Where(x => x.IsDeleted == false));

        auctionRepoMock.Setup(r => r.Delete(It.IsAny<Auction>())).Callback((Auction auction) =>
        {
            var auctionToSoftDelete = auctionsArray.First(x => x.Id == auction.Id);

            auctionToSoftDelete.IsDeleted = true;
            auctionToSoftDelete.DeletedOn = DateTime.UtcNow;
        });

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        await this.auctionService.DeactivateAuctionByIdAsync(testUpcomingAuction.Id.ToString(), testUser.Id.ToString(),
            false);

        //Assert
        var softDeletedStateOfUpcomingAuction = testUpcomingAuction.IsDeleted;

        Assert.Multiple(() =>
        {
            Assert.That(softDeletedStateOfUpcomingAuction, Is.True);
            Assert.That(testUpcomingAuction.DeletedOn, Is.Not.Null);
        });
    }

    [Test]
    public async Task DeactivateAuctionByIdAsyncSetsIsDeletedToTrueIfAuctionExistsHasNotStartedAndUserIsAdmin()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUpcomingAuction = auctionsArray[0];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();

        auctionRepoMock.Setup(r => r.All()).Returns(auctionsAsQueryable.Where(x => x.IsDeleted == false));

        auctionRepoMock.Setup(r => r.Delete(It.IsAny<Auction>())).Callback((Auction auction) =>
        {
            var auctionToSoftDelete = auctionsArray.First(x => x.Id == auction.Id);

            auctionToSoftDelete.IsDeleted = true;
            auctionToSoftDelete.DeletedOn = DateTime.UtcNow;
        });

        this.auctionsRepo = auctionRepoMock.Object;

        //Act
        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        await this.auctionService.DeactivateAuctionByIdAsync(testUpcomingAuction.Id.ToString(), "AdminId",
            true);

        //Assert
        var softDeletedStateOfUpcomingAuction = testUpcomingAuction.IsDeleted;

        Assert.Multiple(() =>
        {
            Assert.That(softDeletedStateOfUpcomingAuction, Is.True);
            Assert.That(testUpcomingAuction.DeletedOn, Is.Not.Null);
        });
    }

    [Test]
    public void DeactivateAuctionByIdAsyncThrowsExceptionWhenAuctionDoesNotExistOrIsSoftDeleted()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers()[1];
        var testAuction = auctionsArray[3];

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();

        auctionRepoMock.Setup(r => r.All()).Returns(auctionsAsQueryable.Where(x => x.IsDeleted == false));

        this.auctionsRepo = auctionRepoMock.Object;

        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Act & Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.auctionService.DeactivateAuctionByIdAsync(testAuction.Id.ToString(), testUser.Id.ToString(),
                false);
        });
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.auctionService.DeactivateAuctionByIdAsync("AuctionIdDoesNotExist", testUser.Id.ToString(),
                false);
        });
    }

    [Test]
    public void AddThumbnailToAuctionByIdAsyncThrowsExceptionWhenAuctionDoesNotExist()
    {
        //Arrange
        var testUser = GetApplicationUsers()[0];
        var testAuction = GetAuctions().First();
        var newThumbnailId = testAuction.Images.Last().Id.ToString();
        var auctionCarImages = GetAuctionCarImages();

        var auctionCarImagesAsQueryable = auctionCarImages.AsQueryable().BuildMock();

        var auctionCarImageRepoMock = new Mock<IDeletableEntityRepository<AuctionCarImage>>();
        auctionCarImageRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionCarImagesAsQueryable);

        this.auctionCarImagesRepo = auctionCarImageRepoMock.Object;

        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.auctionService.AddThumbnailToAuctionByIdAsync("NotExistingAuction", newThumbnailId, testUser.Id.ToString(), false);
        });
    }

    [Test]
    public void AddThumbnailToAuctionByIdAsyncThrowsExceptionWhenImageIdDoesNotExist()
    {
        //Arrange
        var testUser = GetApplicationUsers()[0];
        var testAuction = GetAuctions().First();
        var auctionCarImages = GetAuctionCarImages();

        var auctionImagesAsQueryable = auctionCarImages.AsQueryable().BuildMock();

        var auctionCarImagesRepoMock = new Mock<IDeletableEntityRepository<AuctionCarImage>>();
        auctionCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionImagesAsQueryable);

        this.auctionCarImagesRepo = auctionCarImagesRepoMock.Object;

        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.auctionService.AddThumbnailToAuctionByIdAsync(testAuction.Id.ToString(), "notExistingImageId", testUser.Id.ToString(), false);
        });
    }

    [Test]
    public void AddThumbnailToAuctionByIdAsyncThrowsExceptionWhenUserIsNotCreatorNorAdmin()
    {
        //Arrange
        var testUser = GetApplicationUsers()[1];
        var testAuction = GetAuctions().First();
        var newThumbnailId = testAuction.Images.Last().Id.ToString();
        var auctionCarImages = GetAuctionCarImages();

        var auctionImagesAsQueryable = auctionCarImages.AsQueryable().BuildMock();

        var listingCarImagesRepoMock = new Mock<IDeletableEntityRepository<AuctionCarImage>>();
        listingCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionImagesAsQueryable);

        this.auctionCarImagesRepo = listingCarImagesRepoMock.Object;

        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.auctionService.AddThumbnailToAuctionByIdAsync(testAuction.Id.ToString(), newThumbnailId, testUser.Id.ToString(), false);
        });
    }

    [Test]
    public async Task AddThumbnailToAuctionByIdAsyncChangesThumbnailWhenUserIsCreator()
    {
        //Arrange
        var auctionsArray = GetAuctions();
        var testUser = GetApplicationUsers()[0];
        var testAuction = GetAuctions().First();
        var newThumbnailId = testAuction.Images.Last().Id.ToString();
        var auctionCarImages = GetAuctionCarImages();

        var auctionCarImagesAsQueryable = auctionCarImages.AsQueryable().BuildMock();

        var auctionCarImagesRepoMock = new Mock<IDeletableEntityRepository<AuctionCarImage>>();
        auctionCarImagesRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionCarImagesAsQueryable);

        var auctionsAsQueryable = auctionsArray.AsQueryable().BuildMock();

        var auctionRepoMock = new Mock<IDeletableEntityRepository<Auction>>();
        auctionRepoMock.Setup(r => r.AllWithDeleted()).Returns(auctionsAsQueryable);

        this.auctionsRepo = auctionRepoMock.Object;

        this.auctionCarImagesRepo = auctionCarImagesRepoMock.Object;

        this.auctionService = new AuctionService(autoMapper, this.htmlSanitizer, this.carService, this.auctionImageService, this.auctionsRepo, this.auctionCarImagesRepo, this.auctionFeaturesRepo, this.userFavoriteAuctionsRepo, this.bidsRepo, this.emailSender);

        //Act
        await this.auctionService.AddThumbnailToAuctionByIdAsync(testAuction.Id.ToString(), newThumbnailId, testUser.Id.ToString(), false);

        //Assert
        var changedListing = await auctionsAsQueryable.FirstAsync(x => x.Id == testAuction.Id);

        var expectedThumbnailId = newThumbnailId;
        var actualThumbnailId = changedListing.ThumbnailId.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(testAuction.Thumbnail, Is.Not.Null);
            Assert.That(testAuction.ThumbnailId, Is.Not.Null);
            Assert.That(expectedThumbnailId, Is.EqualTo(actualThumbnailId));
        });
    }
}