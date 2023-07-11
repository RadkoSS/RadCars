// ReSharper disable IdentifierTypo
namespace RadCars.Services.Data;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Web.ViewModels.City;
using Web.ViewModels.CarMake;
using Web.ViewModels.Feature;
using Web.ViewModels.Listing;
using Web.ViewModels.CarImage;
using Web.ViewModels.Thumbnail;
using RadCars.Data.Models.Entities;
using Web.ViewModels.CarEngineType;
using Web.ViewModels.FeatureCategory;
using RadCars.Data.Common.Contracts.Repositories;

using static Common.ExceptionsErrorMessages;

public class ListingService : IListingService
{
    private readonly ICarService carService;
    private readonly ICloudinaryImageService cloudinaryImageService;

    private readonly IDeletableEntityRepository<City> citiesRepository;
    private readonly IDeletableEntityRepository<Listing> listingsRepository;
    private readonly IDeletableEntityRepository<CarMake> carMakesRepository;
    private readonly IDeletableEntityRepository<Feature> featuresRepository;
    private readonly IDeletableEntityRepository<CarImage> carImagesRepository;
    private readonly IDeletableEntityRepository<Category> categoriesRepository;
    private readonly IDeletableEntityRepository<EngineType> engineTypesRepository;

    //ToDo: Implement AutoMapper
    public ListingService(ICloudinaryImageService cloudinaryImageService, ICarService carService, IDeletableEntityRepository<Listing> listingsRepository, IDeletableEntityRepository<CarMake> carMakesRepository, IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<City> citiesRepository, IDeletableEntityRepository<EngineType> engineTypesRepository, IDeletableEntityRepository<CarImage> carImagesRepository, IDeletableEntityRepository<Feature> featuresRepository)
    {
        this.carService = carService;
        this.citiesRepository = citiesRepository;
        this.carMakesRepository = carMakesRepository;
        this.featuresRepository = featuresRepository;
        this.listingsRepository = listingsRepository;
        this.carImagesRepository = carImagesRepository;
        this.categoriesRepository = categoriesRepository;
        this.engineTypesRepository = engineTypesRepository;
        this.cloudinaryImageService = cloudinaryImageService;
    }

    public async Task<IEnumerable<ListingViewModel>> GetAllListingsAsync()
    {
        var listings = await this.listingsRepository.AllAsNoTracking().OrderByDescending(l => l.CreatedOn).Select(l => new ListingViewModel
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
        => await this.carMakesRepository.AllAsNoTracking().Select(cm => new CarMakeViewModel
        {
            Id = cm.Id,
            Name = cm.Name
        }).ToArrayAsync();

    public async Task<IEnumerable<FeatureCategoriesViewModel>> GetFeatureCategoriesAsync()
        => await this.categoriesRepository.AllAsNoTracking().Select(c => new FeatureCategoriesViewModel
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
        => await this.citiesRepository.AllAsNoTracking().Select(c => new CityViewModel
        {
            Id = c.Id,
            Name = c.Name
        }).ToArrayAsync();

    public async Task<IEnumerable<EngineTypeViewModel>> GetEngineTypesAsync()
        => await this.engineTypesRepository.AllAsNoTracking().Select(et => new EngineTypeViewModel
        {
            Id = et.Id,
            Name = et.Name
        }).ToArrayAsync();

    public async Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId)
    {
        var listingToDisplay = await this.listingsRepository.AllAsNoTracking()
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
            await this.listingsRepository.All().FirstAsync(l => l.Id.ToString() == listingId && l.CreatorId.ToString() == userId);

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

        var uploadedImages = await this.cloudinaryImageService.UploadMultipleImagesAsync(listing.Id.ToString(), form.Images);

        if (!uploadedImages.Any())
        {
            throw new ApplicationException(ImagesUploadUnsuccessful);
        }

        listing.Images = uploadedImages;

        await this.listingsRepository.AddAsync(listing);

        await this.listingsRepository.SaveChangesAsync();

        var firstImageThumbnail = listing.Images.First();

        listing.ThumbnailId = firstImageThumbnail.Id;

        await this.listingsRepository.SaveChangesAsync();

        return listing.Id.ToString();
    }

    public async Task AddThumbnailToListingByIdAsync(string listingId, string imageId, string userId)
    {
        var imageAndListingExist = await this.carImagesRepository.All().
            AnyAsync(ci => ci.Id.ToString() == imageId && ci.ListingId.ToString() == listingId && ci.Listing.CreatorId.ToString() == userId);

        if (!imageAndListingExist)
        {
            throw new InvalidOperationException(InvalidImageForThumbnailProvided);
        }

        var listing = await this.listingsRepository.All().FirstAsync(l => l.Id.ToString() == listingId);

        listing.ThumbnailId = Guid.Parse(imageId);

        await this.listingsRepository.SaveChangesAsync();
    }

    private async Task<bool> ValidateForm(ListingFormModel form)
    {
        var carMakeIdExists = await this.carMakesRepository.All().AnyAsync(m => m.Id == form.CarMakeId);
        var carModelsByMakeId = await this.carService.GetModelsByMakeIdAsync(form.CarMakeId);
        var carModelIdExists = carModelsByMakeId.Any(m => m.Id == form.CarModelId);
        var engineTypeIdExists = await this.engineTypesRepository.All().AnyAsync(t => t.Id == form.EngineTypeId);
        var cityIdExists = await this.citiesRepository.All().AnyAsync(c => c.Id == form.CityId);

        var features = await this.featuresRepository.All().Select(f => f.Id).ToArrayAsync();

        bool featureIdsExist = true;
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