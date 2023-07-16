// ReSharper disable IdentifierTypo
namespace RadCars.Services.Data;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Contracts;
using RadCars.Data.Models.Entities;
using RadCars.Data.Common.Contracts.Repositories;

using static Common.ExceptionsAndNotificationsMessages;

public class ImageService : IImageService
{
    private readonly Cloudinary cloudinary;

    private readonly IDeletableEntityRepository<CarImage> carImagesRepository;

    public ImageService(Cloudinary cloudinary, IDeletableEntityRepository<CarImage> carImagesRepository)
    {
        this.cloudinary = cloudinary;
        
        this.carImagesRepository = carImagesRepository;
    }

    public async Task<CarImage> UploadImageAsync(string listingId, IFormFile image)
    {
        var imageId = Guid.NewGuid();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(GetUniqueFileName(image.FileName, imageId.ToString()), image.OpenReadStream()),
            Folder = listingId,
            UseFilename = true,
            UniqueFilename = false
        };

        var uploadResult = await this.cloudinary.UploadAsync(uploadParams);

        var secureUrl = this.cloudinary.Api.UrlImgUp.Secure().BuildUrl($"{uploadResult.PublicId}.{uploadResult.Format}");

        var uploadedImage = new CarImage
        {
            Id = imageId,
            ListingId = Guid.Parse(listingId),
            Url = secureUrl
        };

        return uploadedImage;
    }

    public async Task<ICollection<CarImage>> UploadMultipleImagesAsync(string listingId, IEnumerable<IFormFile> images)
    {
        var uploadedImages = new HashSet<CarImage>();

        foreach (var image in images)
        {
            var uploadedImage = await UploadImageAsync(listingId, image);

            uploadedImages.Add(uploadedImage);
        }

        return uploadedImages;
    }

    public async Task DeleteImageAsync(string listingId, string imageId)
    {
        var carImage = await this.carImagesRepository.All().FirstOrDefaultAsync(ci => ci.ListingId.ToString() == listingId && ci.Id.ToString() == imageId);

        if (carImage == null)
        {
            throw new InvalidOperationException(ImageDoesNotExistError);
        }

        var deletionParams = new DeletionParams($"{listingId}/{imageId}");

        var deletionResult = await this.cloudinary.DestroyAsync(deletionParams);

        if (deletionResult.Result != "ok")
        {
            throw new InvalidOperationException(ImageDeleteUnsuccessful);
        }

        this.carImagesRepository.HardDelete(carImage);
        await this.carImagesRepository.SaveChangesAsync();
    }

    public async Task DeleteAllImagesAsync(string listingId, IEnumerable<string> imagesIds)
    {
        foreach (var imageId in imagesIds)
        {
            await this.DeleteImageAsync(listingId, imageId);
        }

        await this.cloudinary.DeleteFolderAsync(listingId);
    }

    private static string GetUniqueFileName(string fileName, string imageId)
    {
        string fileExtension = Path.GetExtension(fileName);
        return $"{imageId}{fileExtension}";
    }
}