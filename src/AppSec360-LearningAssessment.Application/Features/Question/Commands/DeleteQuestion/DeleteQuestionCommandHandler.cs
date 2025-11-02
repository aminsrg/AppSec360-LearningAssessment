using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Features.Question.Commands.DeleteQuestion;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public DeleteQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Question>.Filter.Eq(x => x.Id, request.Id);
        var question = await _context.Questions.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (question == null)
        {
            return Result<Unit>.Failure($"Question with Id {request.Id} not found.");
        }

        // Soft delete
        question.IsDeleted = true;
        question.DeletedAt = DateTime.UtcNow;

        await _context.Questions.ReplaceOneAsync(filter, question, cancellationToken: cancellationToken);

        // Remove from quiz's question IDs
        var quizFilter = Builders<Quiz>.Filter.Eq(x => x.Id, question.QuizId);
        var update = Builders<Quiz>.Update.Pull(x => x.QuestionIds, question.Id);
        await _context.Quizzes.UpdateOneAsync(quizFilter, update, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
