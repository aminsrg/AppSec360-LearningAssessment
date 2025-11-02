using System;
using System.Collections.Generic;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.CreateAttemptAnswer;

public record CreateAttemptAnswerCommand : IRequest<Result<Guid>>
{
    public Guid QuestionId { get; init; }
    public Guid AttemptId { get; init; }
    public List<Guid> SelectedAnswerIds { get; init; } = new List<Guid>();
    public string? TextAnswer { get; init; }
}
