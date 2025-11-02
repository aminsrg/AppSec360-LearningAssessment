using AutoMapper;
using AppSec360_LearningAssessment.Domain.Entities;
using AppSec360_LearningAssessment.Application.Features.Question.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Question.Mappings;

public class QuestionMappingProfile : Profile
{
    public QuestionMappingProfile()
    {
        CreateMap<Domain.Entities.Question, QuestionDetailDto>()
            .ForMember(d => d.AnswerCount, opt => opt.MapFrom(s => s.AnswerIds.Count));

        CreateMap<Domain.Entities.Question, QuestionListDto>();
    }
}
