using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Question.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Question.Queries.GetQuestionsList;

public record GetQuestionsListQuery : IRequest<Result<PaginatedList<QuestionListDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchTerm { get; init; }
    public Guid? QuizId { get; init; }
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }
}
