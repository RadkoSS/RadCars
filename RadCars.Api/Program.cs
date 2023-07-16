using System.Reflection;

using AutoMapper;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;

using RadCars.Data;
using RadCars.Data.Common;
using RadCars.Services.Mapping;
using RadCars.Data.Repositories;
using RadCars.Web.ViewModels.Home;
using RadCars.Services.Data.Contracts;
using RadCars.Web.Infrastructure.Extensions;
using RadCars.Data.Common.Contracts.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString).UseLazyLoadingProxies());

builder.Services.AddCors(policy => policy.AddPolicy("WebApp", configuration =>
{
    configuration.WithOrigins("https://localhost:7223").AllowAnyMethod().AllowAnyHeader();
}));

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("WebApp");

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
