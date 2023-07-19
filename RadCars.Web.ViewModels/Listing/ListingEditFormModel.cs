// ReSharper disable VirtualMemberCallInConstructor
namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;

using AutoMapper;
using Microsoft.AspNetCore.Http;

using CarImage;
using Data.Models.Entities;

public class ListingEditFormModel : ListingFormModel
{
    public ListingEditFormModel()
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
            .CreateMap<Listing, ListingEditFormModel>()
            .ForMember(destination => destination.Images, options => options.Ignore());
    }
}