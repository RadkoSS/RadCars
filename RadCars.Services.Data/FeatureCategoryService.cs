namespace RadCars.Services.Data;

using Microsoft.EntityFrameworkCore;

using Mapping;
using Contracts;
using Web.ViewModels.Feature;
using RadCars.Data.Models.Entities;
using Web.ViewModels.FeatureCategory;
using RadCars.Data.Common.Contracts.Repositories;

public class FeatureCategoryService : IFeatureCategoryService
{
    private readonly IDeletableEntityRepository<Feature> featuresRepository;
    private readonly IDeletableEntityRepository<Category> categoriesRepository;

    public FeatureCategoryService(IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<Feature> featuresRepository)
    {
        this.featuresRepository = featuresRepository;
        this.categoriesRepository = categoriesRepository;
    }

    public async Task<IEnumerable<FeatureCategoryViewModel>> GetCategoriesAsync()
    {
        var categories = await this.categoriesRepository
            .AllWithDeleted()
            .To<FeatureCategoryViewModel>()
            .ToArrayAsync();

        return categories;
    }

    public async Task EditCategoryAsync(FeatureCategoryFormModel form)
    {
        var categoryToEdit = await this.categoriesRepository
            .AllWithDeleted()
            .FirstAsync(c => c.Id == form.Id);

        categoryToEdit.Name = form.Name;

        await this.categoriesRepository.SaveChangesAsync();
    }

    public async Task<FeatureCategoryFormModel> GetCategoryEditAsync(int categoryId)
    {
        var categoryToEdit = await this.categoriesRepository
            .AllWithDeleted()
            .Where(c => c.Id == categoryId)
            .To<FeatureCategoryFormModel>().FirstAsync();

        return categoryToEdit;
    }

    public async Task<FeaturesWithCategoryViewModel> GetAllFeaturesByCategoryIdAsync(int categoryId)
    {
        var featuresByCategory = await this.categoriesRepository
            .AllWithDeleted()
            .Where(f => f.Id == categoryId)
            .To<FeaturesWithCategoryViewModel>()
            .FirstAsync();

        return featuresByCategory;
    }

    public async Task<int> CreateFeatureAsync(FeatureFormModel input)
    {
        var featureToCreateExists = await this.featuresRepository
            .AllWithDeleted()
            .AnyAsync(f => f.Name.ToLower() == input.Name.ToLower());

        if (featureToCreateExists)
        {
            throw new InvalidOperationException();
        }

        var newFeature = new Feature
        {
            CategoryId = input.CategoryId,
            Name = input.Name
        };

        await this.featuresRepository.AddAsync(newFeature);

        await this.featuresRepository.SaveChangesAsync();

        return newFeature.CategoryId;
    }

    public async Task<FeatureFormModel> GetFeatureEditByIdAsync(int featureId)
    {
        var featureToEdit = await this.featuresRepository
            .AllWithDeleted()
            .Where(f => f.Id == featureId)
            .To<FeatureFormModel>()
            .FirstAsync();

        return featureToEdit;
    }

    public async Task<int> EditFeatureAsync(FeatureFormModel input)
    {
        var featureToEdit = await this.featuresRepository
            .AllWithDeleted()
            .FirstAsync(f => f.Id == input.Id);

        featureToEdit.Name = input.Name;

        this.featuresRepository.Update(featureToEdit);

        await this.featuresRepository.SaveChangesAsync();

        return featureToEdit.CategoryId;
    }

    public async Task<int> SoftDeleteFeatureByIdAsync(int featureId)
    {
        var featureToSoftDelete = await this.featuresRepository
            .All()
            .FirstAsync(f => f.Id == featureId);

        this.featuresRepository.Delete(featureToSoftDelete);

        await this.categoriesRepository.SaveChangesAsync();

        return featureToSoftDelete.CategoryId;
    }

    public async Task<int> UndeleteFeatureByIdAsync(int featureId)
    {
        var featureToUndelete = await this.featuresRepository
            .AllWithDeleted()
            .FirstAsync(f => f.Id == featureId);

        this.featuresRepository.Undelete(featureToUndelete);

        await this.categoriesRepository.SaveChangesAsync();

        return featureToUndelete.CategoryId;
    }

    public async Task<int> CreateCategoryAsync(FeatureCategoryFormModel form)
    {
        var newCategory = new Category
        {
            Name = form.Name
        };

        await this.categoriesRepository.AddAsync(newCategory);

        await this.categoriesRepository.SaveChangesAsync();

        return newCategory.Id;
    }

    public async Task SoftDeleteCategoryAndFeaturesByCategoryIdAsync(int categoryId)
    {
        var categoryToSoftDelete = await this.categoriesRepository
            .All()
            .FirstAsync(c => c.Id == categoryId);

        this.categoriesRepository.Delete(categoryToSoftDelete);

        await this.categoriesRepository.SaveChangesAsync();

        var features = await this.featuresRepository.All().Where(f => f.CategoryId == categoryId).ToArrayAsync();

        foreach (var feature in features)
        {
            this.featuresRepository.Delete(feature);
        }

        await this.featuresRepository.SaveChangesAsync();
    }

    public async Task UndeleteCategoryAndFeaturesByCategoryIdAsync(int categoryId)
    {
        var categoryToUndelete = await this.categoriesRepository
            .AllWithDeleted()
            .FirstAsync(c => c.Id == categoryId);

        this.categoriesRepository.Undelete(categoryToUndelete);

        await this.categoriesRepository.SaveChangesAsync();

        var features = await this.featuresRepository.AllWithDeleted().Where(f => f.CategoryId == categoryId).ToArrayAsync();

        foreach (var feature in features)
        {
            this.featuresRepository.Undelete(feature);
        }

        await this.featuresRepository.SaveChangesAsync();
    }
}