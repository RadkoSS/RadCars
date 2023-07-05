namespace RadCars.Services.Data;

using Microsoft.EntityFrameworkCore;

using Contracts;
using RadCars.Data;
using Web.ViewModels.CarModel;

public class CarService : ICarService
{
    private readonly ApplicationDbContext context;

    public CarService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<CarModelViewModel>> GetModelsByMakeIdAsync(ushort makeId)
    {
        var models = await this.context.CarModels
            .AsNoTracking()
            .Where(m => m.CarMakeId == makeId)
            .Select(m => new CarModelViewModel
            {
                Id = m.Id,
                Name = m.Name
            }).ToArrayAsync();

        return models;
    }
}