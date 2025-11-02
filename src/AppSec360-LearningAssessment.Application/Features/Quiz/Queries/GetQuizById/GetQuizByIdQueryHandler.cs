using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using AppSec360_LearningAssessment.Application.Common.Interfaces;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Quiz.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Queries.GetQuizById;

public class GetQuizByIdQueryHandler : IRequestHandler<GetQuizByIdQuery, Result<QuizDetailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuizByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<QuizDetailDto>> Handle(GetQuizByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Entities.Quiz>.Filter.And(
            Builders<Domain.Entities.Quiz>.Filter.Eq(x => x.Id, request.Id),
            Builders<Domain.Entities.Quiz>.Filter.Eq(x => x.IsDeleted, false)
        );

        var quiz = await _context.Quizzes.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (quiz == null)
        {
            return Result<QuizDetailDto>.Failure($"Quiz with Id {request.Id} not found.");
        }

        var dto = _mapper.Map<QuizDetailDto>(quiz);
        return Result<QuizDetailDto>.Success(dto);
    }
}
