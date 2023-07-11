namespace RadCars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[AutoValidateAntiforgeryToken]
public class BaseController : Controller
{
}