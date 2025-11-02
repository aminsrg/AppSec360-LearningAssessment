using System.Linq;
using AutoMapper;
using AppSec360_LearningAssessment.Domain.Entities;
using AppSec360_LearningAssessment.Application.Features.Quiz.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Mappings;

public class QuizMappingProfile : Profile
{
    public QuizMappingProfile()
    {
        CreateMap<Domain.Entities.Quiz, QuizDetailDto>()
            .ForMember(d => d.QuestionCount, opt => opt.MapFrom(s => s.QuestionIds.Count));

        CreateMap<Domain.Entities.Quiz, QuizListDto>()
            .ForMember(d => d.QuestionCount, opt => opt.MapFrom(s => s.QuestionIds.Count));
    }
}
