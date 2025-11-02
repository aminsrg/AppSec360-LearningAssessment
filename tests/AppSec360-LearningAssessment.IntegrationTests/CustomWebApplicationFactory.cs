using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Testcontainers.MongoDb;
using Xunit;

namespace AppSec360_LearningAssessment.IntegrationTests;

/// <summary>
/// Custom web application factory for integration tests with MongoDB Testcontainers.
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MongoDbContainer _dbContainer = new MongoDbBuilder()
        .WithImage("mongo:7")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            // Override connection string with test container
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = _dbContainer.GetConnectionString(),
                ["DatabaseSettings:DatabaseName"] = "AppSec360LearningAssessmentTestDb"
            });
        });

        builder.ConfigureServices(services =>
        {
            // Remove existing MongoDB client
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IMongoClient));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add test MongoDB client
            services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(_dbContainer.GetConnectionString()));
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}
