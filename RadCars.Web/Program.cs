using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.CookiePolicy;

using RadCars.Data;
using RadCars.Web.Hubs;
using RadCars.Services.Data;
using RadCars.Data.Models.User;
using RadCars.Web.BackgroundServices;
using RadCars.Services.Data.Contracts;
using RadCars.Web.Infrastructure.Filters;
using RadCars.Web.Infrastructure.Extensions;
using RadCars.Web.Infrastructure.ModelBinders;
using RadCars.Web.BackgroundServices.Contracts;

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

//ToDo: Research this!!
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.HttpOnly = HttpOnlyPolicy.Always;
    //options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = "/User/Login";
    cfg.AccessDeniedPath = "/Home/Error/401";
});

builder.Services.AddAuthentication()
.AddGoogle(googleOptions => {
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});

builder.Services.AddMvc(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    options.ModelBinderProviders.Insert(1, new CustomDateTimeFormatModelBinderProvider());
});

builder.Services.AddAntiforgery(options =>
{
    options.FormFieldName = "__RequestVerificationToken";
    options.HeaderName = "X-CSRF-VERIFICATION-TOKEN";
});

//Register all services
builder.Services.AddApplicationServices(typeof(IListingService));
builder.Services.AddExternalApplicationServices(builder.Configuration);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddRecaptchaService();

builder.Services.AddSignalR();

builder.Services.AddHangfire(hf => hf.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));
builder.Services.AddHangfireServer();

builder.Services.AddScoped<IBackgroundJobClient, BackgroundJobClient>();
builder.Services.AddScoped<IAuctionBackgroundJobService, AuctionBackgroundJobService>();

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
    app.UseHangfireDashboard("/hangfireDashboard");
}
else
{
    app.UseHangfireDashboard("/hangfireDashboard", new DashboardOptions
    {
        Authorization = new[] { new HangfireAuthorizationFilter() }
    });
}

app.EnableOnlineUsersCheck();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapDefaultControllerRoute();

    endpoints.MapRazorPages();
});

app.MapRazorPages();

app.MapHub<AuctionHub>("/auctionHub");

await app.RunAsync();