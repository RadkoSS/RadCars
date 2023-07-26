namespace RadCars.Web.ViewModels.Listing;

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Common;
using Data.Models.Entities;
using Services.Mapping.Contracts;

using static RadCars.Common.EntityValidationConstants.ListingConstants;

public class ListingFormModel : BaseCreateFormModel, IMapTo<Listing>, IMapFrom<Listing>, IHaveCustomMappings
{
    [Display(Name = "Цена")]
    [Required(ErrorMessage = "{0}та е задължително поле.")]
    [Range(typeof(decimal), PriceMinimum, PriceMaximum, ErrorMessage = "{0}та трябва да е число между {2} и {1}.")]
    public decimal Price { get; set; }

    public virtual void CreateMappings(IProfileExpression configuration)
    {
        configuration
            .CreateMap<ListingFormModel, Listing>()
            .ForMember(destination => destination.ListingFeatures, options => options.Ignore())
            .ForMember(destination => destination.Images, options => options.Ignore());
    }
}