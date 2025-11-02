using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Assessment.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.Queries.GetAssessmentsList;

public class GetAssessmentsListQueryHandler : IRequestHandler<GetAssessmentsListQuery, Result<PaginatedList<AssessmentListDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAssessmentsListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<AssessmentListDto>>> Handle(GetAssessmentsListQuery request, CancellationToken cancellationToken)
    {
        // Build filter
        var filterBuilder = Builders<Domain.Entities.Assessment>.Filter;
        var filter = filterBuilder.Eq(x => x.IsDeleted, false);

        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchFilter = filterBuilder.Or(
                filterBuilder.Regex(x => x.Title, new MongoDB.Bson.BsonRegularExpression(request.SearchTerm, "i")),
                filterBuilder.Regex(x => x.Description, new MongoDB.Bson.BsonRegularExpression(request.SearchTerm, "i"))
            );
            filter = filterBuilder.And(filter, searchFilter);
        }

        // Sort
        var sortDefinition = request.SortBy?.ToLower() switch
        {
            "title" => request.SortDescending
                ? Builders<Domain.Entities.Assessment>.Sort.Descending(x => x.Title)
                : Builders<Domain.Entities.Assessment>.Sort.Ascending(x => x.Title),
            "createdat" => request.SortDescending
                ? Builders<Domain.Entities.Assessment>.Sort.Descending(x => x.CreatedAt)
                : Builders<Domain.Entities.Assessment>.Sort.Ascending(x => x.CreatedAt),
            _ => Builders<Domain.Entities.Assessment>.Sort.Descending(x => x.CreatedAt)
        };

        // Get total count
        var totalCount = await _context.Assessments.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        // Get paginated data
        var assessments = await _context.Assessments
            .Find(filter)
            .Sort(sortDefinition)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync(cancellationToken);

        // Map to DTOs
        var dtos = _mapper.Map<List<AssessmentListDto>>(assessments);

        // Create paginated list
        var paginatedList = new PaginatedList<AssessmentListDto>(
            dtos,
            (int)totalCount,
            request.PageNumber,
            request.PageSize);

        return Result<PaginatedList<AssessmentListDto>>.Success(paginatedList);
    }
}
