using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Assessment.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.Queries.GetAssessmentById;

public record GetAssessmentByIdQuery(Guid Id) : IRequest<Result<AssessmentDetailDto>>;
