using System.Linq;
using AutoMapper;
using AppSec360_LearningAssessment.Domain.Entities;
using AppSec360_LearningAssessment.Application.Features.Assessment.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.Mappings;

public class AssessmentMappingProfile : Profile
{
    public AssessmentMappingProfile()
    {
        CreateMap<Domain.Entities.Assessment, AssessmentDetailDto>()
            .ForMember(d => d.QuestionCount, opt => opt.MapFrom(s => s.QuestionIds.Count))
            .ForMember(d => d.AttemptCount, opt => opt.MapFrom(s => s.AttemptIds.Count));

        CreateMap<Domain.Entities.Assessment, AssessmentListDto>()
            .ForMember(d => d.QuestionCount, opt => opt.MapFrom(s => s.QuestionIds.Count))
            .ForMember(d => d.AttemptCount, opt => opt.MapFrom(s => s.AttemptIds.Count));
    }
}
