using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Commands.UpdateAnswer;

public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public UpdateAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Answer>.Filter.Eq(x => x.Id, request.Id);
        var answer = await _context.Answers.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (answer == null)
        {
            return Result<Unit>.Failure($"Answer with Id {request.Id} not found.");
        }

        answer.Text = request.Text;
        answer.IsCorrect = request.IsCorrect;
        answer.Feedback = request.Feedback;
        answer.OrderIndex = request.OrderIndex;
        answer.UpdatedAt = DateTime.UtcNow;

        await _context.Answers.ReplaceOneAsync(filter, answer, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
