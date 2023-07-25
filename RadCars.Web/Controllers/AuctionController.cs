namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Griesoft.AspNetCore.ReCaptcha;

using Services.Data.Contracts;

public class AuctionController : BaseController
{
    private readonly ICarService carService;
    private readonly IUserService userService;
    private readonly IAuctionService auctionService;

    public AuctionController(IAuctionService auctionService, IUserService userService, ICarService carService)
    {
        this.carService = carService;
        this.userService = userService;
        this.auctionService = auctionService;
    }

    //[HttpGet]
    //public async Task<IActionResult> Create()
    //{

    //}

    //[HttpPost]
    //[ValidateRecaptcha(Action = nameof(Create),
    //    ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
    //public async Task<IActionResult> Create()
    //{

    //}
}