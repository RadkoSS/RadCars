namespace RadCars.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Data.Contracts;
using Web.ViewModels.CarModel;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService carService;

    public CarController(ICarService carService)
    {
        this.carService = carService;
    }

    [HttpGet]
    [Route("models/{makeId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarModelViewModel>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetModelsByMakeId(string makeId)
    {
        try
        {
            var idAsNumber = Convert.ToUInt16(makeId);

            var makes = await this.carService.GetModelsByMakeIdAsync(idAsNumber);

            if (makes.Any())
            {
                return Ok(makes);
            }

            return NoContent();
        }
        catch
        {
            return NoContent();
        }
    }
}