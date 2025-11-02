using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Question.Commands.UpdateQuestion;

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public UpdateQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Question>.Filter.Eq(x => x.Id, request.Id);
        var question = await _context.Questions.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (question == null)
        {
            return Result<Unit>.Failure($"Question with Id {request.Id} not found.");
        }

        question.Text = request.Text;
        question.Type = request.Type;
        question.Topic = request.Topic;
        question.MaxPoints = request.MaxPoints;
        question.OrderIndex = request.OrderIndex;
        question.UpdatedAt = DateTime.UtcNow;

        await _context.Questions.ReplaceOneAsync(filter, question, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
