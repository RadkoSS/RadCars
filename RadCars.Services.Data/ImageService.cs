namespace RadCars.Services.Data;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Contracts;
using RadCars.Data;
using RadCars.Data.Models.Entities;

using static Common.SecretCredentials;
using static Common.ExceptionsErrorMessages;

public class ImageService : IImageService
{
    private static readonly Cloudinary cloudinary 
        = new Cloudinary(new Account(CloudinaryCloudName, CloudinaryApiKey, CloudinaryApiSecret));

    private readonly ApplicationDbContext dbContext;

    public ImageService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task UploadImageAsync(string listingId, IFormFile image)
    {
        var currentListing = await this.dbContext.Listings.FirstOrDefaultAsync(l => l.Id.ToString() == listingId);

        if (currentListing == null)
        {
            throw new InvalidOperationException(ListingDoesNotExistError);
        }

        var pictureId = Guid.NewGuid();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(GetUniqueFileName(image.FileName, pictureId.ToString()), image.OpenReadStream()),
            Folder = listingId,
            UseFilename = true,
            UniqueFilename = false
        };

        var uploadResult = await cloudinary.UploadAsync(uploadParams);

        var carPicture = new CarPicture
        {
            Id = pictureId,
            Url = uploadResult.Url.ToString(),
            ListingId = currentListing.Id
        };

        await this.dbContext.CarPictures.AddAsync(carPicture);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task UploadMultipleImagesAsync(string listingId, IEnumerable<IFormFile> images)
    {
        foreach (var picture in images)
        {
            await this.UploadImageAsync(listingId, picture);
        }
    }

    public async Task DeleteImageAsync(string listingId, string imageId)
    {
        var carPicture = await this.dbContext.CarPictures.FirstOrDefaultAsync(cp => cp.ListingId.ToString() == listingId && cp.Id.ToString() == imageId);

        if (carPicture == null)
        {
            throw new InvalidOperationException(ImageDoesNotExistError);
        }

        var deletionParams = new DeletionParams($"{listingId}/{imageId}");

        var deletionResult = await cloudinary.DestroyAsync(deletionParams);

        if (deletionResult.Result != "ok")
        {
            throw new InvalidOperationException(ImageDeleteUnsuccessful);
        }

        this.dbContext.CarPictures.Remove(carPicture);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task DeleteAllImagesAsync(string listingId, IEnumerable<string> imagesIds)
    {
        foreach (var pictureId in imagesIds)
        {
            await this.DeleteImageAsync(listingId, pictureId);
        }
    }

    private string GetUniqueFileName(string fileName, string pictureId)
    {
        string fileExtension = Path.GetExtension(fileName);
        return $"{pictureId}{fileExtension}";
    }
}