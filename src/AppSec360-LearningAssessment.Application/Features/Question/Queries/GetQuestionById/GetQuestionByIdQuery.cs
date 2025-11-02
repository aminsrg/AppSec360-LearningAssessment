using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;
using AppSec360_LearningAssessment.Application.Features.Question.DTOs;

namespace AppSec360_LearningAssessment.Application.Features.Question.Queries.GetQuestionById;

public record GetQuestionByIdQuery(Guid Id) : IRequest<Result<QuestionDetailDto>>;
