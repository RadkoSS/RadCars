namespace RadCars.Web.Infrastructure.Extensions;

using System.Reflection;

using Ganss.Xss;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Data.Models.User;
using Services.Data.Contracts;

using static Common.GeneralApplicationConstants;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// This method registers all services with their interfaces and implementations of given assembly.
    /// The assembly is taken from the type of random service interface or implementation provided.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceType"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
    {
        Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);

        if (serviceAssembly == null)
        {
            throw new InvalidOperationException("Invalid service type provided!");
        }

        var implementationTypes = serviceAssembly
            .GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
            .ToArray();

        foreach (var implementationType in implementationTypes)
        {
            var interfaceType = implementationType
                .GetInterface($"I{implementationType.Name}");
            if (interfaceType == null)
            {
                throw new InvalidOperationException(
                    $"No interface is provided for the service with name: {implementationType.Name}");
            }

            if (interfaceType == typeof(IUserService))
            {
                continue;
            }

            services.AddScoped(interfaceType, implementationType);
        }

        services.AddSingleton<IHtmlSanitizer, HtmlSanitizer>();
    }

    /// <summary>
    /// This method seeds admin role if it does not exist.
    /// Passed email should be valid email of existing user in the application.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="email"></param>
    /// <returns>IApplicationBuilder</returns>
    public static async Task<IApplicationBuilder> SeedAdministratorAsync(this IApplicationBuilder builder, string email)
    {
        using IServiceScope scopedServices = builder.ApplicationServices.CreateScope();

        IServiceProvider serviceProvider = scopedServices.ServiceProvider;

        UserManager<ApplicationUser> userManager =
            serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        RoleManager<ApplicationRole> roleManager =
            serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        if (!await roleManager.RoleExistsAsync(AdminRoleName))
        {
            ApplicationRole role = new ApplicationRole(AdminRoleName);
            await roleManager.CreateAsync(role);
        }

        ApplicationUser adminUser = await userManager.FindByEmailAsync(email);

        if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, AdminRoleName))
        {
            await userManager.AddToRoleAsync(adminUser, AdminRoleName);
        }

        return builder;
    }
}