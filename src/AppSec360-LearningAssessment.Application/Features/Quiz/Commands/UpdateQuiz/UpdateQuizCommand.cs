using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Commands.UpdateQuiz;

public record UpdateQuizCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool RandomizeQuestions { get; init; } = false;
}
