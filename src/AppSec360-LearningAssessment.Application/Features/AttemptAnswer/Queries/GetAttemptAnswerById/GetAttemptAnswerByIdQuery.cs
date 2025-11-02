using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.AttemptAnswer.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Queries.GetAttemptAnswerById;

public record GetAttemptAnswerByIdQuery(Guid Id) : IRequest<Result<AttemptAnswerDetailDto>>;
