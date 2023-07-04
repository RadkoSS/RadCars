namespace RadCars.Services.Data.Contracts;

using Web.ViewModels.Listing;

public interface IListingService
{
    Task<ListingViewModel[]> GetAllListingsAsync();

    Task CreateListing(CreateListingFormModel form, string userId);

    Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId);
}