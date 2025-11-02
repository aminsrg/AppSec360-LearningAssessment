using System;
using System.Collections.Generic;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.UpdateAttemptAnswer;

public record UpdateAttemptAnswerCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; init; }
    public List<Guid> SelectedAnswerIds { get; init; } = new List<Guid>();
    public string? TextAnswer { get; init; }
}
