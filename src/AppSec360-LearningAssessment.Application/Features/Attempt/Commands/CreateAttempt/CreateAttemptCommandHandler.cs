using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Commands.CreateAttempt;

public class CreateAttemptCommandHandler : IRequestHandler<CreateAttemptCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreateAttemptCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateAttemptCommand request, CancellationToken cancellationToken)
    {
        // Verify assessment exists
        var assessmentFilter = Builders<Assessment>.Filter.Eq(x => x.Id, request.AssessmentId);
        var assessment = await _context.Assessments.Find(assessmentFilter).FirstOrDefaultAsync(cancellationToken);

        if (assessment == null)
        {
            return Result<Guid>.Failure($"Assessment with Id {request.AssessmentId} not found.");
        }

        // Check if user has exceeded max attempts
        var userAttemptsFilter = Builders<Domain.Entities.Attempt>.Filter.And(
            Builders<Domain.Entities.Attempt>.Filter.Eq(x => x.AssessmentId, request.AssessmentId),
            Builders<Domain.Entities.Attempt>.Filter.Eq(x => x.UserId, request.UserId),
            Builders<Domain.Entities.Attempt>.Filter.Eq(x => x.IsDeleted, false)
        );
        var userAttemptsCount = await _context.Attempts.CountDocumentsAsync(userAttemptsFilter, cancellationToken: cancellationToken);

        if (userAttemptsCount >= assessment.MaxAttemptsPerUser)
        {
            return Result<Guid>.Failure($"User has reached the maximum number of attempts ({assessment.MaxAttemptsPerUser}) for this assessment.");
        }

        var attempt = new Domain.Entities.Attempt
        {
            UserId = request.UserId,
            AssessmentId = request.AssessmentId,
            StartTime = DateTime.UtcNow
        };

        await _context.Attempts.InsertOneAsync(attempt, cancellationToken: cancellationToken);

        // Update assessment's attempt IDs
        var update = Builders<Assessment>.Update.AddToSet(x => x.AttemptIds, attempt.Id);
        await _context.Assessments.UpdateOneAsync(assessmentFilter, update, cancellationToken: cancellationToken);

        return Result<Guid>.Success(attempt.Id);
    }
}
