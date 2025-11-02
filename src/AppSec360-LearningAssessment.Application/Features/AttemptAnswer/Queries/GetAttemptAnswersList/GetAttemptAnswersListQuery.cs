using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.AttemptAnswer.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Queries.GetAttemptAnswersList;

public record GetAttemptAnswersListQuery : IRequest<Result<PaginatedList<AttemptAnswerListDto>>>
{
    public Guid? AttemptId { get; init; }
    public Guid? QuestionId { get; init; }
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
