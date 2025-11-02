using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Commands.DeleteAnswer;

public class DeleteAnswerCommandHandler : IRequestHandler<DeleteAnswerCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public DeleteAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(DeleteAnswerCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Answer>.Filter.Eq(x => x.Id, request.Id);
        var answer = await _context.Answers.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (answer == null)
        {
            return Result<Unit>.Failure($"Answer with Id {request.Id} not found.");
        }

        // Soft delete
        answer.IsDeleted = true;
        answer.DeletedAt = DateTime.UtcNow;

        await _context.Answers.ReplaceOneAsync(filter, answer, cancellationToken: cancellationToken);

        // Remove from question's answer IDs
        var questionFilter = Builders<Question>.Filter.Eq(x => x.Id, answer.QuestionId);
        var update = Builders<Question>.Update.Pull(x => x.AnswerIds, answer.Id);
        await _context.Questions.UpdateOneAsync(questionFilter, update, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
