using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Answer.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Queries.GetAnswerById;

public class GetAnswerByIdQueryHandler : IRequestHandler<GetAnswerByIdQuery, Result<AnswerDetailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAnswerByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<AnswerDetailDto>> Handle(GetAnswerByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Answer>.Filter.And(
            Builders<Domain.Entities.Answer>.Filter.Eq(x => x.Id, request.Id),
            Builders<Domain.Entities.Answer>.Filter.Eq(x => x.IsDeleted, false)
        );

        var answer = await _context.Answers.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (answer == null)
        {
            return Result<AnswerDetailDto>.Failure($"Answer with Id {request.Id} not found.");
        }

        var dto = _mapper.Map<AnswerDetailDto>(answer);
        return Result<AnswerDetailDto>.Success(dto);
    }
}
