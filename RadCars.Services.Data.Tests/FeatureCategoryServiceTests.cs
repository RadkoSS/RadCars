namespace RadCars.Services.Data.Tests;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Feature;
using Web.ViewModels.FeatureCategory;
using Web.ViewModels.Home;

using static CarData;

public class FeatureCategoryServiceTests
{
    private IFeatureCategoryService featureCategoryService = null!;

    private IDeletableEntityRepository<Feature> featuresRepo = null!;
    private IDeletableEntityRepository<Category> categoriesRepo = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }

    [SetUp]
    public void SetUp()
    {
        this.featuresRepo = new Mock<IDeletableEntityRepository<Feature>>().Object;
        this.categoriesRepo = new Mock<IDeletableEntityRepository<Category>>().Object;
    }

    [Test]
    public async Task GetCategoriesAsyncReturnsAllCategories()
    {
        //Arrange
        var testCategories = GetCategories();

        var categoriesAsQueryable = testCategories.AsQueryable().BuildMock();

        var categoriesRepoMock = new Mock<IDeletableEntityRepository<Category>>();
        categoriesRepoMock.Setup(r => r.AllWithDeleted()).Returns(categoriesAsQueryable);

        this.categoriesRepo = categoriesRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act
        var categoriesViewModels = await this.featureCategoryService.GetCategoriesAsync();

        var categoriesViewModelsToLists = categoriesViewModels.ToList();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(categoriesViewModelsToLists, Is.Not.Null);
            Assert.That(categoriesViewModelsToLists, Is.Not.Empty);
            Assert.That(testCategories, Has.Length.EqualTo(categoriesViewModelsToLists.Count));
        });
    }

    [Test]
    public async Task EditCategoryAsyncEditsTheCategory()
    {
        //Arrange
        var testCategories = GetCategories();

        var categoriesAsQueryable = testCategories.AsQueryable().BuildMock();

        var categoriesRepoMock = new Mock<IDeletableEntityRepository<Category>>();
        categoriesRepoMock.Setup(r => r.AllWithDeleted()).Returns(categoriesAsQueryable);

        this.categoriesRepo = categoriesRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act
        var categoryToEdit = await categoriesAsQueryable.FirstAsync();

        const string expectedName = "New name";

        var editForm = new FeatureCategoryFormModel
        {
            Id = categoryToEdit.Id,
            Name = expectedName
        };

        await this.featureCategoryService.EditCategoryAsync(editForm);

        //Assert
        Assert.That(categoryToEdit.Name, Is.EqualTo(expectedName));
    }

    [Test]
    [TestCase(1)]
    public async Task GetCategoryEditAsyncReturnsFormModelIfCategoryExists(int categoryId)
    {
        //Arrange
        var testCategories = GetCategories();

        var categoriesAsQueryable = testCategories.AsQueryable().BuildMock();

        var categoriesRepoMock = new Mock<IDeletableEntityRepository<Category>>();
        categoriesRepoMock.Setup(r => r.AllWithDeleted()).Returns(categoriesAsQueryable);

        this.categoriesRepo = categoriesRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act
        var expectedCategory = testCategories.First(c => c.Id == categoryId);

        var formModel = await this.featureCategoryService.GetCategoryEditAsync(categoryId);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(formModel, Is.Not.Null);
            Assert.That(expectedCategory.Id, Is.EqualTo(formModel.Id));
            Assert.That(expectedCategory.Name, Is.EqualTo(formModel.Name));
        });
    }

    [Test]
    [TestCase(0)]
    [TestCase(int.MinValue)]
    public void GetCategoryEditAsyncThrowsExceptionIfCategoryDoesNotExist(int categoryId)
    {
        //Arrange
        var testCategories = GetCategories();

        var categoriesAsQueryable = testCategories.AsQueryable().BuildMock();

        var categoriesRepoMock = new Mock<IDeletableEntityRepository<Category>>();
        categoriesRepoMock.Setup(r => r.AllWithDeleted()).Returns(categoriesAsQueryable);

        this.categoriesRepo = categoriesRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);
        
        //Act & Assert
        Assert.CatchAsync<Exception>(async () =>
        {
            await this.featureCategoryService.GetCategoryEditAsync(categoryId);
        });
    }

    [Test]
    [TestCase(1)]
    public async Task GetAllFeaturesByCategoryIdAsyncReturnsAllFeaturesIfCategoryExists(int categoryId)
    {
        //Arrange
        var testCategories = GetCategories();

        var categoriesAsQueryable = testCategories.AsQueryable().BuildMock();

        var categoriesRepoMock = new Mock<IDeletableEntityRepository<Category>>();
        categoriesRepoMock.Setup(r => r.AllWithDeleted()).Returns(categoriesAsQueryable);

        this.categoriesRepo = categoriesRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act
        var expectedFeatures = testCategories.First(c => c.Id == categoryId).Features.ToArray();

        var featuresViewModels = await this.featureCategoryService.GetAllFeaturesByCategoryIdAsync(categoryId);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(expectedFeatures, Has.Length.EqualTo(featuresViewModels.FeaturesCount));
        });
    }

    [Test]
    [TestCase("NewFeature", 1)]
    public async Task CreateFeatureAsyncCreatesFeature(string newFeatureName, int categoryId)
    {
        //Arrange
        var features = GetFeatures().ToList();

        var featuresAsQueryable = features.AsQueryable().BuildMock();

        var featuresRepoMock = new Mock<IDeletableEntityRepository<Feature>>();
        featuresRepoMock.Setup(r => r.AllWithDeleted()).Returns(featuresAsQueryable);
        featuresRepoMock.Setup(r => r.AddAsync(It.IsAny<Feature>()))
            .Callback((Feature feature) => features.Add(feature));

        this.featuresRepo = featuresRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act
        var form = new FeatureFormModel { Name = newFeatureName, CategoryId = categoryId };

        var oldCount = features.Count(f => f.CategoryId == categoryId);

        await this.featureCategoryService.CreateFeatureAsync(form);

        var newCount = await featuresAsQueryable.CountAsync(f => f.CategoryId == categoryId);

        Assert.That(newCount, Is.EqualTo(oldCount + 1));
    }

    [Test]
    [TestCase("TeST", 1)]
    [TestCase("TEST", 1)]
    [TestCase("Test", 1)]
    [TestCase("test", 1)]
    public void CreateFeatureAsyncThrowsInvalidOperationExceptionIfFeatureWithThisNameExists(string newFeatureName, int categoryId)
    {
        //Arrange
        var features = GetFeatures().ToList();

        var featuresAsQueryable = features.AsQueryable().BuildMock();

        var featuresRepoMock = new Mock<IDeletableEntityRepository<Feature>>();
        featuresRepoMock.Setup(r => r.AllWithDeleted()).Returns(featuresAsQueryable);

        this.featuresRepo = featuresRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act
        var form = new FeatureFormModel { Name = newFeatureName, CategoryId = categoryId };

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await this.featureCategoryService.CreateFeatureAsync(form);
        });
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task GetFeatureEditByIdAsyncReturnsEditFormModelIfFeatureExists(int featureId)
    {
        //Arrange
        var features = GetFeatures().ToList();
        var featureBeingEdited = features.First(f => f.Id == featureId);

        var featuresAsQueryable = features.AsQueryable().BuildMock();

        var featuresRepoMock = new Mock<IDeletableEntityRepository<Feature>>();
        featuresRepoMock.Setup(r => r.AllWithDeleted()).Returns(featuresAsQueryable);

        this.featuresRepo = featuresRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act
        var formModel = await this.featureCategoryService.GetFeatureEditByIdAsync(featureId);

        Assert.Multiple(() =>
        {
            Assert.That(formModel, Is.Not.Null);
            Assert.That(formModel.Id, Is.EqualTo(featureId));
            Assert.That(formModel.Name, Is.EqualTo(featureBeingEdited.Name));
        });
    }

    [Test]
    [TestCase(1, "newName")]
    public async Task EditFeatureAsyncEditsFeatureIfItExistsAndReturnsCategoryId(int featureId, string newName)
    {
        //Arrange
        var features = GetFeatures().ToList();

        var featuresAsQueryable = features.AsQueryable().BuildMock();

        var featuresRepoMock = new Mock<IDeletableEntityRepository<Feature>>();
        featuresRepoMock.Setup(r => r.AllWithDeleted()).Returns(featuresAsQueryable);

        this.featuresRepo = featuresRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act
        var form = new FeatureFormModel
        {
            Id = featureId,
            Name = newName
        };

        var categoryId = await this.featureCategoryService.EditFeatureAsync(form);

        var editedFeature = await featuresAsQueryable.FirstAsync(f => f.Id == featureId);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(editedFeature.CategoryId, Is.EqualTo(categoryId));
            Assert.That(editedFeature.Name, Is.EqualTo(newName));
        });
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    public async Task SoftDeleteFeatureByIdAsyncIfFeatureExistsAndReturnCategoryId(int featureId)
    {
        //Arrange
        var features = GetFeatures().ToList();

        var featuresAsQueryable = features.AsQueryable().BuildMock();

        var featuresRepoMock = new Mock<IDeletableEntityRepository<Feature>>();
        featuresRepoMock.Setup(r => r.All()).Returns(featuresAsQueryable.Where(x => x.IsDeleted == false));
        featuresRepoMock.Setup(r => r.Delete(It.IsAny<Feature>())).Callback((Feature feature) =>
        {
            feature.IsDeleted = true;
            feature.DeletedOn = DateTime.UtcNow;
        });

        this.featuresRepo = featuresRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act

        var categoryId = await this.featureCategoryService.SoftDeleteFeatureByIdAsync(featureId);

        var editedFeature = await featuresAsQueryable.FirstAsync(f => f.Id == featureId);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(editedFeature.IsDeleted, Is.True);
            Assert.That(editedFeature.DeletedOn, Is.Not.Null);
            Assert.That(editedFeature.CategoryId, Is.EqualTo(categoryId));
        });
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task UnDeleteFeatureByIdAsyncIfFeatureExistsAndReturnCategoryId(int featureId)
    {
        //Arrange
        var features = GetFeatures().ToList();

        var featuresAsQueryable = features.AsQueryable().BuildMock();

        var featuresRepoMock = new Mock<IDeletableEntityRepository<Feature>>();
        featuresRepoMock.Setup(r => r.AllWithDeleted()).Returns(featuresAsQueryable);
        featuresRepoMock.Setup(r => r.Undelete(It.IsAny<Feature>())).Callback((Feature feature) =>
        {
            feature.IsDeleted = false;
            feature.DeletedOn = null;
        });

        this.featuresRepo = featuresRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act

        var categoryId = await this.featureCategoryService.UndeleteFeatureByIdAsync(featureId);

        var editedFeature = await featuresAsQueryable.FirstAsync(f => f.Id == featureId);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(editedFeature.IsDeleted, Is.False);
            Assert.That(editedFeature.DeletedOn, Is.Null);
            Assert.That(editedFeature.CategoryId, Is.EqualTo(categoryId));
        });
    }

    [Test]
    [TestCase("newCategory")]
    [TestCase("newCategory212")]
    public async Task CreateCategoryAsyncCreatesCategoryAndReturnsTheNewId(string newCategoryName)
    {
        //Arrange
        var testCategories = GetCategories().ToList();

        var categoriesRepoMock = new Mock<IDeletableEntityRepository<Category>>();
        categoriesRepoMock.Setup(r => r.AddAsync(It.IsAny<Category>())).Callback((Category category) => testCategories.Add(category));

        this.categoriesRepo = categoriesRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act
        var form = new FeatureCategoryFormModel
        {
            Name = newCategoryName
        };

        var categoryId = await this.featureCategoryService.CreateCategoryAsync(form);

        var categoryIsAdded = testCategories.Any(c => c.Id == categoryId);

        //Assert
        Assert.That(categoryIsAdded, Is.True);
    }

    [Test]
    [TestCase(1)]
    public async Task SoftDeleteCategoryAndFeaturesByCategoryIdAsyncSoftDeletesTheCategoryAndAllRelatedFeatures(int categoryId)
    {
        //Arrange
        var testCategories = GetCategories();
        var features = GetFeatures().ToList();

        var categoryToSoftDelete = testCategories.First(c => c.Id == categoryId);
        var featuresToSoftDelete = features.Where(f => f.CategoryId == categoryId).ToArray();

        var categoriesAsQueryable = testCategories.AsQueryable().BuildMock();
        var featuresAsQueryable = features.AsQueryable().BuildMock();

        var categoriesRepoMock = new Mock<IDeletableEntityRepository<Category>>();
        categoriesRepoMock.Setup(r => r.All()).Returns(categoriesAsQueryable.Where(x => x.IsDeleted == false));
        categoriesRepoMock.Setup(r => r.Delete(It.IsAny<Category>())).Callback((Category category) =>
        {
            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;
        });

        var featuresRepoMock = new Mock<IDeletableEntityRepository<Feature>>();
        featuresRepoMock.Setup(r => r.All()).Returns(featuresAsQueryable.Where(x => x.IsDeleted == false));
        featuresRepoMock.Setup(r => r.Delete(It.IsAny<Feature>())).Callback((Feature feature) =>
        {
            feature.IsDeleted = true;
            feature.DeletedOn = DateTime.UtcNow;
        });

        this.categoriesRepo = categoriesRepoMock.Object;
        this.featuresRepo = featuresRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act

        await this.featureCategoryService.SoftDeleteCategoryAndFeaturesByCategoryIdAsync(categoryId);

        Assert.Multiple(() =>
        {
            Assert.That(categoryToSoftDelete.IsDeleted, Is.True);
            Assert.That(categoryToSoftDelete.DeletedOn, Is.Not.Null);
            Assert.That(featuresToSoftDelete.All(f => f.IsDeleted), Is.True);
        });
    }

    [Test]
    [TestCase(1)]
    public async Task UnDeleteCategoryAndFeaturesByCategoryIdAsyncUnDeletesTheCategoryAndAllRelatedFeatures(int categoryId)
    {
        //Arrange
        var testCategories = GetCategories();
        var features = GetFeatures().ToList();

        var categoryToSoftDelete = testCategories.First(c => c.Id == categoryId);
        var featuresToSoftDelete = features.Where(f => f.CategoryId == categoryId).ToArray();

        var categoriesAsQueryable = testCategories.AsQueryable().BuildMock();
        var featuresAsQueryable = features.AsQueryable().BuildMock();

        var categoriesRepoMock = new Mock<IDeletableEntityRepository<Category>>();
        categoriesRepoMock.Setup(r => r.AllWithDeleted()).Returns(categoriesAsQueryable);
        categoriesRepoMock.Setup(r => r.Undelete(It.IsAny<Category>())).Callback((Category category) =>
        {
            category.IsDeleted = false;
            category.DeletedOn = null;
        });

        var featuresRepoMock = new Mock<IDeletableEntityRepository<Feature>>();
        featuresRepoMock.Setup(r => r.AllWithDeleted()).Returns(featuresAsQueryable);
        featuresRepoMock.Setup(r => r.Undelete(It.IsAny<Feature>())).Callback((Feature feature) =>
        {
            feature.IsDeleted = false;
            feature.DeletedOn = null;
        });

        this.categoriesRepo = categoriesRepoMock.Object;
        this.featuresRepo = featuresRepoMock.Object;

        this.featureCategoryService = new FeatureCategoryService(this.categoriesRepo, this.featuresRepo);

        //Act

        await this.featureCategoryService.UndeleteCategoryAndFeaturesByCategoryIdAsync(categoryId);

        Assert.Multiple(() =>
        {
            Assert.That(categoryToSoftDelete.IsDeleted, Is.False);
            Assert.That(categoryToSoftDelete.DeletedOn, Is.Null);
            Assert.That(featuresToSoftDelete.All(f => f.IsDeleted == false), Is.True);
        });
    }
}