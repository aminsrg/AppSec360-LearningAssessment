using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Question.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Question.Queries.GetQuestionsList;

public class GetQuestionsListQueryHandler : IRequestHandler<GetQuestionsListQuery, Result<PaginatedList<QuestionListDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionsListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<QuestionListDto>>> Handle(GetQuestionsListQuery request, CancellationToken cancellationToken)
    {
        var filterBuilder = Builders<Domain.Entities.Question>.Filter;
        var filter = filterBuilder.Eq(x => x.IsDeleted, false);

        // Filter by QuizId if provided
        if (request.QuizId.HasValue)
        {
            filter = filterBuilder.And(filter, filterBuilder.Eq(x => x.QuizId, request.QuizId.Value));
        }

        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchFilter = filterBuilder.Or(
                filterBuilder.Regex(x => x.Text, new MongoDB.Bson.BsonRegularExpression(request.SearchTerm, "i")),
                filterBuilder.Regex(x => x.Topic, new MongoDB.Bson.BsonRegularExpression(request.SearchTerm, "i"))
            );
            filter = filterBuilder.And(filter, searchFilter);
        }

        // Sort
        var sortDefinition = request.SortBy?.ToLower() switch
        {
            "orderindex" => request.SortDescending
                ? Builders<Domain.Entities.Question>.Sort.Descending(x => x.OrderIndex)
                : Builders<Domain.Entities.Question>.Sort.Ascending(x => x.OrderIndex),
            "text" => request.SortDescending
                ? Builders<Domain.Entities.Question>.Sort.Descending(x => x.Text)
                : Builders<Domain.Entities.Question>.Sort.Ascending(x => x.Text),
            "maxpoints" => request.SortDescending
                ? Builders<Domain.Entities.Question>.Sort.Descending(x => x.MaxPoints)
                : Builders<Domain.Entities.Question>.Sort.Ascending(x => x.MaxPoints),
            _ => Builders<Domain.Entities.Question>.Sort.Ascending(x => x.OrderIndex)
        };

        var totalCount = await _context.Questions.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        var questions = await _context.Questions
            .Find(filter)
            .Sort(sortDefinition)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<QuestionListDto>>(questions);

        var paginatedList = new PaginatedList<QuestionListDto>(
            dtos,
            (int)totalCount,
            request.PageNumber,
            request.PageSize);

        return Result<PaginatedList<QuestionListDto>>.Success(paginatedList);
    }
}
