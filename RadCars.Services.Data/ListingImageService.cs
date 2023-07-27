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

public class ListingImageService : IListingImageService
{
    private readonly Cloudinary cloudinary;

    private readonly IDeletableEntityRepository<CarImage> listingImagesRepository;

    public ListingImageService(Cloudinary cloudinary, IDeletableEntityRepository<CarImage> listingImagesRepository)
    {
        this.cloudinary = cloudinary;
        
        this.listingImagesRepository = listingImagesRepository;
    }

    public async Task<CarImage> UploadListingImageAsync(string listingId, IFormFile image)
    {
        var imageId = Guid.NewGuid();

        var secureUrl = await this.UploadImageToCloudinaryAsync(listingId, image, imageId);

        var uploadedImage = new CarImage
        {
            Id = imageId,
            ListingId = Guid.Parse(listingId),
            Url = secureUrl
        };

        return uploadedImage;
    }

    public async Task<ICollection<CarImage>> UploadMultipleListingImagesAsync(string listingId, IEnumerable<IFormFile> images)
    {
        var uploadedImages = new HashSet<CarImage>();

        foreach (var image in images)
        {
            var uploadedImage = await UploadListingImageAsync(listingId, image);

            uploadedImages.Add(uploadedImage);
        }

        return uploadedImages;
    }

    public async Task DeleteListingImageAsync(string listingId, string imageId)
    {
        var carImage = await this.listingImagesRepository.All().FirstOrDefaultAsync(ci => ci.ListingId.ToString() == listingId && ci.Id.ToString() == imageId);

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

        this.listingImagesRepository.HardDelete(carImage);
        await this.listingImagesRepository.SaveChangesAsync();
    }

    public async Task DeleteAllImagesOfListingAsync(string listingId, IEnumerable<string> imagesIds)
    {
        foreach (var imageId in imagesIds)
        {
            await this.DeleteListingImageAsync(listingId, imageId);
        }

        await this.cloudinary.DeleteFolderAsync(listingId);
    }

    private async Task<string> UploadImageToCloudinaryAsync(string folderName, IFormFile image, Guid imageId)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(GetUniqueFileName(image.FileName, imageId.ToString()), image.OpenReadStream()),
            Folder = folderName,
            UseFilename = true,
            UniqueFilename = false
        };

        var uploadResult = await this.cloudinary.UploadAsync(uploadParams);

        var secureUrl = this.cloudinary.Api.UrlImgUp.Secure().BuildUrl($"{uploadResult.PublicId}.{uploadResult.Format}");

        return secureUrl;
    }

    private static string GetUniqueFileName(string fileName, string imageId)
    {
        string fileExtension = Path.GetExtension(fileName);
        return $"{imageId}{fileExtension}";
    }
}