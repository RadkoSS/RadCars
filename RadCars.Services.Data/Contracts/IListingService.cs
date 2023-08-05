namespace RadCars.Services.Data.Contracts;

using Models.Listing;
using Web.ViewModels.Listing;
using Web.ViewModels.CarImage;
using Web.ViewModels.Thumbnail;

public interface IListingService
{
    Task<AllListingsFilteredAndPagedServiceModel> GetAllListingsAsync(AllListingsQueryModel queryModel, bool withDeleted);

    Task<IEnumerable<AllListingsViewModel>> GetAllListingsByUserIdAsync(string userId);

    Task<IEnumerable<AllListingsViewModel>> GetFavoriteListingsByUserIdAsync(string userId);

    Task<bool> IsListingInUserFavoritesByIdAsync(string listingId, string userId);

    Task FavoriteListingByIdAsync(string listingId, string userId);

    Task<int> GetFavoritesCountForListingByIdAsync(string listingId);

    Task UnFavoriteListingByIdAsync(string listingId, string userId);

    Task<IEnumerable<AllListingsViewModel>> GetAllDeactivatedListingsByUserIdAsync(string userId);

    Task<IEnumerable<ListingIndexViewModel>> GetMostRecentListingsAsync();

    Task<string> CreateListingAsync(ListingFormModel form, string userId);

    Task<ListingEditFormModel> GetListingEditAsync(string listingId, string userId, bool isUserAdmin);

    Task<string> EditListingAsync(ListingEditFormModel form, string userId, bool isUserAdmin);

    Task<ListingFormModel> GetListingCreateAsync();

    Task<IEnumerable<ImageViewModel>> GetUploadedImagesForListingByIdAsync(string listingId, string userId, bool isUserAdmin);

    Task<int> GetUploadedImagesCountForListingByIdAsync(string listingId);

    Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId);

    Task<ListingDetailsViewModel> GetDeactivatedListingDetailsAsync(string listingId, string userId, bool isUserAdmin);

    Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string listingId, string userId, bool isUserAdmin);

    Task AddThumbnailToListingByIdAsync(string listingId, string imageId, string userId, bool isUserAdmin);

    Task DeactivateListingByIdAsync(string listingId, string userId, bool isUserAdmin);

    Task ReactivateListingByIdAsync(string listingId, string userId, bool isUserAdmin);

    Task HardDeleteListingByIdAsync(string listingId, string userId, bool isUserAdmin);
}