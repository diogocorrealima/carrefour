using FluxoDeCaixa.Infra.CrossCutting.Identity;
using FluxoDeCaixa.Services.Api;
using FluxoDeCaixa.Services.Api.Configurations;
using MediatR;
using NetDevPack.Identity;
using NetDevPack.Identity.User;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// ConfigureServices

// WebAPI Config
builder.Services.AddControllers();

// Setting DBContexts
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// ASP.NET Identity Settings & JWT
builder.Services.AddApiIdentityConfiguration(builder.Configuration);

// Interactive AspNetUser (logged in)
builder.Services.AddAspNetUserConfiguration();

// AutoMapper Settings
builder.Services.AddAutoMapperConfiguration();

// Swagger Config
builder.Services.AddSwaggerConfiguration();

// Adding MediatR for Domain Events and Notifications
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddControllers(options => options.Filters.Add(typeof(HttpGlobalExceptionFilter)));

// .NET Native DI Abstraction
builder.Services.AddDependencyInjectionConfiguration();
var app = builder.Build();

// Configure

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthConfiguration();

app.MapControllers();

app.UseSwaggerSetup();

app.Run();