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
    [ProducesResponseType(200, Type = typeof(JsonContent))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetModelsByMakeId(string makeId)
    {
        try
        {
            var idAsNumber = Convert.ToUInt16(makeId);

            var makes = await this.carService.GetModelsByMakeIdAsync(idAsNumber);

            return Ok(makes);
        }
        catch
        {
            return NotFound();
        }
    }
}