using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Commands.UpdateQuiz;

public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public UpdateQuizCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Quiz>.Filter.Eq(x => x.Id, request.Id);
        var quiz = await _context.Quizzes.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (quiz == null)
        {
            return Result<Unit>.Failure($"Quiz with Id {request.Id} not found.");
        }

        quiz.Title = request.Title;
        quiz.Description = request.Description;
        quiz.RandomizeQuestions = request.RandomizeQuestions;
        quiz.UpdatedAt = DateTime.UtcNow;

        await _context.Quizzes.ReplaceOneAsync(filter, quiz, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
