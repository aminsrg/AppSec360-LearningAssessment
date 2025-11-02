using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Answer.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Queries.GetAnswersList;

public record GetAnswersListQuery : IRequest<Result<PaginatedList<AnswerListDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchTerm { get; init; }
    public Guid? QuestionId { get; init; }
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }
}
