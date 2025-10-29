using AppSec360_LearningAssessment.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppSec360_LearningAssessment.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior that wraps command handlers in database transactions.
/// Queries are not wrapped in transactions.
/// </summary>
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(IApplicationDbContext context, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Only wrap commands in transactions, not queries
        var requestName = typeof(TRequest).Name;
        if (requestName.EndsWith("Query"))
        {
            return await next();
        }

        _logger.LogInformation("Beginning transaction for {RequestName}", requestName);

        await using var transaction = await _context.BeginTransactionAsync(cancellationToken);

        try
        {
            var response = await next();

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Committed transaction for {RequestName}", requestName);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling transaction for {RequestName}", requestName);
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
