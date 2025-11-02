using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.UpdateAttemptAnswer;

public class UpdateAttemptAnswerCommandHandler : IRequestHandler<UpdateAttemptAnswerCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public UpdateAttemptAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(UpdateAttemptAnswerCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.AttemptAnswer>.Filter.Eq(x => x.Id, request.Id);
        var attemptAnswer = await _context.AttemptAnswers.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (attemptAnswer == null)
        {
            return Result<Unit>.Failure($"AttemptAnswer with Id {request.Id} not found.");
        }

        attemptAnswer.SelectedAnswerIds = request.SelectedAnswerIds ?? new System.Collections.Generic.List<Guid>();
        attemptAnswer.TextAnswer = request.TextAnswer;
        attemptAnswer.UpdatedAt = DateTime.UtcNow;

        await _context.AttemptAnswers.ReplaceOneAsync(filter, attemptAnswer, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
