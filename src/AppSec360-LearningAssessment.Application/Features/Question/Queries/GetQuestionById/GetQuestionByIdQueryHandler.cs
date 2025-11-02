using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Question.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Question.Queries.GetQuestionById;

public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, Result<QuestionDetailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<QuestionDetailDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Question>.Filter.And(
            Builders<Domain.Entities.Question>.Filter.Eq(x => x.Id, request.Id),
            Builders<Domain.Entities.Question>.Filter.Eq(x => x.IsDeleted, false)
        );

        var question = await _context.Questions.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (question == null)
        {
            return Result<QuestionDetailDto>.Failure($"Question with Id {request.Id} not found.");
        }

        var dto = _mapper.Map<QuestionDetailDto>(question);
        return Result<QuestionDetailDto>.Success(dto);
    }
}
