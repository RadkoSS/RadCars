// ReSharper disable IdentifierTypo
namespace RadCars.Services.Data;

using Ganss.Xss;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Mapping;
using Contracts;
using Models.Listing;
using Common.Exceptions;
using Messaging.Contracts;
using Web.ViewModels.Feature;
using Web.ViewModels.Listing;
using Web.ViewModels.CarImage;
using Web.ViewModels.Thumbnail;
using Web.ViewModels.Listing.Enums;
using RadCars.Data.Models.Entities;
using Web.ViewModels.FeatureCategory;
using RadCars.Data.Common.Contracts.Repositories;

using static Common.GeneralApplicationConstants;
using static Common.ExceptionsAndNotificationsMessages;
using static Messaging.Templates.EmailTemplates.ListingTemplates;

public class ListingService : IListingService
{
    private readonly IMapper mapper;
    private readonly IEmailSender emailSender;
    private readonly IHtmlSanitizer htmlSanitizer;

    private readonly ICarService carService;
    private readonly IListingImageService listingImageService;
    
    private readonly IDeletableEntityRepository<Listing> listingsRepository;
    private readonly IDeletableEntityRepository<CarImage> carImagesRepository;
    private readonly IDeletableEntityRepository<ListingFeature> listingFeaturesRepository;
    private readonly IDeletableEntityRepository<UserFavoriteListing> userFavoriteListingsRepository;

    public ListingService(IListingImageService listingImageService,
        ICarService carService,
        IDeletableEntityRepository<Listing> listingsRepository,
        IDeletableEntityRepository<CarImage> carImagesRepository,
        IMapper mapper, 
        IDeletableEntityRepository<ListingFeature> listingFeaturesRepository,
        IDeletableEntityRepository<UserFavoriteListing> userFavoriteListingsRepository,
        IHtmlSanitizer htmlSanitizer,
        IEmailSender emailSender)
    {
        this.mapper = mapper;
        this.carService = carService;
        this.emailSender = emailSender;
        this.htmlSanitizer = htmlSanitizer;
        this.listingsRepository = listingsRepository;
        this.carImagesRepository = carImagesRepository;
        this.listingImageService = listingImageService;
        this.listingFeaturesRepository = listingFeaturesRepository;
        this.userFavoriteListingsRepository = userFavoriteListingsRepository;
    }

    public async Task<AllListingsFilteredAndPagedServiceModel> GetAllListingsAsync(AllListingsQueryModel queryModel, bool withDeleted)
    {
        IQueryable<Listing> listingsQuery;

        if (withDeleted == false)
        {
            listingsQuery = this.listingsRepository.All().AsQueryable().Where(l => l.ThumbnailId != null);
        }
        else
        {
            listingsQuery = this.listingsRepository.AllWithDeleted().AsQueryable().Where(l => l.IsDeleted == true);
        }

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
                .OrderByDescending(l => l.CreatedOn),
            ListingSorting.Oldest => listingsQuery
                .OrderBy(l => l.CreatedOn),
            ListingSorting.PriceAscending => listingsQuery
                .OrderBy(l => l.Price),
            ListingSorting.PriceDescending => listingsQuery
                .OrderByDescending(l => l.Price),
            _ => listingsQuery
                .OrderByDescending(l => l.CreatedOn)
        };

        if (withDeleted)
        {
            listingsQuery = listingsQuery.OrderByDescending(l => l.DeletedOn);
        }

