// ReSharper disable IdentifierTypo
namespace RadCars.Services.Data;

using Ganss.Xss;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Mapping;
using Contracts;
using Models.Listing;
using Common.Exceptions;
using Web.ViewModels.City;
using Web.ViewModels.Home;
using Web.ViewModels.CarMake;
using Web.ViewModels.Feature;
using Web.ViewModels.Listing;
using Web.ViewModels.CarImage;
using Web.ViewModels.Thumbnail;
using Web.ViewModels.Listing.Enums;
using RadCars.Data.Models.Entities;
using Web.ViewModels.CarEngineType;
using Web.ViewModels.FeatureCategory;
using RadCars.Data.Common.Contracts.Repositories;

using static Common.GeneralApplicationConstants;
using static Common.ExceptionsAndNotificationsMessages;

public class ListingService : IListingService
{
    private readonly IMapper mapper;
    private readonly IHtmlSanitizer htmlSanitizer;

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
        IDeletableEntityRepository<Listing> listingsRepository, IDeletableEntityRepository<CarMake> carMakesRepository, IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<City> citiesRepository, IDeletableEntityRepository<EngineType> engineTypesRepository, IDeletableEntityRepository<CarImage> carImagesRepository, IDeletableEntityRepository<Feature> featuresRepository, IMapper mapper, IDeletableEntityRepository<ListingFeature> listingFeaturesRepository, IDeletableEntityRepository<UserFavoriteListing> userFavoriteListingsRepository, IHtmlSanitizer htmlSanitizer)
    {
        this.mapper = mapper;
        this.carService = carService;
        this.imageService = imageService;
        this.htmlSanitizer = htmlSanitizer;
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

    public async Task<AllListingsFilteredAndPagedServiceModel> GetAllListingsAsync(AllListingsQueryModel queryModel)
    {
        var listingsQuery = this.listingsRepository.All().AsQueryable().Where(l => l.ThumbnailId != null);

        if (queryModel.CarMakeId.HasValue)
        {
            listingsQuery = listingsQuery.Where(l => l.CarMakeId == queryModel.CarMakeId.Value);
        }

        if (queryModel.CarModelId.HasValue)
        {
            listingsQuery = listingsQuery.Where(l => l.CarModelId == queryModel.CarModelId.Value);
        }

        if (queryModel.CityId.HasValue)
        {
            listingsQuery = listingsQuery.Where(l => l.CityId == queryModel.CityId.Value);
        }

        if (queryModel.MaximumMileage > 0)
        {
            listingsQuery = listingsQuery.Where(l => l.Mileage <= queryModel.MaximumMileage);
        }

        if (queryModel.MaximumPrice > 0)
        {
            listingsQuery = listingsQuery.Where(l => l.Price <= queryModel.MaximumPrice);
        }

        if (queryModel.EngineTypeId.HasValue)
        {
            listingsQuery = listingsQuery.Where(l => l.EngineTypeId == queryModel.EngineTypeId.Value);
        }

        if (string.IsNullOrWhiteSpace(queryModel.SearchString) == false)
        {
            var wildCard = $"%{queryModel.SearchString.ToLower()}%";

            listingsQuery = listingsQuery.Where(l => EF.Functions.Like(l.Title, wildCard) || EF.Functions.Like(l.Description, wildCard) || EF.Functions.Like(l.EngineModel, wildCard));
        }

        listingsQuery = queryModel.ListingSorting switch
        {
            ListingSorting.Newest => listingsQuery
                .OrderByDescending(h => h.CreatedOn),
            ListingSorting.Oldest => listingsQuery
                .OrderBy(h => h.CreatedOn),
            ListingSorting.PriceAscending => listingsQuery
                .OrderBy(h => h.Price),
            ListingSorting.PriceDescending => listingsQuery
                .OrderByDescending(h => h.Price),
            _ => listingsQuery
                .OrderByDescending(h => h.CreatedOn)
        };

        var listings = await listingsQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.ListingsPerPage)
            .Take(queryModel.ListingsPerPage)
            .To<AllListingViewModel>()
            .ToArrayAsync();

        var count = listingsQuery.Count();

        var listingsQueryModel = new AllListingsFilteredAndPagedServiceModel
        {
            TotalListingsCount = count,
            Listings = listings
        };
        
        return listingsQueryModel;
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
            .OrderByDescending(l => l.ModifiedOn)
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
    public async Task<ListingEditFormModel> GetListingEditAsync(string listingId, string userId)
    {
        var listingToEdit = await this.listingsRepository.AllWithDeleted()
            .Where(l => l.Id.ToString() == listingId && l.CreatorId.ToString() == userId)
            .To<ListingEditFormModel>()
            .FirstOrDefaultAsync();

        if (listingToEdit == null)
        {
            throw new InvalidOperationException(ListingDoesNotExistError);
        }

        listingToEdit.UploadedImages = await this.GetUploadedImagesForListingByIdAsync(listingId, userId);
        
        listingToEdit.Cities = await GetCitiesAsync();
        listingToEdit.CarMakes = await GetCarMakesAsync();
        listingToEdit.CarModels = await this.carService.GetModelsByMakeIdAsync(listingToEdit.CarMakeId);
        listingToEdit.EngineTypes = await GetEngineTypesAsync();
        listingToEdit.FeatureCategories = await GetFeatureCategoriesAsync();

        var selectedFeaturesWithCategories = await GetSelectedFeaturesByListingIdAsync(listingId);

        foreach (var featuresWithCategory in listingToEdit.FeatureCategories)
        {
            foreach (var selectedFeaturesWithCategory in selectedFeaturesWithCategories)
            {
                foreach (var feature in featuresWithCategory.Features)
                {
                    foreach (var selectedFeature in selectedFeaturesWithCategory.Features)
                    {
                        if (feature.Id == selectedFeature.Id)
                        {
                            feature.IsSelected = true;
                        }
                    }
                }
            }
        }

        return listingToEdit;
    }

    public async Task<string> EditListingAsync(ListingEditFormModel form, string userId)
    {
        var listingToEdit = await this.listingsRepository.AllWithDeleted()
            .Where(l => l.Id.ToString() == form.Id && l.CreatorId.ToString() == userId)
            .FirstAsync();

        if (form.Images.Any() == false && form.DeletedImages.Count() >= listingToEdit.Images.Count)
        {
            throw new InvalidDataException(InvalidDataProvidedError);
        }

        if (listingToEdit.Images.Count - form.DeletedImages.Count() + form.Images.Count() <= 0)
        {
            throw new InvalidDataException(InvalidDataProvidedError);
        }

        if (this.ValidateUploadedImages(form) == false)
        {
            throw new InvalidImagesException(ListingUploadedImagesAreInvalid);
        }

        if (await this.ValidateForm(form) == false)
        {
            throw new InvalidDataException(InvalidDataProvidedError);
        }

        if (form.Images.Any())
        {
            var uploadedImages = await this.imageService.UploadMultipleImagesAsync(listingToEdit.Id.ToString(), form.Images);

            if (!uploadedImages.Any())
            {
                throw new InvalidDataException(ImagesUploadUnsuccessful);
            }

            foreach (var image in uploadedImages)
            {
                await this.carImagesRepository.AddAsync(image);
            }

            await this.carImagesRepository.SaveChangesAsync();
        }

        foreach (var deletedImgId in form.DeletedImages)
        {
            if (listingToEdit.Images.Any(img => img.Id.ToString() == deletedImgId && img.ListingId.ToString() == form.Id.ToLower() && img.Listing.CreatorId.ToString() == userId) == false)
            {
                throw new InvalidDataException(InvalidDataProvidedError);
            }

            if (listingToEdit.ThumbnailId.ToString() == deletedImgId)
            {
                listingToEdit.ThumbnailId = null;
            }

            await this.imageService.DeleteImageAsync(form.Id, deletedImgId);
        }

        form = (this.SanitizeForm(form) as ListingEditFormModel)!;

        listingToEdit.Year = form.Year;
        listingToEdit.Price = form.Price;
        listingToEdit.Title = form.Title;
        listingToEdit.CityId = form.CityId;
        listingToEdit.Mileage = form.Mileage;
        listingToEdit.CarMakeId = form.CarMakeId;
        listingToEdit.VinNumber = form.VinNumber;
        listingToEdit.CarModelId = form.CarModelId;
        listingToEdit.EngineModel = form.EngineModel;
        listingToEdit.Description = form.Description;
        listingToEdit.EngineTypeId = form.EngineTypeId;

        var newSelectedFeatures = new HashSet<ListingFeature>();

        foreach (var id in form.SelectedFeatures)
        {
            newSelectedFeatures.Add(new ListingFeature
            {
                FeatureId = id,
                ListingId = listingToEdit.Id
            });
        }

        foreach (var listingFeature in listingToEdit.ListingFeatures)
        {
            this.listingFeaturesRepository.HardDelete(listingFeature);
        }

        await this.listingFeaturesRepository.SaveChangesAsync();

        listingToEdit.ListingFeatures = newSelectedFeatures;

        var firstImageId = listingToEdit.Images.First().Id.ToString();

        this.listingsRepository.Update(listingToEdit);
        await this.listingsRepository.SaveChangesAsync();

        await this.AddThumbnailToListingByIdAsync(listingToEdit.Id.ToString(), firstImageId, userId);

        return listingToEdit.Id.ToString();
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

    public async Task<IEnumerable<ImageViewModel>> GetUploadedImagesForListingByIdAsync(string listingId, string userId)
    => await this.carImagesRepository.AllWithDeleted()
        .Where(ci => ci.ListingId.ToString() == listingId && ci.Listing.CreatorId.ToString() == userId).To<ImageViewModel>().ToArrayAsync();

    public async Task<int> GetUploadedImagesCountForListingByIdAsync(string listingId)
        => await this.carImagesRepository.AllWithDeleted()
            .Where(ci => ci.ListingId.ToString() == listingId).CountAsync();

    public async Task<ListingDetailsViewModel> GetListingDetailsAsync(string listingId)
    {
        var detailsViewModel = await this.listingsRepository.AllAsNoTracking()
            .Where(l => l.Id.ToString() == listingId).To<ListingDetailsViewModel>().FirstAsync();

        detailsViewModel.ListingFeatures = await this.GetSelectedFeaturesByListingIdAsync(listingId);

        return detailsViewModel;
    }

    private async Task<ICollection<FeatureCategoriesViewModel>> GetSelectedFeaturesByListingIdAsync(string listingId)
    {
        var selectedFeatures = await this.listingsRepository.All()
            .Include(l => l.ListingFeatures)
            .ThenInclude(lf => lf.Feature)
            .ThenInclude(f => f.Category)
            .AsNoTracking()
            .Where(l => l.Id.ToString() == listingId)
            .SelectMany(l => l.ListingFeatures)
            .ToArrayAsync();

        var listingFeatures = new HashSet<FeatureCategoriesViewModel>();

        foreach (var currentLf in selectedFeatures.DistinctBy(lf => lf.Feature.CategoryId))
        {
            var featuresOfCategory = selectedFeatures.Where(l => l.Feature.CategoryId == currentLf.Feature.CategoryId);

            listingFeatures.Add(new FeatureCategoriesViewModel
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

        return listingFeatures;
    }

    public async Task<ListingDetailsViewModel> GetDeactivatedListingDetailsAsync(string listingId, string userId)
    {
        var deactivatedListing = await this.listingsRepository.AllWithDeleted()
            .Where(l => l.IsDeleted && l.CreatorId.ToString() == userId && l.Id.ToString() == listingId)
            .To<ListingDetailsViewModel>()
            .FirstAsync();

        return deactivatedListing;
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

        if (this.ValidateUploadedImages(form) == false)
        {
            throw new InvalidImagesException(ListingUploadedImagesAreInvalid);
        }

        form = this.SanitizeForm(form);

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
            throw new InvalidDataException(ImagesUploadUnsuccessful);
        }

        listing.Images = uploadedImages;

        await this.listingsRepository.AddAsync(listing);

        await this.listingsRepository.SaveChangesAsync();

        var firstImageId = listing.Images.First().Id.ToString();

        await this.AddThumbnailToListingByIdAsync(listing.Id.ToString(), firstImageId, userId);

        return listing.Id.ToString();
    }

    public async Task AddThumbnailToListingByIdAsync(string listingId, string imageId, string userId)
    {
        var imageAndListingExist = await this.carImagesRepository
            .AllWithDeleted()
            .AnyAsync(ci => ci.Id.ToString() == imageId && ci.ListingId.ToString() == listingId && ci.Listing.CreatorId.ToString() == userId);

        if (!imageAndListingExist)
        {
            throw new InvalidOperationException(InvalidImageForThumbnailProvided);
        }

        var listing = await this.listingsRepository.AllWithDeleted().FirstAsync(l => l.Id.ToString() == listingId);

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

        var imageIds =
            await this.carImagesRepository.AllWithDeleted().Where(ci => ci.ListingId == listingToDelete.Id).Select(ci => ci.Id.ToString()).ToArrayAsync();

        await this.imageService.DeleteAllImagesAsync(listingId, imageIds);

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

    private ListingFormModel SanitizeForm(ListingFormModel form)
    {
        form.Title = this.htmlSanitizer.Sanitize(form.Title);
        form.VinNumber = this.htmlSanitizer.Sanitize(form.VinNumber);
        form.Description = this.htmlSanitizer.Sanitize(form.Description);
        form.EngineModel = this.htmlSanitizer.Sanitize(form.EngineModel);

        return form;
    }

    private bool ValidateUploadedImages(ListingFormModel form)
    {
        foreach (var image in form.Images)
        {
            if (image.Length > ImageMaximumSizeInBytes)
            {
                return false;
            }

            if (!image.ContentType.StartsWith("image/"))
            {
                return false;
            }
        }

        return true;
    }
}