using MongoDB.Driver;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Common.Interfaces;

/// <summary>
/// Interface for the application database context.
/// </summary>
public interface IApplicationDbContext
{
    // MongoDB Collections
    IMongoCollection<Quiz> Quizzes { get; }
    IMongoCollection<Question> Questions { get; }
    IMongoCollection<Answer> Answers { get; }
    IMongoCollection<Assessment> Assessments { get; }
    IMongoCollection<Attempt> Attempts { get; }
    IMongoCollection<AttemptAnswer> AttemptAnswers { get; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    Task<IClientSessionHandle> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
