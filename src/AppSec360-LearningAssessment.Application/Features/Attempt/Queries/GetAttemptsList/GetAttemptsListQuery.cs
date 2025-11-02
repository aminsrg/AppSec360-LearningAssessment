using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Attempt.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Queries.GetAttemptsList;

public record GetAttemptsListQuery : IRequest<Result<PaginatedList<AttemptListDto>>>
{
    public string? UserId { get; init; }
    public Guid? AssessmentId { get; init; }
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
