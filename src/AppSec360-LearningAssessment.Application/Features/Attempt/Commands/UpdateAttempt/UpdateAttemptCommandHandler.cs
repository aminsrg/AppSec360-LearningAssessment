using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Commands.UpdateAttempt;

public class UpdateAttemptCommandHandler : IRequestHandler<UpdateAttemptCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public UpdateAttemptCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(UpdateAttemptCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Attempt>.Filter.Eq(x => x.Id, request.Id);
        var attempt = await _context.Attempts.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (attempt == null)
        {
            return Result<Unit>.Failure($"Attempt with Id {request.Id} not found.");
        }

        attempt.EndTime = request.EndTime;
        attempt.Score = request.Score;
        attempt.IsSubmitted = request.IsSubmitted;
        attempt.UpdatedAt = DateTime.UtcNow;

        await _context.Attempts.ReplaceOneAsync(filter, attempt, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
