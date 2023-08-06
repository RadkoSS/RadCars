namespace RadCars.Services.Data.Contracts;

using Web.ViewModels.Feature;
using Web.ViewModels.FeatureCategory;

public interface IFeatureCategoryService
{
    Task<IEnumerable<FeatureCategoryViewModel>> GetCategoriesAsync();

    Task<int> CreateCategoryAsync(FeatureCategoryFormModel form);

    Task<FeatureCategoryFormModel> GetCategoryEditAsync(int categoryId);

    Task EditCategoryAsync(FeatureCategoryFormModel form);

    Task SoftDeleteCategoryAndFeaturesByCategoryIdAsync(int categoryId);

    Task UndeleteCategoryAndFeaturesByCategoryIdAsync(int categoryId);

    Task<FeaturesWithCategoryViewModel> GetAllFeaturesByCategoryIdAsync(int categoryId);

    Task<int> CreateFeatureAsync(FeatureFormModel input);

    Task<FeatureFormModel> GetFeatureEditByIdAsync(int featureId);

    Task<int> EditFeatureAsync(FeatureFormModel input);

    Task<int> SoftDeleteFeatureByIdAsync(int featureId);

    Task<int> UndeleteFeatureByIdAsync(int featureId);
}