namespace RadCars.Services.Data.Contracts;

using Web.ViewModels.CarModel;

public interface ICarService
{
    Task<IEnumerable<CarModelViewModel>> GetModelsByMakeIdAsync(int makeId);
}