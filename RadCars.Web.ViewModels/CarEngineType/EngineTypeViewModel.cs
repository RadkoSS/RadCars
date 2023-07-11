namespace RadCars.Web.ViewModels.CarEngineType;

using Data.Models.Entities;
using Services.Mapping.Contracts;

public class EngineTypeViewModel : IMapFrom<EngineType>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}