using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Question.Commands.DeleteQuestion;

public record DeleteQuestionCommand(Guid Id) : IRequest<Result<Unit>>;
