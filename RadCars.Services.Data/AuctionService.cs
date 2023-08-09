namespace RadCars.Services.Data;

using Ganss.Xss;
using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Mapping;
using Contracts;
using Models.Auction;
using Common.Exceptions;
using Messaging.Contracts;
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
using static Messaging.Templates.EmailTemplates.AuctionTemplates;

public class AuctionService : IAuctionService
{
    private readonly IMapper mapper;
    private readonly IEmailSender emailSender;
    private readonly IHtmlSanitizer htmlSanitizer;

    private readonly ICarService carService;
    private readonly IAuctionImageService auctionImageService;

    private readonly IDeletableEntityRepository<Auction> auctionsRepository;
    private readonly IDeletableEntityRepository<UserAuctionBid> bidsRepository;
    private readonly IDeletableEntityRepository<AuctionCarImage> carImagesRepository;
    private readonly IDeletableEntityRepository<AuctionFeature> auctionFeaturesRepository;
    private readonly IDeletableEntityRepository<UserFavoriteAuction> userFavoriteAuctionsRepository;

    public AuctionService(IMapper mapper,
        IHtmlSanitizer htmlSanitizer,
        ICarService carService,
        IAuctionImageService auctionImageService,
        IDeletableEntityRepository<Auction> auctionsRepository,
        IDeletableEntityRepository<AuctionCarImage> carImagesRepository,
        IDeletableEntityRepository<AuctionFeature> auctionFeaturesRepository,
        IDeletableEntityRepository<UserFavoriteAuction> userFavoriteAuctionsRepository,
        IDeletableEntityRepository<UserAuctionBid> bidsRepository, IEmailSender emailSender)
    {
        this.mapper = mapper;
        this.carService = carService;
        this.emailSender = emailSender;
        this.htmlSanitizer = htmlSanitizer;
        this.bidsRepository = bidsRepository;
        this.auctionsRepository = auctionsRepository;
        this.auctionImageService = auctionImageService;
        this.carImagesRepository = carImagesRepository;
        this.auctionFeaturesRepository = auctionFeaturesRepository;
        this.userFavoriteAuctionsRepository = userFavoriteAuctionsRepository;
    }

    public async Task<AllAuctionsFilteredAndPagedServiceModel> GetAllAuctionsAsync(AllAuctionsQueryModel queryModel, bool withDeleted)
    {
        IQueryable<Auction> auctionsQuery;

        if (withDeleted == false)
        {
            auctionsQuery = this.auctionsRepository.All().AsQueryable().Where(a => a.ThumbnailId != null);
        }
        else
        {
            auctionsQuery = this.auctionsRepository.AllWithDeleted().AsQueryable().Where(a => a.IsDeleted == true);
        }

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
        
        auctionsQuery = queryModel.AuctionSorting switch
        {
            AuctionSorting.Newest => auctionsQuery.OrderByDescending(a => a.CreatedOn),
            AuctionSorting.Oldest => auctionsQuery.OrderBy(a => a.CreatedOn),
            AuctionSorting.NotStarted => auctionsQuery.Where(a => a.IsOver.HasValue == false).OrderByDescending(a => a.StartTime),
            AuctionSorting.Started => auctionsQuery.Where(a => a.IsOver.HasValue && a.IsOver == false).OrderByDescending(a => a.EndTime),
            AuctionSorting.Finished => auctionsQuery.Where(a => a.IsOver.HasValue && a.IsOver == true).OrderByDescending(a => a.CreatedOn),
            _ => auctionsQuery.OrderByDescending(a => a.CreatedOn)
        };

        if (withDeleted)
        {
            auctionsQuery = auctionsQuery.OrderByDescending(l => l.DeletedOn);
        }

        var auctions = await auctionsQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.AuctionsPerPage)
            .Take(queryModel.AuctionsPerPage)
            .To<AllAuctionsViewModel>()
            .ToArrayAsync();

        if (withDeleted == false)
        {
            auctions = auctions.OrderBy(a => a.IsOver == false ? 0 : a.IsOver == null ? 1 : 2)
                .ThenBy(a => a.IsOver == false ? (a.EndTime - DateTime.UtcNow).TotalSeconds : 0)
                .ThenBy(a => a.IsOver == null ? (a.StartTime - DateTime.UtcNow).TotalSeconds : 0)
                .ToArray();
        }

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
        var auctions = await this.auctionsRepository.All()
            .Where(a => a.CreatorId.ToString() == userId && a.IsOver.HasValue && a.IsOver.Value == false)
            .OrderByDescending(a => a.CreatedOn)
            .To<AllAuctionsViewModel>()
            .ToArrayAsync();

