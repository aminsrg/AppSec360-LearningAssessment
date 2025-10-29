using System.Reflection;
using AppSec360_LearningAssessment.Api.Extensions;
using AppSec360_LearningAssessment.Api.Filters;
using AppSec360_LearningAssessment.Application.Common.Behaviors;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Infrastructure.Persistence;
using AppSec360_LearningAssessment.Infrastructure.Services;
using FluentValidation;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers with API versioning
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Add MediatR with application assembly
var applicationAssembly = Assembly.Load("AppSec360-LearningAssessment.Application");
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(applicationAssembly);
});

// Add MediatR behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

// Add FluentValidation
builder.Services.AddValidatorsFromAssembly(applicationAssembly);

// Add AutoMapper
builder.Services.AddAutoMapper(applicationAssembly);

// Add MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<MongoDbContext>();

// Add application services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IDateTime, DateTimeService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Add Swagger with versioning support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = builder.Configuration["ApiSettings:ApiTitle"],
        Version = "v1",
        Description = "AppSec360-LearningAssessment Microservice API"
    });

    // Include XML comments if available
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add health checks
builder.Services.AddHealthChecks()
    .AddMongoDb(
        builder.Configuration.GetConnectionString("DefaultConnection")!,
        name: "mongodb",
        timeout: TimeSpan.FromSeconds(3),
        tags: new[] { "ready" });

var app = builder.Build();

// Configure the HTTP request pipeline

// Exception handling (first)
app.UseCustomMiddleware();

// Swagger (development only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppSec360-LearningAssessment API v1"));
}

// CORS
app.UseCors("AllowAll");

// HTTPS redirection
app.UseHttpsRedirection();

// Authorization
app.UseAuthorization();

// Controllers
app.MapControllers();

// Health checks endpoint
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

// Make Program class accessible to integration tests
public partial class Program { }
