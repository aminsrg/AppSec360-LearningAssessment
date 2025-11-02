using AutoMapper;
using AppSec360_LearningAssessment.Domain.Entities;
using AppSec360_LearningAssessment.Application.Features.Answer.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Mappings;

public class AnswerMappingProfile : Profile
{
    public AnswerMappingProfile()
    {
        CreateMap<Domain.Entities.Answer, AnswerDetailDto>();
        CreateMap<Domain.Entities.Answer, AnswerListDto>();
    }
}
