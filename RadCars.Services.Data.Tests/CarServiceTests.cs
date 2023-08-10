namespace RadCars.Services.Data.Tests;

using System.Reflection;

using Web.ViewModels.Home;

using static CarData;
using static Mapping.AutoMapperConfig;

public class CarServiceTests
{
    private ICarService carService = null!;

    private IDeletableEntityRepository<City> citiesRepository = null!;
    private IDeletableEntityRepository<CarMake> carMakesRepository = null!;
    private IDeletableEntityRepository<Feature> featuresRepository = null!;
    private IDeletableEntityRepository<CarModel> carModelsRepository = null!;
    private IDeletableEntityRepository<Category> categoriesRepository = null!;
    private IDeletableEntityRepository<EngineType> engineTypesRepository = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }

    [SetUp]
    public void SetUp()
    {
        this.citiesRepository = new Mock<IDeletableEntityRepository<City>>().Object;
        this.carMakesRepository = new Mock<IDeletableEntityRepository<CarMake>>().Object;
        this.featuresRepository = new Mock<IDeletableEntityRepository<Feature>>().Object;
        this.carModelsRepository = new Mock<IDeletableEntityRepository<CarModel>>().Object;
        this.categoriesRepository = new Mock<IDeletableEntityRepository<Category>>().Object;
        this.engineTypesRepository = new Mock<IDeletableEntityRepository<EngineType>>().Object;
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task GetModelsByMakeIdAsyncGetsTheCorrectModelsIfMakeIdExists(int carMakeId)
    {
        //Arrange
        var testCarMakes = GetCarMakes();
        var expectedModels = testCarMakes.First(x => x.Id == carMakeId).Models.ToArray();
        var testModels = GetCarModels();

        var carModelsAsQueryable = testModels.AsQueryable().BuildMock();

        var carModelsRepoMock = new Mock<IDeletableEntityRepository<CarModel>>();
        carModelsRepoMock.Setup(r => r.AllAsNoTracking()).Returns(carModelsAsQueryable.Where(x => x.IsDeleted == false));

        this.carModelsRepository = carModelsRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var modelsViewModels = await this.carService.GetModelsByMakeIdAsync(carMakeId);

        //Assert
        var modelsViewModelsAsList = modelsViewModels.ToList();

        var expectedFirstModelId = expectedModels.First().Id;
        var actualFirstModelId = modelsViewModelsAsList.First().Id;

        var expectedLastModelId = expectedModels.Last().Id;
        var actualLastModelId = modelsViewModelsAsList.Last().Id;

        Assert.Multiple(() =>
        {
            Assert.That(modelsViewModelsAsList, Is.Not.Null);
            Assert.That(modelsViewModelsAsList, Is.Not.Empty);
            Assert.That(expectedModels, Has.Length.EqualTo(modelsViewModelsAsList.Count));
            Assert.That(expectedLastModelId, Is.EqualTo(actualLastModelId));
            Assert.That(expectedFirstModelId, Is.EqualTo(actualFirstModelId));
        });
    }

    [Test]
    public async Task GetCarMakesAsyncGetsReturnsAllCarMakes()
    {
        //Arrange
        var testCarMakes = GetCarMakes();

        var expectedCarMakes = testCarMakes.Where(x => x.IsDeleted == false).ToArray();

        var carMakesAsQueryable = testCarMakes.AsQueryable().BuildMock();

        var carMakesRepoMock = new Mock<IDeletableEntityRepository<CarMake>>();
        carMakesRepoMock.Setup(r => r.AllAsNoTracking()).Returns(carMakesAsQueryable.Where(x => x.IsDeleted == false));

        this.carMakesRepository = carMakesRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var carMakesViewModels = await this.carService.GetCarMakesAsync();

        var carMakesViewModelAsList = carMakesViewModels.ToList();

        Assert.Multiple(() =>
        {
            Assert.That(carMakesViewModelAsList, Is.Not.Null);
            Assert.That(carMakesViewModelAsList, Is.Not.Empty);
            Assert.That(expectedCarMakes, Has.Length.EqualTo(carMakesViewModelAsList.Count));
        });
    }

    [Test]
    public async Task GetFeaturesWithCategoriesAsyncReturnsAllCategoriesWithTheirFeatures()
    {
        //Arrange
        var testCategories = GetCategories();

        var expectedCategories = testCategories.Where(x => x.IsDeleted == false).ToArray();

        var categoriesAsQueryable = testCategories.AsQueryable().BuildMock();

        var categoriesRepoMock = new Mock<IDeletableEntityRepository<Category>>();
        categoriesRepoMock.Setup(r => r.AllAsNoTracking()).Returns(categoriesAsQueryable.Where(x => x.IsDeleted == false));

        this.categoriesRepository = categoriesRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var categoriesViewModels = await this.carService.GetFeaturesWithCategoriesAsync();

        var categoriesViewModelsToLists = categoriesViewModels.ToList();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(categoriesViewModelsToLists, Is.Not.Null);
            Assert.That(categoriesViewModelsToLists, Is.Not.Empty);
            Assert.That(expectedCategories, Has.Length.EqualTo(categoriesViewModelsToLists.Count));
        });
    }

    [Test]
    public async Task GetBulgarianCitiesAsyncReturnsAllCitiesInBulgaria()
    {
        //Arrange
        var citiesArray = GetCities();

        var expectedCities = citiesArray.Where(x => x.IsDeleted == false).ToArray();

        var citiesAsQueryable = citiesArray.AsQueryable().BuildMock();

        var citiesRepoMock = new Mock<IDeletableEntityRepository<City>>();
        citiesRepoMock.Setup(r => r.AllAsNoTracking()).Returns(citiesAsQueryable.Where(x => x.IsDeleted == false));

        this.citiesRepository = citiesRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);
        
        //Act
        var citiesViewModels = await this.carService.GetBulgarianCitiesAsync();

        var citiesViewModelsToList = citiesViewModels.ToList();

        Assert.Multiple(() =>
        {
            Assert.That(citiesViewModelsToList, Is.Not.Null);
            Assert.That(citiesViewModelsToList, Is.Not.Empty);
            Assert.That(expectedCities, Has.Length.EqualTo(citiesViewModelsToList.Count));
        });
    }

    [Test]
    public async Task GetEngineTypesAsyncReturnsAllEngineTypes()
    {
        //Arrange
        var engineTypes = GetEngineTypes();

        var expectedEngineTypes = engineTypes.Where(x => x.IsDeleted == false).ToArray();

        var engineTypesAsQueryable = engineTypes.AsQueryable().BuildMock();

        var engineTypesRepoMock = new Mock<IDeletableEntityRepository<EngineType>>();
        engineTypesRepoMock.Setup(r => r.AllAsNoTracking())
            .Returns(engineTypesAsQueryable.Where(x => x.IsDeleted == false));

        this.engineTypesRepository = engineTypesRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var engineTypesViewModels = await this.carService.GetEngineTypesAsync();

        var engineTypesToList = engineTypesViewModels.ToList();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(engineTypesToList, Is.Not.Null);
            Assert.That(engineTypesToList, Is.Not.Empty);
            Assert.That(expectedEngineTypes, Has.Length.EqualTo(engineTypesToList.Count));
        });
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task CarMakeIdExistsAsyncReturnsTrueIfCarMakeExists(int carMakeId)
    {
        //Arrange
        var carMakes = GetCarMakes();

        var carMakesAsQueryable = carMakes.AsQueryable().BuildMock();

        var carMakesRepoMock = new Mock<IDeletableEntityRepository<CarMake>>();
        carMakesRepoMock.Setup(r => r.All())
            .Returns(carMakesAsQueryable.Where(x => x.IsDeleted == false));

        this.carMakesRepository = carMakesRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var result = await this.carService.CarMakeIdExistsAsync(carMakeId);

        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(int.MinValue)]
    public async Task CarMakeIdExistsAsyncReturnsFalseIfCarMakeDoesNotExist(int carMakeId)
    {
        //Arrange
        var carMakes = GetCarMakes();

        var carMakesAsQueryable = carMakes.AsQueryable().BuildMock();

        var carMakesRepoMock = new Mock<IDeletableEntityRepository<CarMake>>();
        carMakesRepoMock.Setup(r => r.All())
            .Returns(carMakesAsQueryable.Where(x => x.IsDeleted == false));

        this.carMakesRepository = carMakesRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var result = await this.carService.CarMakeIdExistsAsync(carMakeId);

        Assert.That(result, Is.False);
    }

    [Test]
    [TestCase(1, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(2, 4)]
    [TestCase(3, 5)]
    public async Task CarModelIdExistsByMakeIdAsyncReturnsTrueIfCarMakeExists(int carMakeId, int carModelId)
    {
        //Arrange
        var carModels = GetCarModels();

        var carModelsAsQueryable = carModels.AsQueryable().BuildMock();

        var carModelsRepoMock = new Mock<IDeletableEntityRepository<CarModel>>();
        carModelsRepoMock.Setup(r => r.AllAsNoTracking())
            .Returns(carModelsAsQueryable.Where(x => x.IsDeleted == false));

        this.carModelsRepository = carModelsRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var result = await this.carService.CarModelIdExistsByMakeIdAsync(carMakeId, carModelId);

        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase(1, 0)]
    [TestCase(0, int.MinValue)]
    [TestCase(int.MinValue, int.MinValue)]
    public async Task CarModelIdExistsByMakeIdAsyncReturnsFalseIfCarMakeDoesNotExist(int carMakeId, int carModelId)
    {
        //Arrange
        var carModels = GetCarModels();

        var carModelsAsQueryable = carModels.AsQueryable().BuildMock();

        var carModelsRepoMock = new Mock<IDeletableEntityRepository<CarModel>>();
        carModelsRepoMock.Setup(r => r.AllAsNoTracking())
            .Returns(carModelsAsQueryable.Where(x => x.IsDeleted == false));

        this.carModelsRepository = carModelsRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var result = await this.carService.CarModelIdExistsByMakeIdAsync(carMakeId, carModelId);

        Assert.That(result, Is.False);
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    public async Task CarEngineTypeIdExistsAsyncReturnsTrueIfEngineTypeExists(int engineTypeId)
    {
        //Arrange
        var engineTypes = GetEngineTypes();

        var engineTypesAsQueryable = engineTypes.AsQueryable().BuildMock();

        var engineTypesRepoMock = new Mock<IDeletableEntityRepository<EngineType>>();
        engineTypesRepoMock.Setup(r => r.All()).Returns(engineTypesAsQueryable.Where(x => x.IsDeleted == false));

        this.engineTypesRepository = engineTypesRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var result = await this.carService.CarEngineTypeIdExistsAsync(engineTypeId);

        //Assert
        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase(0)]
    [TestCase(int.MinValue)]
    public async Task CarEngineTypeIdExistsAsyncReturnsFalseIfEngineTypeDoesNotExist(int engineTypeId)
    {
        //Arrange
        var engineTypes = GetEngineTypes();

        var engineTypesAsQueryable = engineTypes.AsQueryable().BuildMock();

        var engineTypesRepoMock = new Mock<IDeletableEntityRepository<EngineType>>();
        engineTypesRepoMock.Setup(r => r.All()).Returns(engineTypesAsQueryable.Where(x => x.IsDeleted == false));

        this.engineTypesRepository = engineTypesRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var result = await this.carService.CarEngineTypeIdExistsAsync(engineTypeId);

        //Assert
        Assert.That(result, Is.False);
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    public async Task CityIdExistsAsyncReturnsTrueIfCityExists(int cityId)
    {
        //Arrange
        var cities = GetCities();

        var citiesAsQueryable = cities.AsQueryable().BuildMock();

        var citiesRepoMock = new Mock<IDeletableEntityRepository<City>>();
        citiesRepoMock.Setup(r => r.All()).Returns(citiesAsQueryable.Where(x => x.IsDeleted == false));

        this.citiesRepository = citiesRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var result = await this.carService.CityIdExistsAsync(cityId);

        //Assert
        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase(0)]
    [TestCase(int.MinValue)]
    public async Task CityIdExistsAsyncReturnsFalseIfCityDoesNotExist(int cityId)
    {
        //Arrange
        var cities = GetCities();

        var citiesAsQueryable = cities.AsQueryable().BuildMock();

        var citiesRepoMock = new Mock<IDeletableEntityRepository<City>>();
        citiesRepoMock.Setup(r => r.All()).Returns(citiesAsQueryable.Where(x => x.IsDeleted == false));

        this.citiesRepository = citiesRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var result = await this.carService.CityIdExistsAsync(cityId);

        //Assert
        Assert.That(result, Is.False);
    }

    [Test]
    [TestCase(new int[]{ 1, 2, 3, 4 })]
    public async Task SelectedFeaturesIdsExistReturnsTrueIfFeaturesExist(IEnumerable<int> selectedFeatures)
    {
        //Arrange
        var features = GetFeatures();

        var featuresAsQueryable = features.AsQueryable().BuildMock();

        var featuresRepoMock = new Mock<IDeletableEntityRepository<Feature>>();
        featuresRepoMock.Setup(r => r.All()).Returns(featuresAsQueryable);

        this.featuresRepository = featuresRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var result = await this.carService.SelectedFeaturesIdsExist(selectedFeatures);

        //Assert
        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase(new int[] { int.MinValue, 1, 2, 3 })]
    [TestCase(new int[] { int.MinValue, 1, 0, -1 })]
    [TestCase(new int[] { int.MinValue, int.MaxValue, 0, -1 })]
    public async Task SelectedFeaturesIdsExistReturnsFalseIfFeaturesDoNotExist(IEnumerable<int> selectedFeatures)
    {
        //Arrange
        var features = GetFeatures();

        var featuresAsQueryable = features.AsQueryable().BuildMock();

        var featuresRepoMock = new Mock<IDeletableEntityRepository<Feature>>();
        featuresRepoMock.Setup(r => r.All()).Returns(featuresAsQueryable);

        this.featuresRepository = featuresRepoMock.Object;

        this.carService = new CarService(this.carModelsRepository, this.carMakesRepository, this.citiesRepository,
            this.engineTypesRepository, this.categoriesRepository, this.featuresRepository);

        //Act
        var result = await this.carService.SelectedFeaturesIdsExist(selectedFeatures);

        //Assert
        Assert.That(result, Is.False);
    }
}