namespace RadCars.Services.Data.Contracts;

using Web.ViewModels.City;
using Web.ViewModels.Home;
using Web.ViewModels.CarMake;
using Web.ViewModels.Listing;
using Web.ViewModels.Thumbnail;
using Web.ViewModels.CarEngineType;
using Web.ViewModels.FeatureCategory;

public interface IListingService
{
    Task<IEnumerable<AllListingViewModel>> GetAllListingsAsync();

    Task<IEnumerable<AllListingViewModel>> GetAllListingsByUserIdAsync(string userId);

    Task<IEnumerable<AllListingViewModel>> GetFavoriteListingsByUserId(string userId);

    Task<bool> IsListingInUserFavoritesById(string listingId, string userId);

    Task FavoriteListingByIdAsync(string listingId, string userId);

    Task UnFavoriteListingByIdAsync(string listingId, string userId);

    Task<IEnumerable<AllListingViewModel>> GetAllDeactivatedListingsByUserIdAsync(string userId);

    Task<IEnumerable<IndexViewModel>> GetMostRecentListingsAsync();

    Task<string> CreateListingAsync(ListingFormModel form, string userId);

    Task<IEnumerable<CarMakeViewModel>> GetCarMakesAsync();

    Task<IEnumerable<FeatureCategoriesViewModel>> GetFeatureCategoriesAsync();

    Task<IEnumerable<CityViewModel>> GetCitiesAsync();

    Task<IEnumerable<EngineTypeViewModel>> GetEngineTypesAsync();

    Task<ListingFormModel> GetListingCreateAsync();

    Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId);

    Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string listingId, string userId);

    Task AddThumbnailToListingByIdAsync(string listingId, string imageId, string userId);

    Task DeactivateListingByIdAsync(string listingId, string userId);

    Task ReactivateListingByIdAsync(string listingId, string userId);

    Task HardDeleteListingByIdAsync(string listingId, string userId);
}