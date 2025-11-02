using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Quiz.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Queries.GetQuizzesList;

public record GetQuizzesListQuery : IRequest<Result<PaginatedList<QuizListDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchTerm { get; init; }
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }
}
