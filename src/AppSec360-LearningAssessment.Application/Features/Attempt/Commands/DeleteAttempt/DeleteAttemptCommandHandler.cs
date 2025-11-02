using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Commands.DeleteAttempt;

public class DeleteAttemptCommandHandler : IRequestHandler<DeleteAttemptCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public DeleteAttemptCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(DeleteAttemptCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Attempt>.Filter.Eq(x => x.Id, request.Id);
        var attempt = await _context.Attempts.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (attempt == null)
        {
            return Result<Unit>.Failure($"Attempt with Id {request.Id} not found.");
        }

        // Soft delete
        attempt.IsDeleted = true;
        attempt.DeletedAt = DateTime.UtcNow;

        await _context.Attempts.ReplaceOneAsync(filter, attempt, cancellationToken: cancellationToken);

        // Remove from assessment's attempt IDs
        var assessmentFilter = Builders<Assessment>.Filter.Eq(x => x.Id, attempt.AssessmentId);
        var update = Builders<Assessment>.Update.Pull(x => x.AttemptIds, attempt.Id);
        await _context.Assessments.UpdateOneAsync(assessmentFilter, update, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
