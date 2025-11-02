using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Commands.CreateAttempt;

public record CreateAttemptCommand : IRequest<Result<Guid>>
{
    public string UserId { get; init; } = string.Empty;
    public Guid AssessmentId { get; init; }
}
