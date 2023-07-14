// ReSharper disable IdentifierTypo
namespace RadCars.Services.Data;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Mapping;
using Contracts;
using Web.ViewModels.City;
using Web.ViewModels.Home;
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
    private readonly IMapper mapper;
    private readonly ICarService carService;
    private readonly IImageService imageService;
    
    private readonly IDeletableEntityRepository<City> citiesRepository;
    private readonly IDeletableEntityRepository<Listing> listingsRepository;
    private readonly IDeletableEntityRepository<CarMake> carMakesRepository;
    private readonly IDeletableEntityRepository<Feature> featuresRepository;
    private readonly IDeletableEntityRepository<CarImage> carImagesRepository;
    private readonly IDeletableEntityRepository<Category> categoriesRepository;
    private readonly IDeletableEntityRepository<EngineType> engineTypesRepository;
    private readonly IDeletableEntityRepository<ListingFeature> listingFeaturesRepository;
    private readonly IDeletableEntityRepository<UserFavoriteListing> userFavoriteListingsRepository;

    public ListingService(IImageService imageService, ICarService carService,
        IDeletableEntityRepository<Listing> listingsRepository, IDeletableEntityRepository<CarMake> carMakesRepository, IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<City> citiesRepository, IDeletableEntityRepository<EngineType> engineTypesRepository, IDeletableEntityRepository<CarImage> carImagesRepository, IDeletableEntityRepository<Feature> featuresRepository, IMapper mapper, IDeletableEntityRepository<ListingFeature> listingFeaturesRepository, IDeletableEntityRepository<UserFavoriteListing> userFavoriteListingsRepository)
    {
        this.mapper = mapper;
        this.carService = carService;
        this.imageService = imageService;
        this.citiesRepository = citiesRepository;
        this.carMakesRepository = carMakesRepository;
        this.featuresRepository = featuresRepository;
        this.listingsRepository = listingsRepository;
        this.carImagesRepository = carImagesRepository;
        this.categoriesRepository = categoriesRepository;
        this.engineTypesRepository = engineTypesRepository;
        this.listingFeaturesRepository = listingFeaturesRepository;
        this.userFavoriteListingsRepository = userFavoriteListingsRepository;
    }

    public async Task<IEnumerable<AllListingViewModel>> GetAllListingsAsync()
    {
        var listings = await this.listingsRepository
            .AllAsNoTracking()
            .Where(l => l.ThumbnailId != null)
            .OrderByDescending(l => l.CreatedOn)
            .To<AllListingViewModel>()
            .ToArrayAsync();
        
        return listings;
    }

    public async Task<IEnumerable<AllListingViewModel>> GetAllListingsByUserIdAsync(string userId)
    {
        var listings = await this.listingsRepository
            .AllAsNoTracking()
            .Where(l => l.CreatorId.ToString() == userId)
            .OrderByDescending(l => l.CreatedOn)
            .To<AllListingViewModel>()
            .ToArrayAsync();

        return listings;
    }

    public async Task<IEnumerable<AllListingViewModel>> GetFavoriteListingsByUserIdAsync(string userId)
    {
        var favoriteListings = await this.userFavoriteListingsRepository.All()
            .Where(ufl => ufl.UserId.ToString() == userId)
            .Select(ufl => new AllListingViewModel
                {
                    Id = ufl.ListingId.ToString(),
                    CarMakeName = ufl.Listing.CarMake.Name,
                    CarModelName = ufl.Listing.CarModel.Name,
                    City = new CityViewModel
                    {
                        Id = ufl.Listing.City.Id,
                        Name = ufl.Listing.City.Name,
                        Latitude = ufl.Listing.City.Latitude,
                        Longitude = ufl.Listing.City.Longitude
                    },
                    Thumbnail = new ImageViewModel
                    {
                        Id = ufl.Listing.ThumbnailId.ToString()!,
                        Url = ufl.Listing.Thumbnail!.Url
                    },
                    CreatorId = userId,
                    EngineModel = ufl.Listing.EngineModel,
                    Mileage = ufl.Listing.Mileage,
                    Title = ufl.Listing.Title,
                    Price = ufl.Listing.Price,
                    Year = ufl.Listing.Year
                }
            ).ToArrayAsync();

        return favoriteListings;
    }

    public async Task<bool> IsListingInUserFavoritesByIdAsync(string listingId, string userId)
    {
        var result = await this.userFavoriteListingsRepository.All().AnyAsync(ufl => ufl.ListingId.ToString() == listingId && ufl.UserId.ToString() == userId);

        return result;
    }

    public async Task FavoriteListingByIdAsync(string listingId, string userId)
    {
        var listingToFavorite = await this.listingsRepository.All().FirstAsync(l => l.Id.ToString() == listingId && l.CreatorId.ToString() != userId);

        if (await this.IsListingInUserFavoritesByIdAsync(listingId, userId))
        {
            throw new InvalidOperationException(ListingIsAlreadyInCurrentUserFavorites);
        }

        var userFavoriteListing = new UserFavoriteListing
        {
            UserId = Guid.Parse(userId),
            ListingId = listingToFavorite.Id
        };

        await this.userFavoriteListingsRepository.AddAsync(userFavoriteListing);

        await this.userFavoriteListingsRepository.SaveChangesAsync();
    }

    public async Task<int> GetFavoritesCountForListingByIdAsync(string listingId)
    {
        var count = await this.userFavoriteListingsRepository.AllAsNoTracking()
            .CountAsync(ufl => ufl.ListingId.ToString() == listingId);

        return count;
    }

    public async Task UnFavoriteListingByIdAsync(string listingId, string userId)
    {
        var listingToUnFavorite = await this.userFavoriteListingsRepository.All().FirstAsync(l => l.ListingId.ToString() == listingId && l.UserId.ToString() == userId);

        this.userFavoriteListingsRepository.HardDelete(listingToUnFavorite);

        await this.userFavoriteListingsRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<AllListingViewModel>> GetAllDeactivatedListingsByUserIdAsync(string userId)
    {
        var deactivatedListings = await this.listingsRepository
            .AllAsNoTrackingWithDeleted()
            .Where(l => l.CreatorId.ToString() == userId && l.IsDeleted)
            .OrderByDescending(l => l.CreatedOn)
            .To<AllListingViewModel>()
            .ToArrayAsync();

        return deactivatedListings;
    }

    public async Task<IEnumerable<IndexViewModel>> GetMostRecentListingsAsync()
    {
        var mostRecentListings = await this.listingsRepository.AllAsNoTracking()
            .OrderByDescending(l => l.CreatedOn)
            .Take(3)
            .To<IndexViewModel>()
            .ToArrayAsync();

        return mostRecentListings;
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
        => await this.carMakesRepository.AllAsNoTracking()
            .To<CarMakeViewModel>().ToArrayAsync();

    public async Task<IEnumerable<FeatureCategoriesViewModel>> GetFeatureCategoriesAsync()
        => await this.categoriesRepository.AllAsNoTracking().To<FeatureCategoriesViewModel>().ToArrayAsync();

    public async Task<IEnumerable<CityViewModel>> GetCitiesAsync()
        => await this.citiesRepository.AllAsNoTracking().To<CityViewModel>().ToArrayAsync();

    public async Task<IEnumerable<EngineTypeViewModel>> GetEngineTypesAsync()
        => await this.engineTypesRepository.AllAsNoTracking().To<EngineTypeViewModel>().ToArrayAsync();

    public async Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId)
    {
        var detailsViewModel = await this.listingsRepository.AllAsNoTracking()
            .Where(l => l.Id.ToString() == listingId).To<ListingDetailsViewModel>().FirstAsync();

        var listingFeatures = await this.listingsRepository.All()
            .Include(l => l.ListingFeatures)
            .ThenInclude(lf => lf.Feature)
            .ThenInclude(f => f.Category)
            .AsNoTracking()
            .Where(l => l.Id.ToString() == listingId)
            .SelectMany(l => l.ListingFeatures)
            .ToArrayAsync();

        foreach (var currentLf in listingFeatures.DistinctBy(lf => lf.Feature.CategoryId))
        {
            var featuresOfCategory = listingFeatures.Where(l => l.Feature.CategoryId == currentLf.Feature.CategoryId);

            detailsViewModel.ListingFeatures.Add(new FeatureCategoriesViewModel
            {
                Id = currentLf.Feature.CategoryId,
                Name = currentLf.Feature.Category.Name,
                Features = featuresOfCategory.Select(f => new FeatureViewModel
                {
                    Id = f.FeatureId,
                    Name = f.Feature.Name,
                    IsSelected = true
                }).ToArray()
            });
        }

        return detailsViewModel;
    }

    public async Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string listingId, string userId)
    {
        var chooseThumbnailViewModel =
             await this.listingsRepository.All()
                 .Where(l => l.Id.ToString() == listingId && l.CreatorId.ToString() == userId)
                 .To<ChooseThumbnailFormModel>()
                 .FirstAsync();

        return chooseThumbnailViewModel;
    }

    public async Task<string> CreateListingAsync(ListingFormModel form, string userId)
    {
        if (await ValidateForm(form) == false)
        {
            throw new InvalidDataException(InvalidDataProvidedError);
        }

        var listing = this.mapper.Map<Listing>(form);

        listing.Id = Guid.NewGuid();
        listing.CreatorId = Guid.Parse(userId);

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

        await this.listingsRepository.AddAsync(listing);

        await this.listingsRepository.SaveChangesAsync();

        var firstImageThumbnail = listing.Images.First();

        listing.ThumbnailId = firstImageThumbnail.Id;

        await this.listingsRepository.SaveChangesAsync();

        return listing.Id.ToString();
    }

    public async Task AddThumbnailToListingByIdAsync(string listingId, string imageId, string userId)
    {
        var imageAndListingExist = await this.carImagesRepository
            .All()
            .AnyAsync(ci => ci.Id.ToString() == imageId && ci.ListingId.ToString() == listingId && ci.Listing.CreatorId.ToString() == userId);

        if (!imageAndListingExist)
        {
            throw new InvalidOperationException(InvalidImageForThumbnailProvided);
        }

        var listing = await this.listingsRepository.All().FirstAsync(l => l.Id.ToString() == listingId);

        listing.ThumbnailId = Guid.Parse(imageId);

        await this.listingsRepository.SaveChangesAsync();
    }

    public async Task DeactivateListingByIdAsync(string listingId, string userId)
    {
        var listingToDeactivate = await this.listingsRepository.All().FirstAsync(l => l.CreatorId.ToString() == userId && l.Id.ToString() == listingId);

        this.listingsRepository.Delete(listingToDeactivate);

        await this.listingsRepository.SaveChangesAsync();
    }

    public async Task ReactivateListingByIdAsync(string listingId, string userId)
    {
        var listingToDeactivate = await this.listingsRepository.AllWithDeleted().FirstAsync(l => l.CreatorId.ToString() == userId && l.Id.ToString() == listingId);

        this.listingsRepository.Undelete(listingToDeactivate);

        await this.listingsRepository.SaveChangesAsync();
    }

    public async Task HardDeleteListingByIdAsync(string listingId, string userId)
    {
        var listingToDelete = await this.listingsRepository.AllWithDeleted().FirstAsync(l => l.CreatorId.ToString() == userId && l.Id.ToString() == listingId);

        var listingFeaturesToDelete = await this.listingFeaturesRepository.AllWithDeleted().Where(lf => lf.ListingId == listingToDelete.Id).ToArrayAsync();

        foreach (var listingFeature in listingFeaturesToDelete)
        {
            this.listingFeaturesRepository.HardDelete(listingFeature);
        }
        await this.listingFeaturesRepository.SaveChangesAsync();

        var carImagesToDelete =
            await this.carImagesRepository.AllWithDeleted().Where(ci => ci.ListingId == listingToDelete.Id).ToArrayAsync();

        foreach (var carImage in carImagesToDelete)
        {
            this.carImagesRepository.HardDelete(carImage);
        }
        await carImagesRepository.SaveChangesAsync();

        var userFavorites = await this.userFavoriteListingsRepository.AllWithDeleted()
            .Where(ufl => ufl.ListingId == listingToDelete.Id).ToArrayAsync();
        foreach (var userFavorite in userFavorites)
        {
            this.userFavoriteListingsRepository.HardDelete(userFavorite);
        }
        await this.userFavoriteListingsRepository.SaveChangesAsync();

        this.listingsRepository.HardDelete(listingToDelete);

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