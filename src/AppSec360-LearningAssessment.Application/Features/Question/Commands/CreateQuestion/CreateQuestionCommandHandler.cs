using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Features.Question.Commands.CreateQuestion;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreateQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        // Verify quiz exists
        var quizFilter = Builders<Quiz>.Filter.Eq(x => x.Id, request.QuizId);
        var quizExists = await _context.Quizzes.Find(quizFilter).AnyAsync(cancellationToken);

        if (!quizExists)
        {
            return Result<Guid>.Failure($"Quiz with Id {request.QuizId} not found.");
        }

        var question = new Domain.Entities.Question
        {
            Text = request.Text,
            Type = request.Type,
            Topic = request.Topic,
            MaxPoints = request.MaxPoints,
            OrderIndex = request.OrderIndex,
            QuizId = request.QuizId
        };

        await _context.Questions.InsertOneAsync(question, cancellationToken: cancellationToken);

        // Update quiz's question IDs
        var update = Builders<Quiz>.Update.AddToSet(x => x.QuestionIds, question.Id);
        await _context.Quizzes.UpdateOneAsync(quizFilter, update, cancellationToken: cancellationToken);

        return Result<Guid>.Success(question.Id);
    }
}
