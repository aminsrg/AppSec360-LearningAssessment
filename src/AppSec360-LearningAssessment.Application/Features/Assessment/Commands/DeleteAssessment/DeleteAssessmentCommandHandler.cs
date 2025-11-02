using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.Commands.DeleteAssessment;

public class DeleteAssessmentCommandHandler : IRequestHandler<DeleteAssessmentCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public DeleteAssessmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(DeleteAssessmentCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Assessment>.Filter.Eq(x => x.Id, request.Id);
        var assessment = await _context.Assessments.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (assessment == null)
        {
            return Result<Unit>.Failure($"Assessment with Id {request.Id} not found.");
        }

        // Soft delete
        assessment.IsDeleted = true;
        assessment.DeletedAt = DateTime.UtcNow;

        await _context.Assessments.ReplaceOneAsync(filter, assessment, cancellationToken: cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
