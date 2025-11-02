using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Domain.Common;
using AppSec360_LearningAssessment.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AppSec360_LearningAssessment.Infrastructure.Persistence;

/// <summary>
/// MongoDB database context for the application.
/// </summary>
public class MongoDbContext : IApplicationDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IMongoClient _mongoClient;
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;

    public MongoDbContext(
        IMongoClient mongoClient,
        IConfiguration configuration,
        IDateTime dateTime,
        ICurrentUserService currentUserService)
    {
        _mongoClient = mongoClient;
        _database = mongoClient.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
        _dateTime = dateTime;
        _currentUserService = currentUserService;

        RegisterClassMaps();
    }

    // MongoDB Collections
    public IMongoCollection<Quiz> Quizzes => _database.GetCollection<Quiz>("Quizzes");
    public IMongoCollection<Question> Questions => _database.GetCollection<Question>("Questions");
    public IMongoCollection<Answer> Answers => _database.GetCollection<Answer>("Answers");
    public IMongoCollection<Assessment> Assessments => _database.GetCollection<Assessment>("Assessments");
    public IMongoCollection<Attempt> Attempts => _database.GetCollection<Attempt>("Attempts");
    public IMongoCollection<AttemptAnswer> AttemptAnswers => _database.GetCollection<AttemptAnswer>("AttemptAnswers");

    private void RegisterClassMaps()
    {
        // Register class maps from assembly
        // This will be populated as entities are added
    }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// Note: MongoDB doesn't have a SaveChanges concept like EF Core.
    /// This method exists for interface compatibility and returns 0.
    /// All changes are saved immediately when InsertOne, UpdateOne, etc. are called.
    /// </summary>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // MongoDB operations are immediate, no explicit SaveChanges needed
        return Task.FromResult(0);
    }

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    public async Task<IClientSessionHandle> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var session = await _mongoClient.StartSessionAsync(cancellationToken: cancellationToken);
        session.StartTransaction();
        return session;
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
