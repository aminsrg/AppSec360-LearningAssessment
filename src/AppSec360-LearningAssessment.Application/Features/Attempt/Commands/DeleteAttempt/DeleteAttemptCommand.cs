using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Commands.DeleteAttempt;

public record DeleteAttemptCommand(Guid Id) : IRequest<Result<Unit>>;
