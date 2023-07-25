namespace RadCars.Services.Data.Contracts;

using Models.Listing;
using Web.ViewModels.City;
using Web.ViewModels.Home;
using Web.ViewModels.CarMake;
using Web.ViewModels.Listing;
using Web.ViewModels.CarImage;
using Web.ViewModels.Thumbnail;
using Web.ViewModels.CarEngineType;
using Web.ViewModels.FeatureCategory;

public interface IListingService
{
    Task<AllListingsFilteredAndPagedServiceModel> GetAllListingsAsync(AllListingsQueryModel queryModel);

    Task<IEnumerable<AllListingsViewModel>> GetAllListingsByUserIdAsync(string userId);

    Task<IEnumerable<AllListingsViewModel>> GetFavoriteListingsByUserIdAsync(string userId);

    Task<bool> IsListingInUserFavoritesByIdAsync(string listingId, string userId);

    Task FavoriteListingByIdAsync(string listingId, string userId);

    Task<int> GetFavoritesCountForListingByIdAsync(string listingId);

    Task UnFavoriteListingByIdAsync(string listingId, string userId);

    Task<IEnumerable<AllListingsViewModel>> GetAllDeactivatedListingsByUserIdAsync(string userId);

    Task<IEnumerable<IndexViewModel>> GetMostRecentListingsAsync();

    Task<string> CreateListingAsync(ListingFormModel form, string userId);

    Task<ListingEditFormModel> GetListingEditAsync(string listingId, string userId, bool userIsAdmin);

    Task<string> EditListingAsync(ListingEditFormModel form, string userId, bool userIsAdmin);

    Task<IEnumerable<CarMakeViewModel>> GetCarMakesAsync();

    Task<IEnumerable<FeatureCategoriesViewModel>> GetFeatureCategoriesAsync();

    Task<IEnumerable<CityViewModel>> GetCitiesAsync();

    Task<IEnumerable<EngineTypeViewModel>> GetEngineTypesAsync();

    Task<ListingFormModel> GetListingCreateAsync();

    Task<IEnumerable<ImageViewModel>> GetUploadedImagesForListingByIdAsync(string listingId, string userId, bool userIsAdmin);

    Task<int> GetUploadedImagesCountForListingByIdAsync(string listingId);

    Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId);

    Task<ListingDetailsViewModel> GetDeactivatedListingDetailsAsync(string listingId, string userId, bool userIsAdmin);

    Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string listingId, string userId, bool userIsAdmin);

    Task AddThumbnailToListingByIdAsync(string listingId, string imageId, string userId, bool userIsAdmin);

    Task DeactivateListingByIdAsync(string listingId, string userId, bool userIsAdmin);

    Task ReactivateListingByIdAsync(string listingId, string userId, bool userIsAdmin);

    Task HardDeleteListingByIdAsync(string listingId, string userId, bool userIsAdmin);
}