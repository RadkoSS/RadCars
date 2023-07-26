namespace RadCars.Services.Data;

using Contracts;
using Web.ViewModels.Home;
using Web.ViewModels.Auction;
using Web.ViewModels.CarImage;
using Web.ViewModels.Thumbnail;

public class AuctionService : IAuctionService
{
    //public Task<AllAuctionsFilteredAndPagedServiceModel> GetAllAuctionsAsync(AllAuctionsQueryModel queryModel)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<IEnumerable<AllAuctionsViewModel>> GetAllAuctionsByUserIdAsync(string userId)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<IEnumerable<AllAuctionsViewModel>> GetFavoriteAuctionsByUserIdAsync(string userId)
    //{
    //    throw new NotImplementedException();
    //}

    public Task<bool> IsAuctionInUserFavoritesByIdAsync(string auctionId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task FavoriteAuctionByIdAsync(string auctionId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetFavoritesCountForAuctionByIdAsync(string auctionId)
    {
        throw new NotImplementedException();
    }

    public Task UnFavoriteAuctionByIdAsync(string auctionId, string userId)
    {
        throw new NotImplementedException();
    }

    //public Task<IEnumerable<AllAuctionsViewModel>> GetAllDeactivatedAuctionsByUserIdAsync(string userId)
    //{
    //    throw new NotImplementedException();
    //}

    public Task<IEnumerable<IndexViewModel>> GetMostRecentAuctionsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateAuctionAsync(AuctionFormModel form, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<AuctionEditFormModel> GetAuctionEditAsync(string auctionId, string userId, bool userIsAdmin)
    {
        throw new NotImplementedException();
    }

    public Task<string> EditAuctionAsync(AuctionEditFormModel form, string userId, bool userIsAdmin)
    {
        throw new NotImplementedException();
    }

    public Task<AuctionFormModel> GetAuctionCreateAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ImageViewModel>> GetUploadedImagesForAuctionByIdAsync(string auctionId, string userId, bool userIsAdmin)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetUploadedImagesCountForAuctionByIdAsync(string auctionId)
    {
        throw new NotImplementedException();
    }

    //public Task<AuctionDetailsViewModel> GetAuctionDetailsAsync(string auctionId)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<AuctionDetailsViewModel> GetDeactivatedAuctionDetailsAsync(string auctionId, string userId, bool userIsAdmin)
    //{
    //    throw new NotImplementedException();
    //}

    public Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string auctionId, string userId, bool userIsAdmin)
    {
        throw new NotImplementedException();
    }

    public Task AddThumbnailToAuctionByIdAsync(string auctionId, string imageId, string userId, bool userIsAdmin)
    {
        throw new NotImplementedException();
    }

    public Task DeactivateAuctionByIdAsync(string auctionId, string userId, bool userIsAdmin)
    {
        throw new NotImplementedException();
    }

    public Task ReactivateListingByIdAsync(string auctionId, string userId, bool userIsAdmin)
    {
        throw new NotImplementedException();
    }

    public Task HardDeleteListingByIdAsync(string auctionId, string userId, bool userIsAdmin)
    {
        throw new NotImplementedException();
    }
}