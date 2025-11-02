using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Attempt.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Queries.GetAttemptById;

public record GetAttemptByIdQuery(Guid Id) : IRequest<Result<AttemptDetailDto>>;
