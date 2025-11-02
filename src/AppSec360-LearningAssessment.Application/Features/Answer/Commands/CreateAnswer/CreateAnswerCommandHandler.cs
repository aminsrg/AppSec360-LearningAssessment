using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Commands.CreateAnswer;

public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreateAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
    {
        // Verify question exists
        var questionFilter = Builders<Question>.Filter.Eq(x => x.Id, request.QuestionId);
        var questionExists = await _context.Questions.Find(questionFilter).AnyAsync(cancellationToken);

        if (!questionExists)
        {
            return Result<Guid>.Failure($"Question with Id {request.QuestionId} not found.");
        }

        var answer = new Domain.Entities.Answer
        {
            Text = request.Text,
            IsCorrect = request.IsCorrect,
            Feedback = request.Feedback,
            OrderIndex = request.OrderIndex,
            QuestionId = request.QuestionId
        };

        await _context.Answers.InsertOneAsync(answer, cancellationToken: cancellationToken);

        // Update question's answer IDs
        var update = Builders<Question>.Update.AddToSet(x => x.AnswerIds, answer.Id);
        await _context.Questions.UpdateOneAsync(questionFilter, update, cancellationToken: cancellationToken);

        return Result<Guid>.Success(answer.Id);
    }
}
