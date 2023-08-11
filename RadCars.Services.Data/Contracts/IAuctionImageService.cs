namespace RadCars.Services.Data.Contracts;

using Microsoft.AspNetCore.Http;

using RadCars.Data.Models.Entities;

public interface IAuctionImageService
{
    Task<ICollection<AuctionCarImage>> UploadMultipleAuctionImagesAsync(string auctionId, IEnumerable<IFormFile> images);

    Task<AuctionCarImage> UploadAuctionImageAsync(string auctionId, IFormFile image);

    Task DeleteAuctionImageAsync(string auctionId, string imageId);

    Task DeleteAllImagesOfAuctionAsync(string auctionId, IEnumerable<string> imagesIds);
}