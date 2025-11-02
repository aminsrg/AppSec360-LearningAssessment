using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Attempt.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Queries.GetAttemptsList;

public class GetAttemptsListQueryHandler : IRequestHandler<GetAttemptsListQuery, Result<PaginatedList<AttemptListDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAttemptsListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<AttemptListDto>>> Handle(GetAttemptsListQuery request, CancellationToken cancellationToken)
    {
        // Build filter
        var filterBuilder = Builders<Domain.Entities.Attempt>.Filter;
        var filter = filterBuilder.Eq(x => x.IsDeleted, false);

        // Filter by UserId
        if (!string.IsNullOrWhiteSpace(request.UserId))
        {
            filter = filterBuilder.And(filter, filterBuilder.Eq(x => x.UserId, request.UserId));
        }

        // Filter by AssessmentId
        if (request.AssessmentId.HasValue)
        {
            filter = filterBuilder.And(filter, filterBuilder.Eq(x => x.AssessmentId, request.AssessmentId.Value));
        }

        // Sort
        var sortDefinition = request.SortBy?.ToLower() switch
        {
            "starttime" => request.SortDescending
                ? Builders<Domain.Entities.Attempt>.Sort.Descending(x => x.StartTime)
                : Builders<Domain.Entities.Attempt>.Sort.Ascending(x => x.StartTime),
            "score" => request.SortDescending
                ? Builders<Domain.Entities.Attempt>.Sort.Descending(x => x.Score)
                : Builders<Domain.Entities.Attempt>.Sort.Ascending(x => x.Score),
            _ => Builders<Domain.Entities.Attempt>.Sort.Descending(x => x.StartTime)
        };

        // Get total count
        var totalCount = await _context.Attempts.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        // Get paginated data
        var attempts = await _context.Attempts
            .Find(filter)
            .Sort(sortDefinition)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync(cancellationToken);

        // Map to DTOs
        var dtos = _mapper.Map<List<AttemptListDto>>(attempts);

        // Create paginated list
        var paginatedList = new PaginatedList<AttemptListDto>(
            dtos,
            (int)totalCount,
            request.PageNumber,
            request.PageSize);

        return Result<PaginatedList<AttemptListDto>>.Success(paginatedList);
    }
}
