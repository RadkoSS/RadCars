namespace RadCars.Services.Data;

using Microsoft.EntityFrameworkCore;

using Contracts;
using RadCars.Data;
using Web.ViewModels.Listing;
using Web.ViewModels.CarPicture;
using RadCars.Data.Models.Enums;
using RadCars.Data.Models.Entities;

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
            Thumbnail = new PictureViewModel
            {
                Id = l.ThumbnailId.ToString() == null ? l.Pictures.First().Id.ToString() : l.ThumbnailId.ToString()!,
                Url = l.Thumbnail == null ? l.Pictures.First().Url : l.Thumbnail.Url
            }
        }).ToArrayAsync();

        return listings;
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
                Pictures = l.Pictures.Select(p => new PictureViewModel
                {
                    Id = p.Id.ToString(),
                    Url = p.Url
                }).ToArray()
            }).FirstAsync();

        return listingToDisplay;
    }

    public async Task CreateListing(CreateListingInputModel input, string userId)
    {
        //ToDo: Add all the required properties to the input model and implement AutoMapper to the application.

        var listing = new Listing
        {
            Title = input.Title,
            Description = input.Description,
            Year = input.Year,
            CreatorId = Guid.Parse(userId)
        };

        await this.dbContext.Listings.AddAsync(listing);

        await this.dbContext.SaveChangesAsync();

        await this.imageService.UploadMultipleImagesAsync(listing.Id.ToString(), input.Pictures);
    }
}