namespace RadCars.Web.ViewModels.City;

using Data.Models.Entities;
using Services.Mapping.Contracts;

public class CityViewModel : IMapFrom<City>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}