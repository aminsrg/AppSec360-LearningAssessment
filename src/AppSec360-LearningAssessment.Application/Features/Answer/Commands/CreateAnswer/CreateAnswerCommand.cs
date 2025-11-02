using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Commands.CreateAnswer;

public record CreateAnswerCommand : IRequest<Result<Guid>>
{
    public string Text { get; init; } = string.Empty;
    public bool IsCorrect { get; init; }
    public string? Feedback { get; init; }
    public int OrderIndex { get; init; }
    public Guid QuestionId { get; init; }
}
