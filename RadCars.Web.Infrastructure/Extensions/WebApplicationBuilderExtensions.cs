namespace RadCars.Web.Infrastructure.Extensions;

using System.Reflection;

using SendGrid;
using Ganss.Xss;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Data;
using Middlewares;
using Data.Seeding;
using ViewModels.Home;
using Services.Mapping;
using Data.Repositories;
using Services.Messaging;
using RadCars.Data.Common;
using Services.Data.Contracts;
using Services.Messaging.Contracts;
using RadCars.Data.Common.Contracts.Repositories;

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

        //Register SendGrid
        services.AddSingleton<ISendGridClient>(new SendGridClient(configuration.GetSection("Authentication:SendGrid:ApiKey").Value));
        services.AddSingleton<IEmailSender, SendGridEmailSender>();
    }

    /// <summary>
    /// This method creates the Hangfire database if it does not exist with the name specified in the Hangfire connection string.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static async Task CreateHangfireDatabaseIfItDoesNotExistAsync(this IServiceCollection services, IConfiguration configuration)
    {
        var hangfireDbName = configuration.GetConnectionString("HangfireConnection").Split(';', StringSplitOptions.RemoveEmptyEntries)[1].Split('=', StringSplitOptions.RemoveEmptyEntries)[1];

        var masterConnectionString =
            configuration.GetConnectionString("HangfireConnection").Replace(hangfireDbName, "master");

        await using var connection = new SqlConnection(masterConnectionString);
        await connection.OpenAsync();

        const string query = @"
         IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = @dbName) 
                 BEGIN
               DECLARE @sql NVARCHAR(40)
                   SET @sql = N'CREATE DATABASE [' + @dbName + N']'
                  EXEC sp_executesql @sql
                   END";

        //Secured from SQL injection, this query connects to **master** and creates the Hangfire database if it does not exist.
        await using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@dbName", hangfireDbName);

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    /// <summary>
    /// This method seeds all the data registered in the classes that implement the ISeeder interface on application startup.
    /// </summary>
    /// <param name="app"></param>
    public static async Task<IApplicationBuilder> SeedAllDataAsync(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        //Migrations should be applied with the package manager console or power shell. If you want automatic migration applying - uncomment this code:
        //await dbContext.Database.MigrateAsync();

        await new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider);

        return app;
    }

    public static IApplicationBuilder EnableOnlineUsersCheck(this IApplicationBuilder app)
        => app.UseMiddleware<OnlineUsersMiddleware>();
}