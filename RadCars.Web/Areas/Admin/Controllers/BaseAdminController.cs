namespace RadCars.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static Common.GeneralApplicationConstants;

[Area(AdminAreaName)]
[Authorize(Roles = AdminRoleName)]
public class BaseAdminController : Controller
{
}