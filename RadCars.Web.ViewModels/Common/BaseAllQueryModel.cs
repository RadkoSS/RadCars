namespace RadCars.Web.ViewModels.Common;

using System.ComponentModel.DataAnnotations;

using City;
using CarMake;
using CarModel;
using CarEngineType;

using static RadCars.Common.GeneralApplicationConstants;

public abstract class BaseAllQueryModel
{
    protected BaseAllQueryModel()
    {
        this.CurrentPage = DefaultPage;

        this.Cities = new HashSet<CityViewModel>();
        this.CarMakes = new HashSet<CarMakeViewModel>();
        this.CarModels = new HashSet<CarModelViewModel>();
        this.EngineTypes = new HashSet<EngineTypeViewModel>();
    }

    [Display(Name = "Търсене по заглавие и описание:")]
    public string? SearchString { get; set; }

    public int CurrentPage { get; set; }

    [Display(Name = "Населено място:")]
    public int? CityId { get; set; }

    [Display(Name = "Марка:")]
    public int? CarMakeId { get; set; }

    [Display(Name = "Модел:")]
    public int? CarModelId { get; set; }

    [Display(Name = "Вид двигател:")]
    public int? EngineTypeId { get; set; }

    [Display(Name = "Максимален пробег:")]
    public int MaximumMileage { get; set; }

    public IEnumerable<CityViewModel> Cities { get; set; }

    public IEnumerable<CarMakeViewModel> CarMakes { get; set; }

    public IEnumerable<CarModelViewModel> CarModels { get; set; }

    public IEnumerable<EngineTypeViewModel> EngineTypes { get; set; }
}