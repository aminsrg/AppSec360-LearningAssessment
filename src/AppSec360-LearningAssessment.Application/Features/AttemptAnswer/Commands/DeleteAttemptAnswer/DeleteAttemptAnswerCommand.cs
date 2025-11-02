using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.DeleteAttemptAnswer;

public record DeleteAttemptAnswerCommand(Guid Id) : IRequest<Result<Unit>>;
