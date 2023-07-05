namespace RadCars.Services.Data.Contracts;

using Web.ViewModels.Listing;

public interface IListingService
{
    Task<ListingViewModel[]> GetAllListingsAsync();

    Task CreateListingAsync(ListingFormModel form, string userId);

    Task<ListingFormModel> GetListingCreateAsync();

    Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId);

    Task AddThumbnailToListingByIdAsync(string listingId, string imageId);
}