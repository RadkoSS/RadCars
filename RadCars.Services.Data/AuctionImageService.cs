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

public class AuctionImageService : IAuctionImageService
{
    private readonly Cloudinary cloudinary;
    private readonly IDeletableEntityRepository<AuctionCarImage> auctionImagesRepository;

    public AuctionImageService(Cloudinary cloudinary, IDeletableEntityRepository<AuctionCarImage> auctionImagesRepository)
    {
        this.cloudinary = cloudinary;

        this.auctionImagesRepository = auctionImagesRepository;
    }

    public async Task<AuctionCarImage> UploadAuctionImageAsync(string auctionId, IFormFile image)
    {
        var imageId = Guid.NewGuid();

        var secureUrl = await this.UploadImageToCloudinaryAsync(auctionId, image, imageId);

        var uploadedImage = new AuctionCarImage
        {
            Id = imageId,
            AuctionId = Guid.Parse(auctionId),
            Url = secureUrl
        };

        return uploadedImage;
    }

    public async Task<ICollection<AuctionCarImage>> UploadMultipleAuctionImagesAsync(string auctionId, IEnumerable<IFormFile> images)
    {
        var uploadedImages = new HashSet<AuctionCarImage>();

        foreach (var image in images)
        {
            var uploadedImage = await UploadAuctionImageAsync(auctionId, image);

            uploadedImages.Add(uploadedImage);
        }

        return uploadedImages;
    }

    public async Task DeleteAuctionImageAsync(string auctionId, string imageId)
    {
        var carImage = await this.auctionImagesRepository.All().FirstOrDefaultAsync(ci => ci.AuctionId.ToString() == auctionId && ci.Id.ToString() == imageId);

        if (carImage == null)
        {
            throw new InvalidOperationException(ImageDoesNotExistError);
        }

        var deletionParams = new DeletionParams($"{auctionId}/{imageId}");

        var deletionResult = await this.cloudinary.DestroyAsync(deletionParams);

        if (deletionResult.Result != "ok")
        {
            throw new InvalidOperationException(ImageDeleteUnsuccessful);
        }

        this.auctionImagesRepository.HardDelete(carImage);
        await this.auctionImagesRepository.SaveChangesAsync();
    }

    public async Task DeleteAllImagesOfAuctionAsync(string auctionId, IEnumerable<string> imagesIds)
    {
        foreach (var imageId in imagesIds)
        {
            await this.DeleteAuctionImageAsync(auctionId, imageId);
        }

        await this.cloudinary.DeleteFolderAsync(auctionId);
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