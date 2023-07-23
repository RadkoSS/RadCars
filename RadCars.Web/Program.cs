using System.Reflection;

using AutoMapper;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.CookiePolicy;

using RadCars.Data;
using RadCars.Data.Common;
using RadCars.Data.Models.User;
using RadCars.Services.Mapping;
using RadCars.Data.Repositories;
using RadCars.Web.ViewModels.Home;
using RadCars.Services.Data.Contracts;
using RadCars.Web.Infrastructure.Extensions;
using RadCars.Web.Infrastructure.ModelBinders;
using RadCars.Data.Common.Contracts.Repositories;

using static RadCars.Common.GeneralApplicationConstants;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString).UseLazyLoadingProxies());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount =
            builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
    options.Password.RequireLowercase =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
    options.Password.RequireUppercase =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
    options.Password.RequireNonAlphanumeric =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
    options.Password.RequiredLength =
            builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
})
.AddRoles<ApplicationRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.HttpOnly = HttpOnlyPolicy.Always;
    //options.MinimumSameSitePolicy = SameSiteMode.Strict; ToDo: Research this!!
});

builder.Services.AddMvc(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});

builder.Services.AddAntiforgery(options =>
{
    options.FormFieldName = "__RequestVerificationToken";
    options.HeaderName = "X-CSRF-VERIFICATION-TOKEN";
});

//Register Data repositories and DbQuery runner
builder.Services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IDbQueryRunner, DbQueryRunner>();

//Register all services
builder.Services.AddApplicationServices(typeof(IListingService));

//Register mappings
AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
builder.Services.AddSingleton(typeof(IMapper), AutoMapperConfig.MapperInstance);

//Register CloudinaryAPI
builder.Services.AddSingleton(new Cloudinary(new Account(
    builder.Configuration.GetSection("ExternalConnections:Cloudinary:CloudName").Value,
    builder.Configuration.GetSection("ExternalConnections:Cloudinary:ApiKey").Value,
    builder.Configuration.GetSection("ExternalConnections:Cloudinary:ApiSecret").Value))
{
    Api =
    {
        Secure = true
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    await app.SeedAdministratorAsync(DevelopmentAdminEmail);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

await app.RunAsync();