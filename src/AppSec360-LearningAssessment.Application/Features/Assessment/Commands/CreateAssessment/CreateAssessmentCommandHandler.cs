using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Entities;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.Commands.CreateAssessment;

public class CreateAssessmentCommandHandler : IRequestHandler<CreateAssessmentCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreateAssessmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateAssessmentCommand request, CancellationToken cancellationToken)
    {
        var assessment = new Domain.Entities.Assessment
        {
            Title = request.Title,
            Description = request.Description,
            RandomizeQuestions = request.RandomizeQuestions,
            TimeLimitMinutes = request.TimeLimitMinutes,
            MaxAttemptsPerUser = request.MaxAttemptsPerUser,
            PassingScorePercent = request.PassingScorePercent
        };

        await _context.Assessments.InsertOneAsync(assessment, cancellationToken: cancellationToken);

        return Result<Guid>.Success(assessment.Id);
    }
}
