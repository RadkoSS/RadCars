// ReSharper disable IdentifierTypo
namespace RadCars.Services.Data;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Contracts;
using RadCars.Data.Models.Entities;
using RadCars.Data.Common.Contracts.Repositories;

using static Common.ExceptionsErrorMessages;

public class CloudinaryImageService : ICloudinaryImageService
{
    private readonly IDeletableEntityRepository<CarImage> carImagesRepository;

    private readonly IConfiguration configuration;

    private readonly Cloudinary cloudinary;

    public CloudinaryImageService(IDeletableEntityRepository<CarImage> carImagesRepository, IConfiguration configuration)
    {
        this.carImagesRepository = carImagesRepository;

        this.configuration = configuration;

        this.cloudinary = new Cloudinary(new Account(this.configuration.GetSection("ExternalConnections:Cloudinary:CloudName").Value, this.configuration.GetSection("ExternalConnections:Cloudinary:ApiKey").Value, this.configuration.GetSection("ExternalConnections:Cloudinary:ApiSecret").Value));
        this.cloudinary.Api.Secure = true;
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

        var uploadResult = await cloudinary.UploadAsync(uploadParams);

        var uploadedImage = new CarImage
        {
            Id = imageId,
            Url = uploadResult.Url.ToString()
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
        var carImage = await this.carImagesRepository.All().FirstOrDefaultAsync(cp => cp.ListingId.ToString() == listingId && cp.Id.ToString() == imageId);

        if (carImage == null)
        {
            throw new InvalidOperationException(ImageDoesNotExistError);
        }

        var deletionParams = new DeletionParams($"{listingId}/{imageId}");

        var deletionResult = await cloudinary.DestroyAsync(deletionParams);

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
    }

    private static string GetUniqueFileName(string fileName, string imageId)
    {
        string fileExtension = Path.GetExtension(fileName);
        return $"{imageId}{fileExtension}";
    }
}