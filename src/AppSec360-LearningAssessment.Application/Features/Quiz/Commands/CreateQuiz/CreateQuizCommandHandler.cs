using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Commands.CreateQuiz;

public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreateQuizCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = new Domain.Entities.Quiz
        {
            Title = request.Title,
            Description = request.Description,
            RandomizeQuestions = request.RandomizeQuestions
        };

        await _context.Quizzes.InsertOneAsync(quiz, cancellationToken: cancellationToken);

        return Result<Guid>.Success(quiz.Id);
    }
}
