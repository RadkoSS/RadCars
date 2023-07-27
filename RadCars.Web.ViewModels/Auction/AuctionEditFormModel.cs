// ReSharper disable VirtualMemberCallInConstructor
namespace RadCars.Web.ViewModels.Auction;

using System.ComponentModel.DataAnnotations;

using AutoMapper;
using Microsoft.AspNetCore.Http;

using CarImage;
using Data.Models.Entities;

public class AuctionEditFormModel : AuctionFormModel
{
    public AuctionEditFormModel()
    {
        this.Images = new HashSet<IFormFile>();
        this.DeletedImages = new HashSet<string>();
        this.UploadedImages = new HashSet<ImageViewModel>();
    }

    [Required]
    public string Id { get; set; } = null!;

    public override IEnumerable<IFormFile> Images { get; set; }

    public IEnumerable<ImageViewModel> UploadedImages { get; set; }

    public IEnumerable<string> DeletedImages { get; set; }

    public override void CreateMappings(IProfileExpression configuration)
    {
        configuration
            .CreateMap<Auction, AuctionEditFormModel>()
            .ForMember(destination => destination.Images, options => options.Ignore())
            .ForMember(destination => destination.StartTime, options => options.MapFrom(source => source.StartTime.ToLocalTime()))
            .ForMember(destination => destination.EndTime, options => options.MapFrom(source => source.EndTime.ToUniversalTime()));
    }
}