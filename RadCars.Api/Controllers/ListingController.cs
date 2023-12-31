﻿namespace RadCars.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using Models;
using Services.Data.Contracts;

[ApiController]
[Route("api/[controller]")]
public class ListingController : ControllerBase
{
    private readonly IListingService listingService;

    public ListingController(IListingService listingService)
    {
        this.listingService = listingService;
    }

    [HttpPost]
    [Route("favorites/exists")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> IsListingInFavorites([FromBody] ListingFavoriteInputModel data)
    {
        try
        {
            var result = await this.listingService.IsListingInUserFavoritesByIdAsync(data.ListingId, data.UserId);
            
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
    public async Task<IActionResult> ListingFavoritesCount([FromBody] ListingFavoritesCountInputModel data)
    {
        try
        {
            var result = await this.listingService.GetFavoritesCountForListingByIdAsync(data.ListingId);

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
    public async Task<IActionResult> ListingUploadedImagesCount([FromBody] string listingId)
    {
        try
        {
            var result = await this.listingService.GetUploadedImagesCountForListingByIdAsync(listingId);

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
    public async Task<IActionResult> AddListingToUserFavorites([FromBody] ListingFavoriteInputModel data)
    {
        try
        {
            await this.listingService.FavoriteListingByIdAsync(data.ListingId, data.UserId);

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
    public async Task<IActionResult> RemoveListingFromUserFavorites([FromBody] ListingFavoriteInputModel data)
    {
        try
        {
            await this.listingService.UnFavoriteListingByIdAsync(data.ListingId, data.UserId);

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