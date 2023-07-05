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
    [Route("makes/{makeId}")]
    [ProducesResponseType(typeof(JsonContent), 200)]
    public async Task<IEnumerable<CarModelViewModel>> GetModelsByMakeId(string makeId)
    {
        try
        {
            var idAsNumber = Convert.ToUInt16(makeId);

            var makes = await this.carService.GetModelsByMakeIdAsync(idAsNumber);

            return makes;
        }
        catch
        {
            return Enumerable.Empty<CarModelViewModel>();
        }
    }
}