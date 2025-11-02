using System.Linq;
using AutoMapper;
using AppSec360_LearningAssessment.Domain.Entities;
using AppSec360_LearningAssessment.Application.Features.Attempt.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Mappings;

public class AttemptMappingProfile : Profile
{
    public AttemptMappingProfile()
    {
        CreateMap<Domain.Entities.Attempt, AttemptDetailDto>()
            .ForMember(d => d.AnswerCount, opt => opt.MapFrom(s => s.AttemptAnswerIds.Count));

        CreateMap<Domain.Entities.Attempt, AttemptListDto>();
    }
}
