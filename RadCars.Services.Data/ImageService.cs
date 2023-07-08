namespace RadCars.Services.Data;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Contracts;
using RadCars.Data;
using RadCars.Data.Models.Entities;

using static Common.ExceptionsErrorMessages;

public class ImageService : IImageService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IConfiguration configuration;

    private readonly Cloudinary cloudinary;

    public ImageService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.configuration = configuration;

        this.cloudinary = new Cloudinary(new Account(this.configuration.GetSection("ExternalConnections:Cloudinary:CloudName").Value, this.configuration.GetSection("ExternalConnections:Cloudinary:ApiKey").Value, this.configuration.GetSection("ExternalConnections:Cloudinary:ApiSecret").Value));
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

        var carPicture = new CarImage
        {
            Id = pictureId,
            Url = uploadResult.Url.ToString(),
            ListingId = currentListing.Id
        };

        await this.dbContext.CarImages.AddAsync(carPicture);

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
        var carPicture = await this.dbContext.CarImages.FirstOrDefaultAsync(cp => cp.ListingId.ToString() == listingId && cp.Id.ToString() == imageId);

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

        this.dbContext.CarImages.Remove(carPicture);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task DeleteAllImagesAsync(string listingId, IEnumerable<string> imagesIds)
    {
        foreach (var imageId in imagesIds)
        {
            await this.DeleteImageAsync(listingId, imageId);
        }
    }

    private string GetUniqueFileName(string fileName, string imageId)
    {
        string fileExtension = Path.GetExtension(fileName);
        return $"{imageId}{fileExtension}";
    }
}