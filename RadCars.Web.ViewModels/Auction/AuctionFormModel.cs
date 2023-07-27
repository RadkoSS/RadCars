namespace RadCars.Web.ViewModels.Auction;

using System.ComponentModel.DataAnnotations;
using AutoMapper;

using Common;
using Data.Models.Entities;
using Services.Mapping.Contracts;

using static RadCars.Common.EntityValidationConstants.AuctionConstants;

public class AuctionFormModel : BaseCreateFormModel, IMapFrom<Auction>, IMapTo<Auction>, IHaveCustomMappings
{
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "{0} е задължително поле.")]
    [Display(Name = "Начало на търга")]
    public DateTime StartTime { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "{0} е задължително поле.")]
    [Display(Name = "Край на търга")]
    public DateTime EndTime { get; set; }

    [Display(Name = "Начална цена")]
    [Required(ErrorMessage = "{0} е задължително поле.")]
    [Range(typeof(decimal), PriceMinimum, PriceMaximum, ErrorMessage = "Началната цена трябва да е число между {2} и {1}.")]
    public decimal StartingPrice { get; set; }

    [Display(Name = "Минимална стъпка на наддаване")]
    [Required(ErrorMessage = "{0} е задължително поле.")]
    [Range(typeof(int), PriceMinimum, PriceMaximum, ErrorMessage = "Минималната стъпка трябва да е число между {2} и {1}.")]
    public int MinimumBid { get; set; }

    [Display(Name = "Блиц цена (цена, при която търгът приключва)")]
    [Range(typeof(decimal), PriceMinimum, PriceMaximum, ErrorMessage = "{0}та трябва да е число между {2} и {1}.")]
    public decimal? BlitzPrice { get; set; }

    public virtual void CreateMappings(IProfileExpression configuration)
    {
        configuration
            .CreateMap<AuctionFormModel, Auction>()
            .ForMember(destination => destination.AuctionFeatures, options => options.Ignore())
            .ForMember(destination => destination.Images, options => options.Ignore())
            .ForMember(destination => destination.CurrentPrice,
                options =>
                    options.MapFrom(source => source.StartingPrice));
    }
}