        auctions = auctions.OrderBy(a => a.IsOver == false ? 0 : a.IsOver == null ? 1 : 2)
            .ThenBy(a => a.IsOver == false ? (a.EndTime - DateTime.UtcNow).TotalMinutes : 0)
            .ThenBy(a => a.IsOver == null ? (a.StartTime - DateTime.UtcNow).TotalMinutes : 0)
            .ToArray();

        return auctions;
    }

    public async Task<IEnumerable<AllAuctionsViewModel>> GetAllExpiredAuctionsByUserIdAsync(string userId)
    {
        var auctions = await this.auctionsRepository.All()
            .Where(a => a.IsOver.HasValue && a.IsOver.Value == true && a.CreatorId.ToString() == userId)
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

    public async Task<IEnumerable<AllAuctionsViewModel>> GetAllDeactivatedAuctionsByUserIdAsync(string userId)
    {
        var deactivatedAuctions = await this.auctionsRepository.AllWithDeleted()
            .Where(a => a.IsDeleted == true && a.CreatorId.ToString() == userId)
            .OrderByDescending(a => a.DeletedOn)
            .To<AllAuctionsViewModel>().ToArrayAsync();

        deactivatedAuctions = deactivatedAuctions.OrderByDescending(a => a.DeletedOn).ToArray();

        return deactivatedAuctions;
    }

    public async Task<IEnumerable<AuctionIndexViewModel>> GetMostRecentAuctionsAsync()
    {
        var mostRecentAuctions = await this.auctionsRepository.AllAsNoTracking()
            .OrderByDescending(a => a.CreatedOn)
            .Take(3)
            .To<AuctionIndexViewModel>()
            .ToArrayAsync();

        mostRecentAuctions = mostRecentAuctions.OrderBy(a => a.IsOver == false ? 0 : a.IsOver == null ? 1 : 2)
            .ThenBy(a => a.IsOver == false ? (a.EndTime - DateTime.UtcNow).TotalSeconds : 0)
            .ThenBy(a => a.IsOver == null ? (a.StartTime - DateTime.UtcNow).TotalSeconds : 0)
            .ToArray();

        return mostRecentAuctions;
    }

    public async Task<AuctionDetailsViewModel> GetAuctionDetailsAsync(string auctionId, string? userId, bool isUserAdmin)
    {
        AuctionDetailsViewModel detailsViewModel;

        var userIsCreator = await this.IsUserCreatorOfAuctionByIdAsync(auctionId, userId);

        if (isUserAdmin || userIsCreator)
        {
            detailsViewModel = await this.GetAuctionActivatedAndDeactivatedDetailsAsync(auctionId);
        }
        else
        {
            detailsViewModel = await this.auctionsRepository.All()
                .Where(a => a.Id.ToString() == auctionId)
                .To<AuctionDetailsViewModel>()
                .FirstAsync();
        }

        detailsViewModel.AuctionFeatures = await this.GetSelectedFeaturesByAuctionIdAsync(auctionId);

        return detailsViewModel;
    }

    public async Task<ICollection<FeaturesWithCategoryViewModel>> GetSelectedFeaturesByAuctionIdAsync(string auctionId)
    {
        var selectedFeatures = await this.auctionsRepository.AllWithDeleted()
            .Include(a => a.AuctionFeatures)
            .ThenInclude(af => af.Feature)
            .ThenInclude(f => f.Category)
            .AsNoTracking()
            .Where(a => a.Id.ToString() == auctionId)
            .SelectMany(a => a.AuctionFeatures)
            .ToArrayAsync();

        var auctionFeatures = new HashSet<FeaturesWithCategoryViewModel>();

        foreach (var currentAf in selectedFeatures.Where(af => af.Feature.IsDeleted == false && af.Feature.Category.IsDeleted == false).DistinctBy(af => af.Feature.CategoryId))
        {
            var featuresOfCategory = selectedFeatures.Where(a => a.Feature.CategoryId == currentAf.Feature.CategoryId);

            auctionFeatures.Add(new FeaturesWithCategoryViewModel
            {
                Id = currentAf.Feature.CategoryId,
                Name = currentAf.Feature.Category.Name,
                Features = featuresOfCategory.Select(f => new FeatureViewModel
                {
                    Id = f.FeatureId,
                    Name = f.Feature.Name,
                    IsSelected = true
                }).ToArray()
            });
        }

        return auctionFeatures;
    }

    public async Task<AuctionFormModel> GetAuctionCreateAsync()
    {
        var formModel = new AuctionFormModel
        {
            CarMakes = await this.carService.GetCarMakesAsync(),
            FeatureCategories = await this.carService.GetFeaturesWithCategoriesAsync(),
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

    public async Task<AuctionBidServiceModel> CreateBidAsync(string auctionId, string userId, decimal amount)
    {
        var auction = await this.auctionsRepository.All()
            .Where(a => a.Id.ToString() == auctionId && a.CreatorId.ToString() != userId)
            .FirstAsync();

        if (auction.Bids.Any() == false)
        {
            if (amount < auction.StartingPrice)
            {
                throw new InvalidOperationException();
            }
        }
        else
        {
            var highestBid = auction.Bids.OrderByDescending(b => b.CreatedOn).First().Amount;

            if (amount < highestBid + auction.MinimumBid)
            {
                throw new InvalidOperationException();
            }
        }

        if (auction.IsOver.HasValue == false || auction.IsOver is true)
        {
            throw new InvalidOperationException();
        }

        var bid = new UserAuctionBid
        {
            AuctionId = auction.Id,
            Amount = amount,
            BidderId = Guid.Parse(userId)
        };

        await this.bidsRepository.AddAsync(bid);
        await this.bidsRepository.SaveChangesAsync();

        auction.CurrentPrice = amount;
        auction.Bids.Add(bid);

        await this.auctionsRepository.SaveChangesAsync();

        var serviceModel = new AuctionBidServiceModel
        {
            CreatedOn = bid.CreatedOn.ToLocalTime().ToString("f"),
            UserFullNameAndUserName = $"{bid.Bidder.FullName} ({bid.Bidder.UserName})",
            EndAuctionJobId = auction.EndAuctionJobId,
            OverBlitzPrice = false
        };

        if (auction.BlitzPrice.HasValue && bid.Amount >= auction.BlitzPrice.Value)
        {
            serviceModel.OverBlitzPrice = true;

            auction.IsOver = true;
            await this.auctionsRepository.SaveChangesAsync();

            var winnerInfo = string.Format(WinnerAnnounce, serviceModel.UserFullNameAndUserName, amount.ToString("C"),
                serviceModel.CreatedOn, bid.Bidder.Email, bid.Bidder.PhoneNumber);

            var auctionEndEmailToCreatorHtmlContent = string.Format(AuctionEndedEmailToCreator,
                auction.Creator.FullName, auction.CarMake.Name, auction.CarModel.Name, auction.Thumbnail!.Url,
                auction.Title, winnerInfo);

            await this.emailSender.SendEmailAsync(SendGridSenderEmail, SendGridSenderName, auction.Creator.Email,
                $"Край на търга за {auction.CarMake.Name} {auction.CarModel.Name}",
                auctionEndEmailToCreatorHtmlContent);
        }

        return serviceModel;
    }

    public async Task<ChooseThumbnailFormModel> GetChooseThumbnailAsync(string auctionId, string userId, bool isUserAdmin)
    {
        var chooseThumbnailViewModel = await this.auctionsRepository.AllWithDeleted()
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

    private async Task<bool?> GetAuctionStateAsync(string auctionId)
    {
        var state = await this.auctionsRepository.AllWithDeleted().Where(a => a.Id.ToString() == auctionId)
            .Select(a => a.IsOver).FirstAsync();

        return state;
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
            throw new InvalidDataException(AuctionDoesNotExistError);
        }

        var auctionStartedOrIsOver = await this.GetAuctionStateAsync(auctionId);

        if (isUserAdmin == false)
        {
            //false means started, true means ended
            if (auctionStartedOrIsOver is false || (auctionStartedOrIsOver is true && await this.GetBidsCountForAuctionByIdAsync(auctionId) > 0))
            {
                throw new InvalidOperationException(AuctionHasAlreadyStarted);
            }
        }
        //state is null -> not started yet

        auctionToEdit.UploadedImages = await this.GetUploadedImagesForAuctionByIdAsync(auctionId, userId, isUserAdmin);

        auctionToEdit.Cities = await this.carService.GetBulgarianCitiesAsync();
        auctionToEdit.CarMakes = await this.carService.GetCarMakesAsync();
        auctionToEdit.CarModels = await this.carService.GetModelsByMakeIdAsync(auctionToEdit.CarMakeId);
        auctionToEdit.EngineTypes = await this.carService.GetEngineTypesAsync();
        auctionToEdit.FeatureCategories = await this.carService.GetFeaturesWithCategoriesAsync();

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
            .Include(a => a.Bids)
            .Where(a => a.Id.ToString() == form.Id && (a.CreatorId.ToString() == userId || isUserAdmin))
            .FirstAsync();

        if (auctionToEdit.Bids.Any())
        {
            if (isUserAdmin == false)
            {
                throw new InvalidOperationException(CannotEditActiveAuctionWithBids);
            }

            foreach (var bid in auctionToEdit.Bids)
            {
                this.bidsRepository.HardDelete(bid);
            }

            await this.bidsRepository.SaveChangesAsync();

            this.auctionsRepository.Delete(auctionToEdit);

            await this.auctionsRepository.SaveChangesAsync();
        }

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

        foreach (var auctionFeature in auctionToEdit.AuctionFeatures)
        {
            this.auctionFeaturesRepository.HardDelete(auctionFeature);
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

        if (auctionToEdit.IsDeleted)
        {
            this.auctionsRepository.Undelete(auctionToEdit);

            await this.auctionsRepository.SaveChangesAsync();
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

    private async Task<AuctionDetailsViewModel> GetAuctionActivatedAndDeactivatedDetailsAsync(string auctionId)
    {
        var auctionDetails = await this.auctionsRepository.AllWithDeleted()
            .Where(a => a.Id.ToString() == auctionId)
            .To<AuctionDetailsViewModel>()
            .FirstAsync();

        return auctionDetails;
    }

    private async Task<bool> IsUserCreatorOfAuctionByIdAsync(string auctionId, string? userId)
    {
        var result = await this.auctionsRepository.AllWithDeleted()
            .Where(a => a.Id.ToString() == auctionId && a.CreatorId.ToString() == userId)
            .AnyAsync();

        return result;
    }

    public async Task DeactivateAuctionByIdAsync(string auctionId, string userId, bool isUserAdmin)
    {
        var auctionToDeactivate = await this.auctionsRepository.All()
            .FirstAsync(l => (l.CreatorId.ToString() == userId || isUserAdmin) && l.Id.ToString() == auctionId);

        if (auctionToDeactivate.IsOver.HasValue)
        {
            if (isUserAdmin == false)
            {
                throw new InvalidOperationException(CannotDeactivateAuction);
            }
        }

        if (auctionToDeactivate.Bids.Any())
        {
            if (isUserAdmin == false)
            {
                throw new InvalidOperationException(CannotEditActiveAuctionWithBids);
            }

            foreach (var bid in auctionToDeactivate.Bids)
            {
                this.bidsRepository.HardDelete(bid);
            }

            await this.bidsRepository.SaveChangesAsync();
        }
        
        this.auctionsRepository.Delete(auctionToDeactivate);

        await this.auctionsRepository.SaveChangesAsync();
    }

    public async Task HardDeleteAuctionByIdAsync(string auctionId)
    {
        var auctionToDelete = await this.auctionsRepository.AllWithDeleted()
            .Include(a => a.Bids)
            .FirstAsync(a => a.Id.ToString() == auctionId);

        foreach (var bid in auctionToDelete.Bids)
        {
            this.bidsRepository.HardDelete(bid);
        }

        await this.bidsRepository.SaveChangesAsync();

        var auctionFeaturesToDelete = await this.auctionFeaturesRepository.AllWithDeleted().Where(af => af.AuctionId == auctionToDelete.Id).ToArrayAsync();

        foreach (var auctionFeature in auctionFeaturesToDelete)
        {
            this.auctionFeaturesRepository.HardDelete(auctionFeature);
        }
        await this.auctionFeaturesRepository.SaveChangesAsync();

        var imageIds =
            await this.carImagesRepository.AllWithDeleted().Where(ci => ci.AuctionId == auctionToDelete.Id).Select(ci => ci.Id.ToString()).ToArrayAsync();

        await this.auctionImageService.DeleteAllImagesOfAuctionAsync(auctionId, imageIds);

        var userFavorites = await this.userFavoriteAuctionsRepository.AllWithDeleted()
            .Where(ufa => ufa.AuctionId == auctionToDelete.Id).ToArrayAsync();
        foreach (var userFavorite in userFavorites)
        {
            this.userFavoriteAuctionsRepository.HardDelete(userFavorite);
        }
        await this.userFavoriteAuctionsRepository.SaveChangesAsync();

        this.auctionsRepository.HardDelete(auctionToDelete);

        await this.auctionsRepository.SaveChangesAsync();
    }

    public async Task<int> GetBidsCountForAuctionByIdAsync(string auctionId)
    {
        var bidsForAuction =
            await this.auctionsRepository.AllAsNoTrackingWithDeleted().Where(a => a.Id.ToString() == auctionId).SelectMany(a => a.Bids).ToArrayAsync();

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

        if (startTimeToUtc < currentDateAndTime.AddMinutes(MinimumMinutesToAuctionStart))
        {
            return false;
        }
        if (startTimeToUtc.AddHours(MinimumHoursToAuctionEnd) > endTimeToUtc)
        {
            return false;
        }
        if (endTimeToUtc > startTimeToUtc.AddDays(MaximumDaysOfAuctioning))
        {
            return false;
        }
        return carMakeIdExists && carModelIdExists && engineTypeIdExists && cityIdExists && featureIdsExist;
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