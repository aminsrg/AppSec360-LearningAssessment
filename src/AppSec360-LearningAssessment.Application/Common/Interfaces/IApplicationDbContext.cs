using Microsoft.EntityFrameworkCore.Storage;

namespace AppSec360_LearningAssessment.Application.Common.Interfaces;

/// <summary>
/// Interface for the application database context.
/// </summary>
public interface IApplicationDbContext
{
    // DbSets will be added here as entities are created
    // Example: DbSet<Product> Products { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
