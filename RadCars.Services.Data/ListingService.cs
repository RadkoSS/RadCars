// ReSharper disable IdentifierTypo
namespace RadCars.Services.Data;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Mapping;
using Contracts;
using Web.ViewModels.City;
using Web.ViewModels.CarMake;
using Web.ViewModels.Listing;
using Web.ViewModels.Thumbnail;
using RadCars.Data.Models.Entities;
using Web.ViewModels.CarEngineType;
using Web.ViewModels.FeatureCategory;
using RadCars.Web.ViewModels.Feature;
using RadCars.Data.Common.Contracts.Repositories;

using static Common.ExceptionsErrorMessages;

public class ListingService : IListingService
{
    private readonly IMapper mapper;
    private readonly ICarService carService;
    private readonly ICloudinaryImageService cloudinaryImageService;

    private readonly IDeletableEntityRepository<City> citiesRepository;
    private readonly IDeletableEntityRepository<Listing> listingsRepository;
    private readonly IDeletableEntityRepository<CarMake> carMakesRepository;
    private readonly IDeletableEntityRepository<Feature> featuresRepository;
    private readonly IDeletableEntityRepository<CarImage> carImagesRepository;
    private readonly IDeletableEntityRepository<Category> categoriesRepository;
    private readonly IDeletableEntityRepository<EngineType> engineTypesRepository;
    
    public ListingService(ICloudinaryImageService cloudinaryImageService, ICarService carService, 
        IDeletableEntityRepository<Listing> listingsRepository, IDeletableEntityRepository<CarMake> carMakesRepository, IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<City> citiesRepository, IDeletableEntityRepository<EngineType> engineTypesRepository, IDeletableEntityRepository<CarImage> carImagesRepository, IDeletableEntityRepository<Feature> featuresRepository, IMapper mapper)
    {
        this.mapper = mapper;
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
            .Where(l => l.Id.ToString() == listingId).SelectMany(l => l.ListingFeatures).ToArrayAsync();

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
                })
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
        listing.CreatorId = Guid.Parse(userId);

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