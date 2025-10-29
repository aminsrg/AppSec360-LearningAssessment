using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Domain.Common;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AppSec360_LearningAssessment.Infrastructure.Persistence;

/// <summary>
/// MongoDB database context for the application.
/// </summary>
public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;

    public MongoDbContext(
        IMongoClient mongoClient,
        IConfiguration configuration,
        IDateTime dateTime,
        ICurrentUserService currentUserService)
    {
        _database = mongoClient.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
        _dateTime = dateTime;
        _currentUserService = currentUserService;

        RegisterClassMaps();
    }

    // Collections will be added here as entities are created
    // Example: public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");

    private void RegisterClassMaps()
    {
        // Register class maps from assembly
        // This will be populated as entities are added
    }

    /// <summary>
    /// Applies audit information to an entity.
    /// </summary>
    public void ApplyAuditInfo<T>(T entity, bool isNew) where T : IAuditableEntity
    {
        if (isNew)
        {
            entity.CreatedBy = _currentUserService.UserId ?? "System";
            entity.CreatedAt = _dateTime.UtcNow;
        }
        else
        {
            entity.UpdatedBy = _currentUserService.UserId ?? "System";
            entity.UpdatedAt = _dateTime.UtcNow;
        }
    }

    /// <summary>
    /// Applies soft delete to an entity.
    /// </summary>
    public void ApplySoftDelete<T>(T entity) where T : ISoftDeletable
    {
        entity.IsDeleted = true;
        entity.DeletedAt = _dateTime.UtcNow;
        entity.DeletedBy = _currentUserService.UserId ?? "System";
    }
}
