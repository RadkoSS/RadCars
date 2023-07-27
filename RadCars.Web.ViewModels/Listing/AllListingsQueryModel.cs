namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;

using Enums;
using Common;
using static RadCars.Common.GeneralApplicationConstants;

public class AllListingsQueryModel : BaseAllQueryModel
{
    public AllListingsQueryModel()
    {
        this.ListingsPerPage = EntitiesPerPage;
        
        this.Listings = new HashSet<AllListingsViewModel>();
    }

    [Display(Name = "Сортиране:")]
    public ListingSorting ListingSorting { get; set; }

    [Display(Name = "Брой обяви на страница:")]
    public int ListingsPerPage { get; set; }

    public int TotalListings { get; set; }

    [Display(Name = "Диапазон на цената:")]
    public int MaximumPrice { get; set; }

    public IEnumerable<AllListingsViewModel> Listings { get; set; }
}