        var listings = await listingsQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.ListingsPerPage)
            .Take(queryModel.ListingsPerPage)
            .To<AllListingsViewModel>()
            .ToArrayAsync();

        var count = listingsQuery.Count();

        var listingsQueryModel = new AllListingsFilteredAndPagedServiceModel
        {
            TotalListingsCount = count,
            Listings = listings
        };
        
        return listingsQueryModel;
    }

    public async Task<IEnumerable<AllListingsViewModel>> GetAllListingsByUserIdAsync(string userId)
    {
        var listings = await this.listingsRepository
            .AllAsNoTracking()
            .Where(l => l.CreatorId.ToString() == userId)
            .OrderByDescending(l => l.CreatedOn)
            .To<AllListingsViewModel>()
            .ToArrayAsync();

        return listings;
    }

    public async Task<IEnumerable<AllListingsViewModel>> GetFavoriteListingsByUserIdAsync(string userId)
    {
        var favoriteListings = await this.userFavoriteListingsRepository.All()
            .Where(ufl => ufl.UserId.ToString() == userId)
            .To<AllListingsViewModel>().ToArrayAsync();

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

    public async Task<IEnumerable<AllListingsViewModel>> GetAllDeactivatedListingsByUserIdAsync(string userId)
    {
        var deactivatedListings = await this.listingsRepository
            .AllAsNoTrackingWithDeleted()
            .Where(l => l.CreatorId.ToString() == userId && l.IsDeleted)
            .OrderByDescending(l => l.ModifiedOn)
            .To<AllListingsViewModel>()
            .ToArrayAsync();

        return deactivatedListings;
    }

    public async Task<IEnumerable<ListingIndexViewModel>> GetMostRecentListingsAsync()
    {
        var mostRecentListings = await this.listingsRepository.AllAsNoTracking()
            .OrderByDescending(l => l.CreatedOn)
            .Take(3)
            .To<ListingIndexViewModel>()
            .ToArrayAsync();

        return mostRecentListings;
    }

    public async Task<ListingFormModel> GetListingCreateAsync()
    {
        var formModel = new ListingFormModel
        {
            CarMakes = await this.carService.GetCarMakesAsync(),
            FeatureCategories = await this.carService.GetFeaturesWithCategoriesAsync(),
            Cities = await this.carService.GetBulgarianCitiesAsync(),
            EngineTypes = await this.carService.GetEngineTypesAsync()
        };

        return formModel;
    }
    public async Task<ListingEditFormModel> GetListingEditAsync(string listingId, string userId, bool isUserAdmin)
    {
        var listingToEdit = await this.listingsRepository.AllWithDeleted()
            .Where(l => l.Id.ToString() == listingId && l.CreatorId.ToString() == userId || l.Id.ToString() == listingId && isUserAdmin)
            .To<ListingEditFormModel>()
            .FirstOrDefaultAsync();

        if (listingToEdit == null)
        {
            throw new InvalidOperationException(ListingDoesNotExistError);
        }

        listingToEdit.UploadedImages = await this.GetUploadedImagesForListingByIdAsync(listingId, userId, isUserAdmin);
        
        listingToEdit.Cities = await this.carService.GetBulgarianCitiesAsync();
        listingToEdit.CarMakes = await this.carService.GetCarMakesAsync();
        listingToEdit.CarModels = await this.carService.GetModelsByMakeIdAsync(listingToEdit.CarMakeId);
        listingToEdit.EngineTypes = await this.carService.GetEngineTypesAsync();
        listingToEdit.FeatureCategories = await this.carService.GetFeaturesWithCategoriesAsync();

        var selectedFeaturesWithCategories = await GetSelectedFeaturesByListingIdAsync(listingId);

        foreach (var featuresWithCategory in listingToEdit.FeatureCategories)
        {
            foreach (var feature in featuresWithCategory.Features)
            {
                feature.IsSelected = selectedFeaturesWithCategories
                    .SelectMany(sfc => sfc.Features)
                    .Any(sf => sf.Id == feature.Id);
            }
        }

        return listingToEdit;
    }

    public async Task<string> EditListingAsync(ListingEditFormModel form, string userId, bool isUserAdmin)
    {
        var listingToEdit = await this.listingsRepository.AllWithDeleted()
            .Where(l => l.Id.ToString() == form.Id && l.CreatorId.ToString() == userId || l.Id.ToString() == form.Id && isUserAdmin)
            .FirstAsync();

        var oldThumbnailId = listingToEdit.ThumbnailId!.Value;

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
            throw new InvalidImagesException(UploadedImagesAreInvalid);
        }

        if (await this.ValidateForm(form) == false)
        {
            throw new InvalidDataException(InvalidDataProvidedError);
        }

        form = (this.SanitizeForm(form) as ListingEditFormModel)!;

        if (form.Images.Any())
        {
            var uploadedImages = await this.listingImageService.UploadMultipleListingImagesAsync(listingToEdit.Id.ToString(), form.Images);

            if (uploadedImages.Any() == false)
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
            if (listingToEdit.Images.Any(img => img.Id.ToString() == deletedImgId && img.ListingId.ToString() == form.Id.ToLower() && (img.Listing.CreatorId.ToString() == userId || isUserAdmin)) == false)
            {
                throw new InvalidDataException(InvalidDataProvidedError);
            }

            if (listingToEdit.ThumbnailId.ToString() == deletedImgId)
            {
                listingToEdit.ThumbnailId = null;
            }

            await this.listingImageService.DeleteListingImageAsync(form.Id, deletedImgId);
        }

        var oldPrice = listingToEdit.Price;

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
        listingToEdit.PhoneNumber = form.PhoneNumber!;
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

        this.listingsRepository.Update(listingToEdit);
        await this.listingsRepository.SaveChangesAsync();

        if (listingToEdit.ThumbnailId.HasValue == false || listingToEdit.ThumbnailId!.Value != oldThumbnailId)
        {
            var firstImageId = listingToEdit.Images.First().Id.ToString();
            await this.AddThumbnailToListingByIdAsync(listingToEdit.Id.ToString(), firstImageId, userId, isUserAdmin);
        }

        if (oldPrice * ListingMinimumPriceDecreaseThreshold > listingToEdit.Price)
        {
            var usersThatHaveFavoritedTheListing = listingToEdit.Favorites.Select(f => f.User);

            foreach (var user in usersThatHaveFavoritedTheListing)
            {
                var emailHtmlContent = string.Format(PriceChangeEmailTemplate, listingToEdit.Title, listingToEdit.Thumbnail!.Url, listingToEdit.CarMake.Name, listingToEdit.CarModel.Name, listingToEdit.Year, listingToEdit.City.Name, listingToEdit.EngineModel, listingToEdit.Mileage.ToString("##,###"), oldPrice.ToString("C"), listingToEdit.Price.ToString("C"), user.FullName);

                await this.emailSender.SendEmailAsync(SendGridSenderEmail, SendGridSenderName, user.Email,
                    "Намалена цена", emailHtmlContent);
            }
        }

        return listingToEdit.Id.ToString();
    }

    public async Task<IEnumerable<ImageViewModel>> GetUploadedImagesForListingByIdAsync(string listingId, string userId, bool isUserAdmin)
    => await this.carImagesRepository.AllWithDeleted()
        .Where(ci => ci.ListingId.ToString() == listingId && (ci.Listing.CreatorId.ToString() == userId || isUserAdmin)).To<ImageViewModel>().ToArrayAsync();

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

    private async Task<ICollection<FeaturesWithCategoryViewModel>> GetSelectedFeaturesByListingIdAsync(string listingId)
    {
        var selectedFeatures = await this.listingsRepository.All()
            .Include(l => l.ListingFeatures.Where(lf => lf.Feature.IsDeleted == false && lf.Feature.Category.IsDeleted == false))
            .ThenInclude(lf => lf.Feature)
            .ThenInclude(f => f.Category)
            .AsNoTracking()
            .Where(l => l.Id.ToString() == listingId)
            .SelectMany(l => l.ListingFeatures)
            .ToArrayAsync();

        var listingFeatures = new HashSet<FeaturesWithCategoryViewModel>();

        foreach (var currentLf in selectedFeatures.DistinctBy(lf => lf.Feature.CategoryId))
        {
            var featuresOfCategory = selectedFeatures.Where(l => l.Feature.CategoryId == currentLf.Feature.CategoryId);

            listingFeatures.Add(new FeaturesWithCategoryViewModel
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

    public async Task<ListingDetailsViewModel> GetDeactivatedListingDetailsAsync(string listingId, string userId, bool isUserAdmin)
    {
        var deactivatedListing = await this.listingsRepository.AllWithDeleted()
            .Where(l => l.IsDeleted && (l.CreatorId.ToString() == userId && l.Id.ToString() == listingId || l.Id.ToString() == listingId && isUserAdmin))
            .To<ListingDetailsViewModel>()
            .FirstAsync();

        return deactivatedListing;
    }

    public async Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string listingId, string userId, bool isUserAdmin)
    {
        var chooseThumbnailViewModel =
             await this.listingsRepository.All()
                 .Where(l => l.Id.ToString() == listingId && l.CreatorId.ToString() == userId || l.Id.ToString() == listingId && isUserAdmin)
                 .To<ChooseThumbnailFormModel>()
                 .FirstAsync();

        return chooseThumbnailViewModel;
    }

    public async Task<string> CreateListingAsync(ListingFormModel form, string userId)
    {
        if (await this.ValidateForm(form) == false)
        {
            throw new InvalidDataException(InvalidDataProvidedError);
        }

        if (this.ValidateUploadedImages(form) == false)
        {
            throw new InvalidImagesException(UploadedImagesAreInvalid);
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

        var uploadedImages = await this.listingImageService.UploadMultipleListingImagesAsync(listing.Id.ToString(), form.Images);

        if (uploadedImages.Any() == false)
        {
            throw new InvalidDataException(ImagesUploadUnsuccessful);
        }

        listing.Images = uploadedImages;

        await this.listingsRepository.AddAsync(listing);

        await this.listingsRepository.SaveChangesAsync();

        var firstImageId = listing.Images.First().Id.ToString();

        await this.AddThumbnailToListingByIdAsync(listing.Id.ToString(), firstImageId, userId, false);

        return listing.Id.ToString();
    }

    public async Task AddThumbnailToListingByIdAsync(string listingId, string imageId, string userId, bool isUserAdmin)
    {
        var imageAndListingExist = await this.carImagesRepository
            .AllWithDeleted()
            .AnyAsync(ci => ci.Id.ToString() == imageId && ci.ListingId.ToString() == listingId && (ci.Listing.CreatorId.ToString() == userId || isUserAdmin));

        if (imageAndListingExist == false)
        {
            throw new InvalidOperationException(InvalidImageForThumbnailProvided);
        }

        var listing = await this.listingsRepository.AllWithDeleted().FirstAsync(l => l.Id.ToString() == listingId);

        listing.ThumbnailId = Guid.Parse(imageId);

        await this.listingsRepository.SaveChangesAsync();
    }

    public async Task DeactivateListingByIdAsync(string listingId, string userId, bool isUserAdmin)
    {
        var listingToDeactivate = await this.listingsRepository.All().FirstAsync(l => (l.CreatorId.ToString() == userId || isUserAdmin) && l.Id.ToString() == listingId);

        this.listingsRepository.Delete(listingToDeactivate);

        await this.listingsRepository.SaveChangesAsync();
    }

    public async Task ReactivateListingByIdAsync(string listingId, string userId, bool isUserAdmin)
    {
        var listingToDeactivate = await this.listingsRepository.AllWithDeleted().FirstAsync(l => (l.CreatorId.ToString() == userId || isUserAdmin) && l.Id.ToString() == listingId);

        this.listingsRepository.Undelete(listingToDeactivate);

        await this.listingsRepository.SaveChangesAsync();
    }

    public async Task HardDeleteListingByIdAsync(string listingId, string userId, bool isUserAdmin)
    {
        var listingToDelete = await this.listingsRepository.AllWithDeleted().FirstAsync(l => (l.CreatorId.ToString() == userId || isUserAdmin) && l.Id.ToString() == listingId);

        var listingFeaturesToDelete = await this.listingFeaturesRepository.AllWithDeleted().Where(lf => lf.ListingId == listingToDelete.Id).ToArrayAsync();

        foreach (var listingFeature in listingFeaturesToDelete)
        {
            this.listingFeaturesRepository.HardDelete(listingFeature);
        }
        await this.listingFeaturesRepository.SaveChangesAsync();

        var imageIds =
            await this.carImagesRepository.AllWithDeleted().Where(ci => ci.ListingId == listingToDelete.Id).Select(ci => ci.Id.ToString()).ToArrayAsync();

        await this.listingImageService.DeleteAllImagesOfListingAsync(listingId, imageIds);

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
        var carMakeIdExists = await this.carService.CarMakeIdExistsAsync(form.CarMakeId);
        var carModelIdExists = await this.carService.CarModelIdExistsByMakeIdAsync(form.CarMakeId, form.CarModelId);
        var engineTypeIdExists = await this.carService.CarEngineTypeIdExistsAsync(form.EngineTypeId);
        var cityIdExists = await this.carService.CityIdExistsAsync(form.CityId);
        var featureIdsExist = await this.carService.SelectedFeaturesIdsExist(form.SelectedFeatures);

        return engineTypeIdExists && carMakeIdExists && carModelIdExists && cityIdExists && featureIdsExist;
    }

    private ListingFormModel SanitizeForm(ListingFormModel form)
    {
        form.Title = this.htmlSanitizer.Sanitize(form.Title);
        form.VinNumber = this.htmlSanitizer.Sanitize(form.VinNumber);
        form.Description = this.htmlSanitizer.Sanitize(form.Description);
        form.EngineModel = this.htmlSanitizer.Sanitize(form.EngineModel);
        form.PhoneNumber = this.htmlSanitizer.Sanitize(form.PhoneNumber!);

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

            if (image.ContentType.StartsWith("image/") == false)
            {
                return false;
            }
        }

        return true;
    }
}