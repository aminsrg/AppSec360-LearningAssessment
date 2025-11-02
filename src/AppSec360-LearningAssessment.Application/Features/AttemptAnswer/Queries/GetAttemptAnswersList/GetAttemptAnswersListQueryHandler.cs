using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.AttemptAnswer.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Queries.GetAttemptAnswersList;

public class GetAttemptAnswersListQueryHandler : IRequestHandler<GetAttemptAnswersListQuery, Result<PaginatedList<AttemptAnswerListDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAttemptAnswersListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<AttemptAnswerListDto>>> Handle(GetAttemptAnswersListQuery request, CancellationToken cancellationToken)
    {
        // Build filter
        var filterBuilder = Builders<Domain.Entities.AttemptAnswer>.Filter;
        var filter = filterBuilder.Eq(x => x.IsDeleted, false);

        // Filter by AttemptId
        if (request.AttemptId.HasValue)
        {
            filter = filterBuilder.And(filter, filterBuilder.Eq(x => x.AttemptId, request.AttemptId.Value));
        }

        // Filter by QuestionId
        if (request.QuestionId.HasValue)
        {
            filter = filterBuilder.And(filter, filterBuilder.Eq(x => x.QuestionId, request.QuestionId.Value));
        }

        // Sort
        var sortDefinition = request.SortBy?.ToLower() switch
        {
            "createdat" => request.SortDescending
                ? Builders<Domain.Entities.AttemptAnswer>.Sort.Descending(x => x.CreatedAt)
                : Builders<Domain.Entities.AttemptAnswer>.Sort.Ascending(x => x.CreatedAt),
            _ => Builders<Domain.Entities.AttemptAnswer>.Sort.Descending(x => x.CreatedAt)
        };

        // Get total count
        var totalCount = await _context.AttemptAnswers.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        // Get paginated data
        var attemptAnswers = await _context.AttemptAnswers
            .Find(filter)
            .Sort(sortDefinition)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync(cancellationToken);

        // Map to DTOs
        var dtos = _mapper.Map<List<AttemptAnswerListDto>>(attemptAnswers);

        // Create paginated list
        var paginatedList = new PaginatedList<AttemptAnswerListDto>(
            dtos,
            (int)totalCount,
            request.PageNumber,
            request.PageSize);

        return Result<PaginatedList<AttemptAnswerListDto>>.Success(paginatedList);
    }
}
