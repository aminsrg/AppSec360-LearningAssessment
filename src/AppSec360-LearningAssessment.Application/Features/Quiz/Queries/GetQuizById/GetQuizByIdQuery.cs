using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Quiz.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Queries.GetQuizById;

public record GetQuizByIdQuery(Guid Id) : IRequest<Result<QuizDetailDto>>;
