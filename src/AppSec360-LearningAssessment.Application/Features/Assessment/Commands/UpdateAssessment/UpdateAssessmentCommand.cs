using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.Commands.UpdateAssessment;

public record UpdateAssessmentCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool RandomizeQuestions { get; init; } = false;
    public int TimeLimitMinutes { get; init; } = 0;
    public int MaxAttemptsPerUser { get; init; } = 1;
    public double PassingScorePercent { get; init; } = 60.0;
}
