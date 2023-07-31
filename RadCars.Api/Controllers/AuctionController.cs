namespace RadCars.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using Models;
using Services.Data.Contracts;

[ApiController]
[Route("api/[controller]")]
public class AuctionController : ControllerBase
{
    private readonly IAuctionService auctionService;

    public AuctionController(IAuctionService auctionService)
    {
        this.auctionService = auctionService;
    }

    [HttpPost]
    [Route("favorites/exists")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> IsAuctionInFavorites([FromBody] AuctionFavoriteInputModel data)
    {
        try
        {
            var result = await this.auctionService.IsAuctionInUserFavoritesByIdAsync(data.AuctionId, data.UserId);

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPost]
    [Route("favorites/count")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> AuctionFavoritesCount([FromBody] AuctionFavoritesCountInputModel data)
    {
        try
        {
            var result = await this.auctionService.GetFavoritesCountForAuctionByIdAsync(data.AuctionId);

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPost]
    [Route("bids/count")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> AuctionBidsCount([FromBody] string auctionId)
    {
        try
        {
            var result = await this.auctionService.GetBidsCountForAuctionByIdAsync(auctionId);

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPost]
    [Route("uploadedImages/count")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> AuctionUploadedImagesCount([FromBody] string auctionId)
    {
        try
        {
            var result = await this.auctionService.GetUploadedImagesCountForAuctionByIdAsync(auctionId);

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPost]
    [Route("favorites/add")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> AddAuctionToUserFavorites([FromBody] AuctionFavoriteInputModel data)
    {
        try
        {
            await this.auctionService.FavoriteAuctionByIdAsync(data.AuctionId, data.UserId);

            return Ok();
        }
        catch (InvalidOperationException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPost]
    [Route("favorites/remove")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> RemoveAuctionFromUserFavorites([FromBody] AuctionFavoriteInputModel data)
    {
        try
        {
            await this.auctionService.UnFavoriteAuctionByIdAsync(data.AuctionId, data.UserId);

            return Ok();
        }
        catch (InvalidOperationException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}