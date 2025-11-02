using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Commands.UpdateAttempt;

public record UpdateAttemptCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; init; }
    public DateTime? EndTime { get; init; }
    public double? Score { get; init; }
    public bool IsSubmitted { get; init; }
}
