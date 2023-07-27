// ReSharper disable IdentifierTypo
namespace RadCars.Services.Data.Contracts;

using Microsoft.AspNetCore.Http;

using RadCars.Data.Models.Entities;

public interface IListingImageService
{
    Task<ICollection<CarImage>> UploadMultipleListingImagesAsync(string listingId, IEnumerable<IFormFile> images);

    Task<CarImage> UploadListingImageAsync(string listingId, IFormFile image);

    Task DeleteListingImageAsync(string listingId, string imageId);

    Task DeleteAllImagesOfListingAsync(string listingId, IEnumerable<string> imagesIds);
}