namespace RadCars.Services.Data.Contracts;

using Web.ViewModels.City;
using Web.ViewModels.CarMake;
using Web.ViewModels.Listing;
using Web.ViewModels.Thumbnail;
using Web.ViewModels.CarEngineType;
using Web.ViewModels.FeatureCategory;

public interface IListingService
{
    Task<IEnumerable<ListingViewModel>> GetAllListingsAsync();

    Task<string> CreateListingAsync(ListingFormModel form, string userId);

    Task<IEnumerable<CarMakeViewModel>> GetCarMakesAsync();

    Task<IEnumerable<FeatureCategoriesViewModel>> GetFeatureCategoriesAsync();

    Task<IEnumerable<CityViewModel>> GetCitiesAsync();

    Task<IEnumerable<EngineTypeViewModel>> GetEngineTypesAsync();

    Task<ListingFormModel> GetListingCreateAsync();

    Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId);

    Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string listingId, string creatorId);

    Task AddThumbnailToListingByIdAsync(string listingId, string imageId, string creatorId);
}