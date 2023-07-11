namespace RadCars.Web.ViewModels.CarModel;

using Data.Models.Entities;
using Services.Mapping.Contracts;

public class CarModelViewModel : IMapFrom<CarModel>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}