namespace RadCars.Web.ViewModels.Listing;

using Common;
using Data.Models.Entities;
using Services.Mapping.Contracts;

public class ListingIndexViewModel : BaseIndexViewModel, IMapFrom<Listing>
{
    public decimal Price { get; set; }
}
