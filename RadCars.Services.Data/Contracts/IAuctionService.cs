﻿namespace RadCars.Services.Data.Contracts;

using Models.Auction;
using Web.ViewModels.Auction;
using Web.ViewModels.CarImage;
using Web.ViewModels.Thumbnail;
using Web.ViewModels.FeatureCategory;

public interface IAuctionService
{
    Task<AllAuctionsFilteredAndPagedServiceModel> GetAllAuctionsAsync(AllAuctionsQueryModel queryModel, bool withDeleted);

    Task<IEnumerable<AllAuctionsViewModel>> GetAllAuctionsByUserIdAsync(string userId);

    Task<IEnumerable<AllAuctionsViewModel>> GetAllExpiredAuctionsByUserIdAsync(string userId);

    Task<IEnumerable<AllAuctionsViewModel>> GetFavoriteAuctionsByUserIdAsync(string userId);

    Task<bool> IsAuctionInUserFavoritesByIdAsync(string auctionId, string userId);

    Task FavoriteAuctionByIdAsync(string auctionId, string userId);

    Task<int> GetFavoritesCountForAuctionByIdAsync(string auctionId);

    Task UnFavoriteAuctionByIdAsync(string auctionId, string userId);

    Task<AuctionBidServiceModel> CreateBidAsync(string auctionId, string userId, decimal amount);

    Task<IEnumerable<AllAuctionsViewModel>> GetAllDeactivatedAuctionsByUserIdAsync(string userId);

    Task<IEnumerable<AuctionIndexViewModel>> GetMostRecentAuctionsAsync();

    Task<string> CreateAuctionAsync(AuctionFormModel form, string userId);

    Task<AuctionEditFormModel> GetAuctionEditAsync(string auctionId, string userId, bool isUserAdmin);

    Task<string> EditAuctionAsync(AuctionEditFormModel form, string userId, bool isUserAdmin);

    Task<AuctionFormModel> GetAuctionCreateAsync();

    Task<IEnumerable<ImageViewModel>> GetUploadedImagesForAuctionByIdAsync(string auctionId, string userId, bool isUserAdmin);

    Task<int> GetUploadedImagesCountForAuctionByIdAsync(string auctionId);

    Task<ICollection<FeaturesWithCategoryViewModel>> GetSelectedFeaturesByAuctionIdAsync(string auctionId);

    Task<AuctionDetailsViewModel> GetAuctionDetailsAsync(string auctionId, string? userId, bool isUserAdmin);

    Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string auctionId, string userId, bool isUserAdmin);

    Task AddThumbnailToAuctionByIdAsync(string auctionId, string imageId, string userId, bool isUserAdmin);

    Task DeactivateAuctionByIdAsync(string auctionId, string userId, bool isUserAdmin);

    Task HardDeleteAuctionByIdAsync(string auctionId);

    Task<int> GetBidsCountForAuctionByIdAsync(string auctionId);
}