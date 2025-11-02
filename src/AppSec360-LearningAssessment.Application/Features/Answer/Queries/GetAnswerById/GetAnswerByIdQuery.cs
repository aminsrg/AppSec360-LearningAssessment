using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Answer.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Queries.GetAnswerById;

public record GetAnswerByIdQuery(Guid Id) : IRequest<Result<AnswerDetailDto>>;
