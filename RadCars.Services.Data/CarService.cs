namespace RadCars.Services.Data;

using Microsoft.EntityFrameworkCore;

using Mapping;
using Contracts;
using Web.ViewModels.City;
using Web.ViewModels.CarMake;
using Web.ViewModels.CarModel;
using Web.ViewModels.CarEngineType;
using RadCars.Data.Models.Entities;
using Web.ViewModels.FeatureCategory;
using RadCars.Data.Common.Contracts.Repositories;

public class CarService : ICarService
{
    private readonly IDeletableEntityRepository<City> citiesRepository;
    private readonly IDeletableEntityRepository<CarMake> carMakesRepository;
    private readonly IDeletableEntityRepository<Feature> featuresRepository;
    private readonly IDeletableEntityRepository<CarModel> carModelsRepository;
    private readonly IDeletableEntityRepository<Category> categoriesRepository;
    private readonly IDeletableEntityRepository<EngineType> engineTypesRepository;

    public CarService(IDeletableEntityRepository<CarModel> carModelsRepository, IDeletableEntityRepository<CarMake> carMakesRepository, IDeletableEntityRepository<City> citiesRepository, IDeletableEntityRepository<EngineType> engineTypesRepository, IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<Feature> featuresRepository)
    {
        this.citiesRepository = citiesRepository;
        this.carMakesRepository = carMakesRepository;
        this.featuresRepository = featuresRepository;
        this.carModelsRepository = carModelsRepository;
        this.categoriesRepository = categoriesRepository;
        this.engineTypesRepository = engineTypesRepository;
    }

    public async Task<IEnumerable<CarModelViewModel>> GetModelsByMakeIdAsync(int makeId)
    => await this.carModelsRepository
        .AllAsNoTracking()
        .Where(m => m.CarMakeId == makeId)
        .To<CarModelViewModel>()
        .ToArrayAsync();

    public async Task<IEnumerable<CarMakeViewModel>> GetCarMakesAsync()
        => await this.carMakesRepository.AllAsNoTracking()
            .To<CarMakeViewModel>().ToArrayAsync();

    public async Task<IEnumerable<FeatureCategoriesViewModel>> GetFeatureCategoriesAsync()
        => await this.categoriesRepository.AllAsNoTracking().To<FeatureCategoriesViewModel>().ToArrayAsync();

    public async Task<IEnumerable<CityViewModel>> GetCitiesAsync()
        => await this.citiesRepository.AllAsNoTracking().To<CityViewModel>().ToArrayAsync();

    public async Task<IEnumerable<EngineTypeViewModel>> GetEngineTypesAsync()
        => await this.engineTypesRepository.AllAsNoTracking().To<EngineTypeViewModel>().ToArrayAsync();

    public async Task<bool> CarMakeIdExistsAsync(int carMakeId)
    => await this.carMakesRepository.All().AnyAsync(m => m.Id == carMakeId);

    public async Task<bool> CarModelIdExistsByMakeIdAsync(int carMakeId, int carModelId)
    {
        var modelsByMake = await this.GetModelsByMakeIdAsync(carMakeId);

        return modelsByMake.Any(m => m.Id == carModelId);
    }

    public async Task<bool> CarEngineTypeIdExistsAsync(int engineTypeId)
        => await this.engineTypesRepository.All().AnyAsync(t => t.Id == engineTypeId);

    public async Task<bool> CityIdExistsAsync(int cityId)
        => await this.citiesRepository.All().AnyAsync(c => c.Id == cityId);

    public async Task<bool> SelectedFeaturesIdsExist(IEnumerable<int> selectedFeatures)
    {
        var features = await this.featuresRepository.All().Select(f => f.Id).ToArrayAsync();

        bool featureIdsExist = true;
        foreach (var featureId in selectedFeatures)
        {
            if (features.Contains(featureId) == false)
            {
                featureIdsExist = false;
                break;
            }
        }

        return featureIdsExist;
    }
}