namespace RadCars.Web.Infrastructure.Filters;

using Hangfire.Dashboard;

using static Common.GeneralApplicationConstants;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        return httpContext.User.Identity?.IsAuthenticated != false && httpContext.User.IsInRole(AdminRoleName);
    }
}