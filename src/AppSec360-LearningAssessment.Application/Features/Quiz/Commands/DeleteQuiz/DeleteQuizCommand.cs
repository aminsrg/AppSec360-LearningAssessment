using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Commands.DeleteQuiz;

public record DeleteQuizCommand(Guid Id) : IRequest<Result<Unit>>;
