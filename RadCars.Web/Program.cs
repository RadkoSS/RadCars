using System.Reflection;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
.AddEntityFrameworkStores<ApplicationDbContext>();

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

//Register all Data services
builder.Services.AddApplicationServices(typeof(IListingService));

//Register mappings
AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
builder.Services.AddSingleton(typeof(IMapper), AutoMapperConfig.MapperInstance);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

await app.RunAsync();