using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.AttemptAnswer.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Queries.GetAttemptAnswerById;

public class GetAttemptAnswerByIdQueryHandler : IRequestHandler<GetAttemptAnswerByIdQuery, Result<AttemptAnswerDetailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAttemptAnswerByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<AttemptAnswerDetailDto>> Handle(GetAttemptAnswerByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.AttemptAnswer>.Filter.And(
            Builders<Domain.Entities.AttemptAnswer>.Filter.Eq(x => x.Id, request.Id),
            Builders<Domain.Entities.AttemptAnswer>.Filter.Eq(x => x.IsDeleted, false)
        );

        var attemptAnswer = await _context.AttemptAnswers.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (attemptAnswer == null)
        {
            return Result<AttemptAnswerDetailDto>.Failure($"AttemptAnswer with Id {request.Id} not found.");
        }

        var dto = _mapper.Map<AttemptAnswerDetailDto>(attemptAnswer);
        return Result<AttemptAnswerDetailDto>.Success(dto);
    }
}
