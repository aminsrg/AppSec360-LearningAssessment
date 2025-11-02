using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.DeleteAttemptAnswer;

public class DeleteAttemptAnswerCommandHandler : IRequestHandler<DeleteAttemptAnswerCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public DeleteAttemptAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(DeleteAttemptAnswerCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.AttemptAnswer>.Filter.Eq(x => x.Id, request.Id);
        var attemptAnswer = await _context.AttemptAnswers.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (attemptAnswer == null)
        {
            return Result<Unit>.Failure($"AttemptAnswer with Id {request.Id} not found.");
        }

        // Soft delete
        attemptAnswer.IsDeleted = true;
        attemptAnswer.DeletedAt = DateTime.UtcNow;

        await _context.AttemptAnswers.ReplaceOneAsync(filter, attemptAnswer, cancellationToken: cancellationToken);

        // Remove from attempt's answer IDs
        var attemptFilter = Builders<Domain.Entities.Attempt>.Filter.Eq(x => x.Id, attemptAnswer.AttemptId);
        var update = Builders<Domain.Entities.Attempt>.Update.Pull(x => x.AttemptAnswerIds, attemptAnswer.Id);
        await _context.Attempts.UpdateOneAsync(attemptFilter, update, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
