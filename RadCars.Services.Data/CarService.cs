namespace RadCars.Services.Data;

using Microsoft.EntityFrameworkCore;

using Mapping;
using Contracts;
using Web.ViewModels.CarModel;
using RadCars.Data.Models.Entities;
using RadCars.Data.Common.Contracts.Repositories;

public class CarService : ICarService
{
    private readonly IDeletableEntityRepository<CarModel> carModelsRepository;

    public CarService(IDeletableEntityRepository<CarModel> carModelsRepository)
    {
        this.carModelsRepository = carModelsRepository;
    }

    public async Task<IEnumerable<CarModelViewModel>> GetModelsByMakeIdAsync(int makeId)
    {
        var models = await this.carModelsRepository
            .AllAsNoTracking()
            .Where(m => m.CarMakeId == makeId)
            .To<CarModelViewModel>()
            .ToArrayAsync();

        return models;
    }
}