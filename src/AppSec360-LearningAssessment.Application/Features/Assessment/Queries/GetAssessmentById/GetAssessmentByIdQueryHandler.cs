using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Assessment.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.Queries.GetAssessmentById;

public class GetAssessmentByIdQueryHandler : IRequestHandler<GetAssessmentByIdQuery, Result<AssessmentDetailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAssessmentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<AssessmentDetailDto>> Handle(GetAssessmentByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Assessment>.Filter.And(
            Builders<Domain.Entities.Assessment>.Filter.Eq(x => x.Id, request.Id),
            Builders<Domain.Entities.Assessment>.Filter.Eq(x => x.IsDeleted, false)
        );

        var assessment = await _context.Assessments.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (assessment == null)
        {
            return Result<AssessmentDetailDto>.Failure($"Assessment with Id {request.Id} not found.");
        }

        var dto = _mapper.Map<AssessmentDetailDto>(assessment);
        return Result<AssessmentDetailDto>.Success(dto);
    }
}
