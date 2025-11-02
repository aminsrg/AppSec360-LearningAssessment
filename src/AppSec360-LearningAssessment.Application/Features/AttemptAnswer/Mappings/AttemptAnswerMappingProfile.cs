using System.Linq;
using AutoMapper;
using AppSec360_LearningAssessment.Domain.Entities;
using AppSec360_LearningAssessment.Application.Features.AttemptAnswer.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Mappings;

public class AttemptAnswerMappingProfile : Profile
{
    public AttemptAnswerMappingProfile()
    {
        CreateMap<Domain.Entities.AttemptAnswer, AttemptAnswerDetailDto>();

        CreateMap<Domain.Entities.AttemptAnswer, AttemptAnswerListDto>();
    }
}
