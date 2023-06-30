namespace RadCars.Services.Data.Contracts;

using Microsoft.AspNetCore.Http;

public interface IImageService
{
    Task UploadMultipleImagesAsync(string listingId, IEnumerable<IFormFile> images);

    Task UploadImageAsync(string listingId, IFormFile image);

    Task DeleteImageAsync(string listingId, string imageId);

    Task DeleteAllImagesAsync(string listingId, IEnumerable<string> imagesIds);
}