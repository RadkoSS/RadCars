namespace RadCars.Services.Data;

using AutoMapper;
using Common.Exceptions;
using Ganss.Xss;

using Microsoft.EntityFrameworkCore;

using Mapping;
using Contracts;
using Models.Auction;
using Web.ViewModels.Auction;
using Web.ViewModels.Feature;
using Web.ViewModels.CarImage;
using Web.ViewModels.Thumbnail;
using Web.ViewModels.Auction.Enums;
using RadCars.Data.Models.Entities;
using Web.ViewModels.FeatureCategory;
using RadCars.Data.Common.Contracts.Repositories;

using static Common.GeneralApplicationConstants;
using static Common.ExceptionsAndNotificationsMessages;
using static Common.EntityValidationConstants.AuctionConstants;

public class AuctionService : IAuctionService
{
    private readonly IMapper mapper;
    private readonly IHtmlSanitizer htmlSanitizer;

    private readonly ICarService carService;
    private readonly IAuctionImageService auctionImageService;

    private readonly IDeletableEntityRepository<Auction> auctionsRepository;
    private readonly IDeletableEntityRepository<AuctionCarImage> carImagesRepository;
    private readonly IDeletableEntityRepository<AuctionFeature> auctionFeaturesRepository;
    private readonly IDeletableEntityRepository<UserFavoriteAuction> userFavoriteAuctionsRepository;

    public AuctionService(IMapper mapper, IHtmlSanitizer htmlSanitizer, ICarService carService, IAuctionImageService auctionImageService, IDeletableEntityRepository<Auction> auctionsRepository, IDeletableEntityRepository<AuctionCarImage> carImagesRepository, IDeletableEntityRepository<AuctionFeature> auctionFeaturesRepository, IDeletableEntityRepository<UserFavoriteAuction> userFavoriteAuctionsRepository)
    {
        this.mapper = mapper;
        this.carService = carService;
        this.htmlSanitizer = htmlSanitizer;
        this.auctionsRepository = auctionsRepository;
        this.auctionImageService = auctionImageService;
        this.carImagesRepository = carImagesRepository;
        this.auctionFeaturesRepository = auctionFeaturesRepository;
        this.userFavoriteAuctionsRepository = userFavoriteAuctionsRepository;
    }

    public async Task<AllAuctionsFilteredAndPagedServiceModel> GetAllAuctionsAsync(AllAuctionsQueryModel queryModel)
    {
        var auctionsQuery = this.auctionsRepository.All().AsQueryable().Where(a => a.ThumbnailId != null);

        if (queryModel.CarMakeId.HasValue)
        {
            auctionsQuery = auctionsQuery.Where(l => l.CarMakeId == queryModel.CarMakeId.Value);
        }

        if (queryModel.CarModelId.HasValue)
        {
            auctionsQuery = auctionsQuery.Where(l => l.CarModelId == queryModel.CarModelId.Value);
        }

        if (queryModel.CityId.HasValue)
        {
            auctionsQuery = auctionsQuery.Where(l => l.CityId == queryModel.CityId.Value);
        }

        if (queryModel.MaximumMileage > 0)
        {
            auctionsQuery = auctionsQuery.Where(l => l.Mileage <= queryModel.MaximumMileage);
        }

        if (queryModel.EngineTypeId.HasValue)
        {
            auctionsQuery = auctionsQuery.Where(l => l.EngineTypeId == queryModel.EngineTypeId.Value);
        }

        if (string.IsNullOrWhiteSpace(queryModel.SearchString) == false)
        {
            var wildCard = $"%{queryModel.SearchString.ToLower()}%";

            auctionsQuery = auctionsQuery.Where(a => EF.Functions.Like(a.Title, wildCard) || EF.Functions.Like(a.Description, wildCard) || EF.Functions.Like(a.EngineModel, wildCard));
        }

        //ToDo: implement logic to check if an auction is still not finished. Only Newest and Oldest are working now!
        auctionsQuery = queryModel.AuctionSorting switch
        {
            AuctionSorting.Newest => auctionsQuery.OrderByDescending(a => a.CreatedOn),
            AuctionSorting.Oldest => auctionsQuery.OrderBy(a => a.CreatedOn),
            AuctionSorting.MostTimeLeft => auctionsQuery.OrderByDescending(a => a),
            AuctionSorting.LeastTimeLeft => auctionsQuery.OrderBy(a => a),
            AuctionSorting.NotStarted => auctionsQuery.OrderByDescending(a => a),
            AuctionSorting.Started => auctionsQuery.OrderByDescending(a => a),
            AuctionSorting.Finished => auctionsQuery.OrderByDescending(a => a),
            _ => auctionsQuery.OrderByDescending(a => a.CreatedOn)
        };

        var auctions = await auctionsQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.AuctionsPerPage)
            .Take(queryModel.AuctionsPerPage)
            .To<AllAuctionsViewModel>()
            .ToArrayAsync();

