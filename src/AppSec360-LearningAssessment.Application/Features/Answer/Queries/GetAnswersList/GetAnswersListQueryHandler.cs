using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Answer.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Queries.GetAnswersList;

public class GetAnswersListQueryHandler : IRequestHandler<GetAnswersListQuery, Result<PaginatedList<AnswerListDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAnswersListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<AnswerListDto>>> Handle(GetAnswersListQuery request, CancellationToken cancellationToken)
    {
        var filterBuilder = Builders<Domain.Entities.Answer>.Filter;
        var filter = filterBuilder.Eq(x => x.IsDeleted, false);

        // Filter by QuestionId if provided
        if (request.QuestionId.HasValue)
        {
            filter = filterBuilder.And(filter, filterBuilder.Eq(x => x.QuestionId, request.QuestionId.Value));
        }

        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchFilter = filterBuilder.Or(
                filterBuilder.Regex(x => x.Text, new MongoDB.Bson.BsonRegularExpression(request.SearchTerm, "i")),
                filterBuilder.Regex(x => x.Feedback, new MongoDB.Bson.BsonRegularExpression(request.SearchTerm, "i"))
            );
            filter = filterBuilder.And(filter, searchFilter);
        }

        // Sort
        var sortDefinition = request.SortBy?.ToLower() switch
        {
            "orderindex" => request.SortDescending
                ? Builders<Domain.Entities.Answer>.Sort.Descending(x => x.OrderIndex)
                : Builders<Domain.Entities.Answer>.Sort.Ascending(x => x.OrderIndex),
            "text" => request.SortDescending
                ? Builders<Domain.Entities.Answer>.Sort.Descending(x => x.Text)
                : Builders<Domain.Entities.Answer>.Sort.Ascending(x => x.Text),
            "iscorrect" => request.SortDescending
                ? Builders<Domain.Entities.Answer>.Sort.Descending(x => x.IsCorrect)
                : Builders<Domain.Entities.Answer>.Sort.Ascending(x => x.IsCorrect),
            _ => Builders<Domain.Entities.Answer>.Sort.Ascending(x => x.OrderIndex)
        };

        var totalCount = await _context.Answers.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        var answers = await _context.Answers
            .Find(filter)
            .Sort(sortDefinition)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<AnswerListDto>>(answers);

        var paginatedList = new PaginatedList<AnswerListDto>(
            dtos,
            (int)totalCount,
            request.PageNumber,
            request.PageSize);

        return Result<PaginatedList<AnswerListDto>>.Success(paginatedList);
    }
}
