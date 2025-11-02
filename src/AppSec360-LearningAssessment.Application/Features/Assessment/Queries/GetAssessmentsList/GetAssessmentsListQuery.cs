using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Assessment.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.Queries.GetAssessmentsList;

public record GetAssessmentsListQuery : IRequest<Result<PaginatedList<AssessmentListDto>>>
{
    public string? SearchTerm { get; init; }
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
