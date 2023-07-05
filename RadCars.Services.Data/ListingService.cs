namespace RadCars.Services.Data;

using Microsoft.EntityFrameworkCore;

using Contracts;
using RadCars.Data;
using Web.ViewModels.Listing;
using Web.ViewModels.CarImage;
using RadCars.Data.Models.Entities;
using Web.ViewModels.CarEngineType;
using Web.ViewModels.CarMake;
using Web.ViewModels.CarModel;
using Web.ViewModels.City;
using Web.ViewModels.Feature;
using Web.ViewModels.FeatureCategory;
using static Common.ExceptionsErrorMessages;

public class ListingService : IListingService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IImageService imageService;

    //ToDo: Implement AutoMapper
    public ListingService(ApplicationDbContext dbContext, IImageService imageService)
    {
        this.dbContext = dbContext;
        this.imageService = imageService;
    }

    public async Task<ListingViewModel[]> GetAllListingsAsync()
    {
        var listings = await this.dbContext.Listings.AsNoTracking().Select(l => new ListingViewModel
        {
            Id = l.Id.ToString(),
            CreatorName = l.Creator.UserName,
            Description = l.Description,
            Title = l.Title,
            Year = l.Year,
            Thumbnail = new ImageViewModel
            {
                Id = l.ThumbnailId.ToString() == null ? l.Images.First().Id.ToString() : l.ThumbnailId.ToString()!,
                Url = l.Thumbnail == null ? l.Images.First().Url : l.Thumbnail.Url
            }
        }).ToArrayAsync();

        return listings;
    }

    public async Task<ListingFormModel> GetListingCreateAsync()
    {
        var formModel = new ListingFormModel
        {
            CarMakes = await this.dbContext.CarMakes.AsNoTracking().Select(cm => new CarMakeViewModel
            {
                Id = cm.Id,
                Name = cm.Name
            }).ToArrayAsync(),
            FeatureCategories = await this.dbContext.Categories.AsNoTracking().Select(c => new FeatureCategoriesViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Features = c.Features.Select(f => new FeatureViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    CategoryId = f.CategoryId
                })
            }).ToArrayAsync(),
            Cities = await this.dbContext.Cities.AsNoTracking().Select(c => new CityViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToArrayAsync(),
            EngineTypes = await this.dbContext.EngineTypes.AsNoTracking().Select(et => new CarEngineTypeViewModel
            {
                Id = et.Id,
                Name = et.Name
            }).ToArrayAsync()
        };

        //Empty array on initialize - will be dynamically loaded via the API.
        formModel.CarModels = Array.Empty<CarModelViewModel>();

        return formModel;
    }

    public async Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId)
    {
        var listingToDisplay = await this.dbContext.Listings.AsNoTracking()
            .Where(l => l.Id.ToString() == listingId)
            .Select(l => new ListingDetailsViewModel
            {
                Id = l.Id.ToString(),
                Title = l.Title,
                CreatorName = l.Creator.UserName,
                Description = l.Description,
                Year = l.Year,
                Pictures = l.Images.Select(p => new ImageViewModel
                {
                    Id = p.Id.ToString(),
                    Url = p.Url
                }).ToArray()
            }).FirstAsync();

        return listingToDisplay;
    }

    public async Task CreateListingAsync(ListingFormModel form, string userId)
    {
        //ToDo: Add all the required properties to the form model and implement AutoMapper to the application.

        //ToDo: add validation!!
        var listing = new Listing
        {
            Title = form.Title,
            Description = form.Description,
            Price = form.Price,
            Year = form.Year,
            Mileage = form.Mileage,
            EngineModel = form.EngineModel,
            EngineTypeId = form.EngineTypeId,
            VinNumber = form.VinNumber,
            CreatorId = Guid.Parse(userId),
            CarMakeId = form.CarMakeId,
            CarModelId = form.CarModelId,
            CityId = form.CityId
        };

        foreach (var featureId in form.SelectedFeatures)
        {
            listing.ListingFeatures.Add(new ListingFeature
            {
                FeatureId = featureId,
                Listing = listing
            });
        }

        await this.dbContext.Listings.AddAsync(listing);

        await this.dbContext.SaveChangesAsync();

        await this.imageService.UploadMultipleImagesAsync(listing.Id.ToString(), form.Images);
    }

    public async Task AddThumbnailToListingByIdAsync(string listingId, string imageId)
    {
        var imageExists = await this.dbContext.CarImages.AsNoTracking().AnyAsync(ci => ci.Id.ToString() == imageId);

        if (!imageExists)
        {
            throw new InvalidOperationException(InvalidImageForThumbnailProvided);
        }

        var listing = await this.dbContext.Listings.FirstAsync(l => l.Id.ToString() == listingId);

        listing.ThumbnailId = Guid.Parse(imageId);

        await this.dbContext.SaveChangesAsync();
    }
}