        var count = auctionsQuery.Count();

        var auctionsQueryModel = new AllAuctionsFilteredAndPagedServiceModel
        {
            TotalAuctionsCount = count,
            Auctions = auctions
        };

        return auctionsQueryModel;
    }

    public async Task<IEnumerable<AllAuctionsViewModel>> GetAllAuctionsByUserIdAsync(string userId)
    {
        var auctions = await this.auctionsRepository
            .AllAsNoTracking()
            .Where(a => a.CreatorId.ToString() == userId)
            .OrderByDescending(a => a.CreatedOn)
            .To<AllAuctionsViewModel>()
            .ToArrayAsync();

        return auctions;
    }

    public async Task<IEnumerable<AllAuctionsViewModel>> GetFavoriteAuctionsByUserIdAsync(string userId)
    {
        var favoriteAuctions = await this.userFavoriteAuctionsRepository.All()
            .Where(ufa => ufa.UserId.ToString() == userId)
            .To<AllAuctionsViewModel>().ToListAsync();

        return favoriteAuctions;
    }

    public async Task<bool> IsAuctionInUserFavoritesByIdAsync(string auctionId, string userId)
    {
        var result = await this.userFavoriteAuctionsRepository.All().AnyAsync(ufa => ufa.AuctionId.ToString() == auctionId && ufa.UserId.ToString() == userId);

        return result;
    }

    public async Task FavoriteAuctionByIdAsync(string auctionId, string userId)
    {
        var auctionToFavorite = await this.auctionsRepository.All().FirstAsync(a => a.Id.ToString() == auctionId && a.CreatorId.ToString() != userId);

        if (await this.IsAuctionInUserFavoritesByIdAsync(auctionId, userId))
        {
            throw new InvalidOperationException(AuctionIsAlreadyInCurrentUserFavorites);
        }

        var userFavoriteAuction = new UserFavoriteAuction
        {
            UserId = Guid.Parse(userId),
            AuctionId = auctionToFavorite.Id
        };

        await this.userFavoriteAuctionsRepository.AddAsync(userFavoriteAuction);

        await this.userFavoriteAuctionsRepository.SaveChangesAsync();
    }

    public async Task<int> GetFavoritesCountForAuctionByIdAsync(string auctionId)
    {
        var count = await this.userFavoriteAuctionsRepository.AllAsNoTracking()
            .CountAsync(ufa => ufa.AuctionId.ToString() == auctionId);

        return count;
    }

    public async Task UnFavoriteAuctionByIdAsync(string auctionId, string userId)
    {
        var auctionToUnFavorite = await this.userFavoriteAuctionsRepository.All()
            .FirstAsync(a => a.AuctionId.ToString() == auctionId && a.UserId.ToString() == userId);

        this.userFavoriteAuctionsRepository.HardDelete(auctionToUnFavorite);

        await this.userFavoriteAuctionsRepository.SaveChangesAsync();
    }

    public Task<IEnumerable<AllAuctionsViewModel>> GetAllDeactivatedAuctionsByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AuctionIndexViewModel>> GetMostRecentAuctionsAsync()
    {
        var mostRecentAuctions = await this.auctionsRepository.AllAsNoTracking()
            .OrderByDescending(a => a.CreatedOn)
            .Take(3)
            .To<AuctionIndexViewModel>()
            .ToArrayAsync();

        return mostRecentAuctions;
    }

    public async Task<AuctionDetailsViewModel> GetAuctionDetailsAsync(string auctionId)
    {
        var detailsViewModel = await this.auctionsRepository.All()
            .Where(a => a.Id.ToString() == auctionId)
            .To<AuctionDetailsViewModel>()
            .FirstAsync();

        detailsViewModel.AuctionFeatures = await this.GetSelectedFeaturesByAuctionIdAsync(auctionId);

        return detailsViewModel;
    }

    public async Task<ICollection<FeatureCategoriesViewModel>> GetSelectedFeaturesByAuctionIdAsync(string auctionId)
    {
        var selectedFeatures = await this.auctionsRepository.All()
            .Include(l => l.AuctionFeatures)
            .ThenInclude(lf => lf.Feature)
            .ThenInclude(f => f.Category)
            .AsNoTracking()
            .Where(l => l.Id.ToString() == auctionId)
            .SelectMany(l => l.AuctionFeatures)
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

    public async Task<AuctionFormModel> GetAuctionCreateAsync()
    {
        var formModel = new AuctionFormModel
        {
            CarMakes = await this.carService.GetCarMakesAsync(),
            FeatureCategories = await this.carService.GetFeatureCategoriesAsync(),
            Cities = await this.carService.GetBulgarianCitiesAsync(),
            EngineTypes = await this.carService.GetEngineTypesAsync()
        };

        return formModel;
    }

    public async Task<string> CreateAuctionAsync(AuctionFormModel form, string userId)
    {
        if (await this.ValidateForm(form) == false)
        {
            throw new InvalidDataException();
        }

        if (this.ValidateUploadedImages(form) == false)
        {
            throw new InvalidImagesException("");
        }

        form = this.SanitizeForm(form);

        var auction = this.mapper.Map<Auction>(form);

        auction.Id = Guid.NewGuid();
        auction.CreatorId = Guid.Parse(userId);

        foreach (var id in form.SelectedFeatures)
        {
            auction.AuctionFeatures.Add(new AuctionFeature
            {
                FeatureId = id,
                Auction = auction
            });
        }

        var uploadedImages = await this.auctionImageService.UploadMultipleAuctionImagesAsync(auction.Id.ToString(), form.Images);

        if (uploadedImages.Any() == false)
        {
            throw new InvalidDataException();
        }

        auction.Images = uploadedImages;

        await this.auctionsRepository.AddAsync(auction);

        await this.auctionsRepository.SaveChangesAsync();

        var firstImageId = auction.Images.First().Id.ToString();

        await this.AddThumbnailToAuctionByIdAsync(auction.Id.ToString(), firstImageId, userId, false);

        return auction.Id.ToString();
    }

    public async Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string auctionId, string userId, bool isUserAdmin)
    {
        var chooseThumbnailViewModel = await this.auctionsRepository.All()
            .Where(a => a.Id.ToString() == auctionId && (a.CreatorId.ToString() == userId || isUserAdmin))
            .To<ChooseThumbnailFormModel>()
            .FirstAsync();

        return chooseThumbnailViewModel;
    }

    public async Task AddThumbnailToAuctionByIdAsync(string auctionId, string imageId, string userId, bool isUserAdmin)
    {
        var imageAndAuctionExist = await this.carImagesRepository.AllWithDeleted()
            .AnyAsync(ci => ci.Id.ToString() == imageId && ci.AuctionId.ToString() == auctionId && (ci.Auction.CreatorId.ToString() == userId || isUserAdmin));

        if (imageAndAuctionExist == false)
        {
            throw new InvalidOperationException();
        }

        var auction = await this.auctionsRepository.AllWithDeleted().FirstAsync(a => a.Id.ToString() == auctionId);

        auction.ThumbnailId = Guid.Parse(imageId);

        await this.auctionsRepository.SaveChangesAsync();
    }

    public async Task<AuctionEditFormModel> GetAuctionEditAsync(string auctionId, string userId, bool isUserAdmin)
    {
        var auctionToEdit = await this.auctionsRepository.AllWithDeleted()
            .Where(a => a.Id.ToString() == auctionId && (a.CreatorId.ToString() == userId ||
                        isUserAdmin))
            .To<AuctionEditFormModel>()
            .FirstOrDefaultAsync();

        if (auctionToEdit == null)
        {
            throw new InvalidOperationException(AuctionDoesNotExistError);
        }

        auctionToEdit.UploadedImages = await this.GetUploadedImagesForAuctionByIdAsync(auctionId, userId, isUserAdmin);

        auctionToEdit.Cities = await this.carService.GetBulgarianCitiesAsync();
        auctionToEdit.CarMakes = await this.carService.GetCarMakesAsync();
        auctionToEdit.CarModels = await this.carService.GetModelsByMakeIdAsync(auctionToEdit.CarMakeId);
        auctionToEdit.EngineTypes = await this.carService.GetEngineTypesAsync();
        auctionToEdit.FeatureCategories = await this.carService.GetFeatureCategoriesAsync();

        var selectedFeaturesWithCategories = await GetSelectedFeaturesByAuctionIdAsync(auctionId);

        foreach (var featuresWithCategory in auctionToEdit.FeatureCategories)
        {
            foreach (var feature in featuresWithCategory.Features)
            {
                feature.IsSelected = selectedFeaturesWithCategories
                    .SelectMany(sfc => sfc.Features)
                    .Any(sf => sf.Id == feature.Id);
            }
        }

        return auctionToEdit;
    }

    public async Task<string> EditAuctionAsync(AuctionEditFormModel form, string userId, bool isUserAdmin)
    {
        var auctionToEdit = await this.auctionsRepository.AllWithDeleted()
            .Where(a => a.Id.ToString() == form.Id && (a.CreatorId.ToString() == userId || isUserAdmin))
            .FirstAsync();

        var oldThumbnail = auctionToEdit.ThumbnailId!.Value;

        if (form.Images.Any() == false && form.DeletedImages.Count() >= auctionToEdit.Images.Count)
        {
            throw new InvalidDataException(InvalidDataProvidedError);
        }

        if (auctionToEdit.Images.Count - form.DeletedImages.Count() + form.Images.Count() <= 0)
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

        form = (this.SanitizeForm(form) as AuctionEditFormModel)!;

        if (form.Images.Any())
        {
            var uploadedImages = await this.auctionImageService.UploadMultipleAuctionImagesAsync(auctionToEdit.Id.ToString(), form.Images);

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
            if (auctionToEdit.Images.Any(img => img.Id.ToString() == deletedImgId && img.AuctionId.ToString() == form.Id.ToLower() && (img.Auction.CreatorId.ToString() == userId || isUserAdmin)) == false)
            {
                throw new InvalidDataException(InvalidDataProvidedError);
            }

            if (auctionToEdit.ThumbnailId.ToString() == deletedImgId)
            {
                auctionToEdit.ThumbnailId = null;
            }

            await this.auctionImageService.DeleteAuctionImageAsync(form.Id, deletedImgId);
        }

        auctionToEdit.Year = form.Year;
        auctionToEdit.Title = form.Title;
        auctionToEdit.CityId = form.CityId;
        auctionToEdit.Mileage = form.Mileage;
        auctionToEdit.CarMakeId = form.CarMakeId;
        auctionToEdit.VinNumber = form.VinNumber;
        auctionToEdit.BlitzPrice = form.BlitzPrice;
        auctionToEdit.CarModelId = form.CarModelId;
        auctionToEdit.MinimumBid = form.MinimumBid;
        auctionToEdit.EngineModel = form.EngineModel;
        auctionToEdit.Description = form.Description;
        auctionToEdit.PhoneNumber = form.PhoneNumber!;
        auctionToEdit.EngineTypeId = form.EngineTypeId;
        auctionToEdit.CurrentPrice = form.StartingPrice;
        auctionToEdit.StartingPrice = form.StartingPrice;
        auctionToEdit.EndTime = form.EndTime.ToUniversalTime();
        auctionToEdit.StartTime = form.StartTime.ToUniversalTime();

        var newSelectedFeatures = new HashSet<AuctionFeature>();

        foreach (var id in form.SelectedFeatures)
        {
            newSelectedFeatures.Add(new AuctionFeature
            {
                FeatureId = id,
                AuctionId = auctionToEdit.Id
            });
        }

        foreach (var listingFeature in auctionToEdit.AuctionFeatures)
        {
            this.auctionFeaturesRepository.HardDelete(listingFeature);
        }

        await this.auctionFeaturesRepository.SaveChangesAsync();

        auctionToEdit.AuctionFeatures = newSelectedFeatures;

        this.auctionsRepository.Update(auctionToEdit);
        await this.auctionsRepository.SaveChangesAsync();

        if (auctionToEdit.ThumbnailId.HasValue == false || auctionToEdit.ThumbnailId!.Value != oldThumbnail)
        {
            var firstImageId = auctionToEdit.Images.First().Id.ToString();
            await this.AddThumbnailToAuctionByIdAsync(auctionToEdit.Id.ToString(), firstImageId, userId, isUserAdmin);
        }

        return auctionToEdit.Id.ToString();
    }

    public async Task<IEnumerable<ImageViewModel>> GetUploadedImagesForAuctionByIdAsync(string auctionId, string userId,
        bool isUserAdmin)
        => await this.carImagesRepository.AllWithDeleted()
            .Where(ci =>
                ci.AuctionId.ToString() == auctionId && (ci.Auction.CreatorId.ToString() == userId || isUserAdmin))
                .To<ImageViewModel>().ToArrayAsync();

    public async Task<int> GetUploadedImagesCountForAuctionByIdAsync(string auctionId)
        => await this.carImagesRepository.AllWithDeleted()
            .Where(ci => ci.AuctionId.ToString() == auctionId).CountAsync();

    public async Task<AuctionDetailsViewModel> GetDeactivatedAuctionDetailsAsync(string auctionId, string userId,
        bool isUserAdmin)
    {
        throw new NotImplementedException();
    }

    public Task DeactivateAuctionByIdAsync(string auctionId, string userId, bool isUserAdmin)
    {
        throw new NotImplementedException();
    }

    public Task ReactivateAuctionByIdAsync(string auctionId, string userId, bool isUserAdmin)
    {
        throw new NotImplementedException();
    }

    public Task HardDeleteAuctionByIdAsync(string auctionId, string userId, bool isUserAdmin)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetBidsCountForAuctionByIdAsync(string auctionId)
    {
        var bidsForAuction =
            await this.auctionsRepository.AllAsNoTracking().Where(a => a.Id.ToString() == auctionId).SelectMany(a => a.Bids).ToArrayAsync();

        return bidsForAuction.Length;
    }
    private async Task<bool> ValidateForm(AuctionFormModel form)
    {
        var cityIdExists = await this.carService.CityIdExistsAsync(form.CityId);
        var carMakeIdExists = await this.carService.CarMakeIdExistsAsync(form.CarMakeId);
        var carModelIdExists = await this.carService.CarModelIdExistsByMakeIdAsync(form.CarMakeId, form.CarModelId);
        var featureIdsExist = await this.carService.SelectedFeaturesIdsExist(form.SelectedFeatures);
        var engineTypeIdExists = await this.carService.CarEngineTypeIdExistsAsync(form.EngineTypeId);

        var currentDateAndTime = DateTime.UtcNow - TimeSpan.FromMinutes(1); //ToDo: Fix this! We give a little gap to compensate server response times and other things slowing down the requests especially while debugging!

        var startTimeToUtc = form.StartTime.ToUniversalTime();
        var endTimeToUtc = form.EndTime.ToUniversalTime();

        var datesAreValid = true;

        if (startTimeToUtc < currentDateAndTime.AddMinutes(MinimumMinutesToAuctionStart))
        {
            datesAreValid = false;
        }
        else if (startTimeToUtc.AddHours(MinimumHoursToAuctionEnd) > endTimeToUtc)
        {
            datesAreValid = false;
        }
        //ToDo: Add front end validation ensuring EndDate is at most 14 days after the StartDate!
        else if (endTimeToUtc > startTimeToUtc.AddDays(MaximumDaysOfAuctioning))
        {
            datesAreValid = false;
        }
        return carMakeIdExists && carModelIdExists && engineTypeIdExists && cityIdExists && featureIdsExist && datesAreValid;
    }

    private AuctionFormModel SanitizeForm(AuctionFormModel form)
    {
        form.Title = this.htmlSanitizer.Sanitize(form.Title);
        form.VinNumber = this.htmlSanitizer.Sanitize(form.VinNumber);
        form.Description = this.htmlSanitizer.Sanitize(form.Description);
        form.EngineModel = this.htmlSanitizer.Sanitize(form.EngineModel);
        form.PhoneNumber = this.htmlSanitizer.Sanitize(form.PhoneNumber!);

        return form;
    }

    private bool ValidateUploadedImages(AuctionFormModel form)
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