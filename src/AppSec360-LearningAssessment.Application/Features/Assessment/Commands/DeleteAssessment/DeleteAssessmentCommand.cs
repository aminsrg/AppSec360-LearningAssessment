using System;
using MediatR;
using AppSec360_LearningAssessment.Application.Common.Models;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.Commands.DeleteAssessment;

public record DeleteAssessmentCommand(Guid Id) : IRequest<Result<Unit>>;
