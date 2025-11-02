using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Commands.DeleteQuiz;

public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public DeleteQuizCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Quiz>.Filter.Eq(x => x.Id, request.Id);
        var quiz = await _context.Quizzes.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (quiz == null)
        {
            return Result<Unit>.Failure($"Quiz with Id {request.Id} not found.");
        }

        // Soft delete
        quiz.IsDeleted = true;
        quiz.DeletedAt = DateTime.UtcNow;

        await _context.Quizzes.ReplaceOneAsync(filter, quiz, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
