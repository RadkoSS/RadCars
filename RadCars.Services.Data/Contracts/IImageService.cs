namespace RadCars.Services.Data.Contracts;

using Microsoft.AspNetCore.Http;

using RadCars.Data.Models.Entities;

public interface IImageService
{
    Task<ICollection<CarImage>> UploadMultipleImagesAsync(string listingId, IEnumerable<IFormFile> images);

    Task<CarImage> UploadImageAsync(string listingId, IFormFile image);

    Task DeleteImageAsync(string listingId, string imageId);

    Task DeleteAllImagesAsync(string listingId, IEnumerable<string> imagesIds);
}