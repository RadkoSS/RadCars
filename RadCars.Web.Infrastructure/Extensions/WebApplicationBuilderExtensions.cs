namespace RadCars.Web.Infrastructure.Extensions;

using System.Reflection;

using SendGrid;
using Ganss.Xss;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Data;
using Middlewares;
using ViewModels.Home;
using Data.Models.User;
using Services.Mapping;
using Data.Repositories;
using Services.Messaging;
using RadCars.Data.Common;
using Services.Data.Contracts;
using Services.Messaging.Contracts;
using RadCars.Data.Common.Contracts.Repositories;

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

        //Register mappings
        AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        services.AddSingleton(typeof(IMapper), AutoMapperConfig.MapperInstance);

        //Register Data repositories and DbQuery runner
        services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<IDbQueryRunner, DbQueryRunner>();
    }

    /// <summary>
    /// This method registers all external services with the credentials provided in the config files.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddExternalApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Register CloudinaryAPI
        services.AddSingleton(new Cloudinary(new Account(
            configuration.GetSection("ExternalConnections:Cloudinary:CloudName").Value,
            configuration.GetSection("ExternalConnections:Cloudinary:ApiKey").Value,
            configuration.GetSection("ExternalConnections:Cloudinary:ApiSecret").Value))
        {
            Api =
            {
                Secure = true
            }
        });

        //Register Sendgrid
        services.AddSingleton<ISendGridClient>(new SendGridClient(configuration.GetSection("Authentication:SendGrid:ApiKey").Value));
        services.AddSingleton<IEmailSender, SendGridEmailSender>();
    }

    /// <summary>
    /// This method seeds admin role if it does not exist.
    /// Passed email should be valid email of existing user in the application.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="email"></param>
    /// <returns>IApplicationBuilder</returns>
    public static async Task<IApplicationBuilder> SeedAdministratorAsync(this IApplicationBuilder app, string email)
    {
        using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

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

        return app;
    }

    public static IApplicationBuilder EnableOnlineUsersCheck(this IApplicationBuilder app)
        => app.UseMiddleware<OnlineUsersMiddleware>();
}