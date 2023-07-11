namespace RadCars.Web.ViewModels.CarMake;

using Data.Models.Entities;
using Services.Mapping.Contracts;

public class CarMakeViewModel : IMapFrom<CarMake>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}