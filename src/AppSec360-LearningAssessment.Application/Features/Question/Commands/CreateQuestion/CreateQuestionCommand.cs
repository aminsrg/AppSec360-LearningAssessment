using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Domain.Enums;

namespace AppSec360_LearningAssessment.Application.Features.Question.Commands.CreateQuestion;

public record CreateQuestionCommand : IRequest<Result<Guid>>
{
    public string Text { get; init; } = string.Empty;
    public QuestionType Type { get; init; }
    public string? Topic { get; init; }
    public double MaxPoints { get; init; } = 1.0;
    public int OrderIndex { get; init; }
    public Guid QuizId { get; init; }
}
