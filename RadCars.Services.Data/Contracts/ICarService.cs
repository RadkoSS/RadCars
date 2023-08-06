namespace RadCars.Services.Data.Contracts;

using Web.ViewModels.City;
using Web.ViewModels.CarMake;
using Web.ViewModels.CarModel;
using Web.ViewModels.CarEngineType;
using Web.ViewModels.FeatureCategory;

public interface ICarService
{
    Task<IEnumerable<CarModelViewModel>> GetModelsByMakeIdAsync(int makeId);

    Task<IEnumerable<CarMakeViewModel>> GetCarMakesAsync();

    Task<IEnumerable<FeaturesWithCategoryViewModel>> GetFeaturesWithCategoriesAsync();

    Task<IEnumerable<CityViewModel>> GetBulgarianCitiesAsync();

    Task<IEnumerable<EngineTypeViewModel>> GetEngineTypesAsync();

    Task<bool> CarMakeIdExistsAsync(int carMakeId);

    Task<bool> CarModelIdExistsByMakeIdAsync(int carMakeId, int carModelId);

    Task<bool> CarEngineTypeIdExistsAsync(int engineTypeId);

    Task<bool> CityIdExistsAsync(int cityId);

    Task<bool> SelectedFeaturesIdsExist(IEnumerable<int> selectedFeatures);
}