﻿namespace RadCars.Services.Data;

using Microsoft.EntityFrameworkCore;

using Contracts;
using RadCars.Data;
using Web.ViewModels.City;
using Web.ViewModels.CarMake;
using Web.ViewModels.Feature;
using Web.ViewModels.Listing;
using Web.ViewModels.CarImage;
using Web.ViewModels.Thumbnail;
using RadCars.Data.Models.Entities;
using Web.ViewModels.CarEngineType;
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

    public async Task<IEnumerable<ListingViewModel>> GetAllListingsAsync()
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
                Id = l.ThumbnailId == null ? l.Images.First().Id.ToString() : l.ThumbnailId.ToString()!,
                Url = l.Thumbnail == null ? l.Images.First().Url : l.Thumbnail.Url
            }
        }).ToArrayAsync();

        return listings;
    }

    public async Task<ListingFormModel> GetListingCreateAsync()
    {
        var formModel = new ListingFormModel
        {
            CarMakes = await GetCarMakesAsync(),
            FeatureCategories = await GetFeatureCategoriesAsync(),
            Cities = await GetCitiesAsync(),
            EngineTypes = await GetEngineTypesAsync()
        };

        return formModel;
    }

    public async Task<IEnumerable<CarMakeViewModel>> GetCarMakesAsync()
        => await this.dbContext.CarMakes.AsNoTracking().Select(cm => new CarMakeViewModel
        {
            Id = cm.Id,
            Name = cm.Name
        }).ToArrayAsync();

    public async Task<IEnumerable<FeatureCategoriesViewModel>> GetFeatureCategoriesAsync()
        => await this.dbContext.Categories.AsNoTracking().Select(c => new FeatureCategoriesViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Features = c.Features.Select(f => new FeatureViewModel
            {
                Id = f.Id,
                Name = f.Name
            })
        }).ToArrayAsync();

    public async Task<IEnumerable<CityViewModel>> GetCitiesAsync()
        => await this.dbContext.Cities.AsNoTracking().Select(c => new CityViewModel
        {
            Id = c.Id,
            Name = c.Name
        }).ToArrayAsync();

    public async Task<IEnumerable<EngineTypeViewModel>> GetEngineTypesAsync()
        => await this.dbContext.EngineTypes.AsNoTracking().Select(et => new EngineTypeViewModel
        {
            Id = et.Id,
            Name = et.Name
        }).ToArrayAsync();

    public async Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId)
    {
        var listingToDisplay = await this.dbContext.Listings.AsNoTracking()
            .Where(l => l.Id.ToString() == listingId)
            .Select(l => new ListingDetailsViewModel
            {
                Id = l.Id.ToString(),
                Title = l.Title,
                CreatorId = l.CreatorId.ToString(),
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

    public async Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string listingId, string userId)
    {
        var currentListing =
            await this.dbContext.Listings.FirstAsync(l => l.Id.ToString() == listingId && l.CreatorId.ToString() == userId);

        var chooseThumbnailViewModel = new ChooseThumbnailFormModel
        {
            ListingId = currentListing.Id.ToString(),
            Images = currentListing.Images.Select(p => new ImageViewModel
            {
                Id = p.Id.ToString(),
                Url = p.Url
            }).ToArray()
        };

        return chooseThumbnailViewModel;
    }

    public async Task<string> CreateListingAsync(ListingFormModel form, string userId)
    {
        if (await ValidateForm(form) == false)
        {
            throw new InvalidDataException(InvalidDataProvidedError);
        }

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

        foreach (var id in form.SelectedFeatures)
        {
            listing.ListingFeatures.Add(new ListingFeature
            {
                FeatureId = id,
                Listing = listing
            });
        }

        var uploadedImages = await this.imageService.UploadMultipleImagesAsync(listing.Id.ToString(), form.Images);

        if (!uploadedImages.Any())
        {
            throw new ApplicationException(ImagesUploadUnsuccessful);
        }

        listing.Images = uploadedImages;

        await this.dbContext.Listings.AddAsync(listing);

        await this.dbContext.SaveChangesAsync();

        var firstImageThumbnail = listing.Images.First();

        listing.ThumbnailId = firstImageThumbnail.Id;

        await this.dbContext.SaveChangesAsync();

        return listing.Id.ToString();
    }

    public async Task AddThumbnailToListingByIdAsync(string listingId, string imageId, string userId)
    {
        var imageAndListingExist = await this.dbContext.CarImages.
            AnyAsync(ci => ci.Id.ToString() == imageId && ci.ListingId.ToString() == listingId && ci.Listing.CreatorId.ToString() == userId);

        if (!imageAndListingExist)
        {
            throw new InvalidOperationException(InvalidImageForThumbnailProvided);
        }

        var listing = await this.dbContext.Listings.FirstAsync(l => l.Id.ToString() == listingId);

        listing.ThumbnailId = Guid.Parse(imageId);

        await this.dbContext.SaveChangesAsync();
    }

    private async Task<bool> ValidateForm(ListingFormModel form)
    {
        var engineTypeIdExists = await this.dbContext.EngineTypes.AsNoTracking().AnyAsync(t => t.Id == form.EngineTypeId);
        var carMakeIdExists = await this.dbContext.CarMakes.AsNoTracking().AnyAsync(m => m.Id == form.CarMakeId);
        var carModelIdExists = await this.dbContext.CarModels.AsNoTracking().AnyAsync(m => m.Id == form.CarModelId);
        var cityIdExists = await this.dbContext.Cities.AsNoTracking().AnyAsync(c => c.Id == form.CityId);
        bool featureIdsExist = true;

        var features = await this.dbContext.Features.AsNoTracking().Select(f => f.Id).ToArrayAsync();

        foreach (var featureId in form.SelectedFeatures)
        {
            if (!features.Contains(featureId))
            {
                featureIdsExist = false;
                break;
            }
        }

        return engineTypeIdExists && carMakeIdExists && carModelIdExists && cityIdExists && featureIdsExist;
    }
}