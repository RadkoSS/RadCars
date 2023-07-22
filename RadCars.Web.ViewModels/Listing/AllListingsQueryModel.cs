namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;

using City;
using Enums;
using CarMake;
using CarModel;
using CarEngineType;
using static Common.GeneralApplicationConstants;

public class AllListingsQueryModel
{
    public AllListingsQueryModel()
    {
        this.CurrentPage = DefaultPage;
        this.ListingsPerPage = EntitiesPerPage;

        this.Cities = new HashSet<CityViewModel>();
        this.CarMakes = new HashSet<CarMakeViewModel>();
        this.CarModels = new HashSet<CarModelViewModel>();
        this.Listings = new HashSet<AllListingsViewModel>();
        this.EngineTypes = new HashSet<EngineTypeViewModel>();
    }

    [Display(Name = "Търсене по заглавие и описание:")]
    public string? SearchString { get; set; }

    [Display(Name = "Сортиране:")]
    public ListingSorting ListingSorting { get; set; }

    public int CurrentPage { get; set; }

    [Display(Name = "Брой обяви на страница:")]
    public int ListingsPerPage { get; set; }

    public int TotalListings { get; set; }

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

    [Display(Name = "Диапазон на цената:")]
    public int MaximumPrice { get; set; }

    public IEnumerable<CityViewModel> Cities { get; set; }

    public IEnumerable<CarMakeViewModel> CarMakes { get; set; }

    public IEnumerable<CarModelViewModel> CarModels { get; set; }

    public IEnumerable<EngineTypeViewModel> EngineTypes { get; set; }

    public IEnumerable<AllListingsViewModel> Listings { get; set; }
}