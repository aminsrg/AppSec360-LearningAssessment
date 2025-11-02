using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.CreateAttemptAnswer;

public class CreateAttemptAnswerCommandHandler : IRequestHandler<CreateAttemptAnswerCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreateAttemptAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateAttemptAnswerCommand request, CancellationToken cancellationToken)
    {
        // Verify attempt exists
        var attemptFilter = Builders<Domain.Entities.Attempt>.Filter.Eq(x => x.Id, request.AttemptId);
        var attemptExists = await _context.Attempts.Find(attemptFilter).AnyAsync(cancellationToken);

        if (!attemptExists)
        {
            return Result<Guid>.Failure($"Attempt with Id {request.AttemptId} not found.");
        }

        // Verify question exists
        var questionFilter = Builders<Question>.Filter.Eq(x => x.Id, request.QuestionId);
        var questionExists = await _context.Questions.Find(questionFilter).AnyAsync(cancellationToken);

        if (!questionExists)
        {
            return Result<Guid>.Failure($"Question with Id {request.QuestionId} not found.");
        }

        var attemptAnswer = new Domain.Entities.AttemptAnswer
        {
            QuestionId = request.QuestionId,
            AttemptId = request.AttemptId,
            SelectedAnswerIds = request.SelectedAnswerIds ?? new System.Collections.Generic.List<Guid>(),
            TextAnswer = request.TextAnswer
        };

        await _context.AttemptAnswers.InsertOneAsync(attemptAnswer, cancellationToken: cancellationToken);

        // Update attempt's answer IDs
        var update = Builders<Domain.Entities.Attempt>.Update.AddToSet(x => x.AttemptAnswerIds, attemptAnswer.Id);
        await _context.Attempts.UpdateOneAsync(attemptFilter, update, cancellationToken: cancellationToken);

        return Result<Guid>.Success(attemptAnswer.Id);
    }
}
