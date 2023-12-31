﻿namespace RadCars.Web.ViewModels.CarImage;

using Data.Models.Entities;
using Services.Mapping.Contracts;

public class ImageViewModel : IMapFrom<CarImage>, IMapFrom<AuctionCarImage>
{
    public string Id { get; set; } = null!;

    public string Url { get; set; } = null!;
}