using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Commands.DeleteAnswer;

public record DeleteAnswerCommand(Guid Id) : IRequest<Result<Unit>>;
