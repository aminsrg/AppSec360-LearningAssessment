using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Attempt.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Queries.GetAttemptById;

public class GetAttemptByIdQueryHandler : IRequestHandler<GetAttemptByIdQuery, Result<AttemptDetailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAttemptByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<AttemptDetailDto>> Handle(GetAttemptByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Attempt>.Filter.And(
            Builders<Domain.Entities.Attempt>.Filter.Eq(x => x.Id, request.Id),
            Builders<Domain.Entities.Attempt>.Filter.Eq(x => x.IsDeleted, false)
        );

        var attempt = await _context.Attempts.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (attempt == null)
        {
            return Result<AttemptDetailDto>.Failure($"Attempt with Id {request.Id} not found.");
        }

        var dto = _mapper.Map<AttemptDetailDto>(attempt);
        return Result<AttemptDetailDto>.Success(dto);
    }
}
