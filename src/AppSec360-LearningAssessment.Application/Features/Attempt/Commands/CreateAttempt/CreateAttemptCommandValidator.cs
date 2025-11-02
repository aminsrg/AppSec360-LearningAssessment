using FluentValidation;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Commands.CreateAttempt;

public class CreateAttemptCommandValidator : AbstractValidator<CreateAttemptCommand>
{
    public CreateAttemptCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage("User Id is required.");

        RuleFor(v => v.AssessmentId)
            .NotEmpty().WithMessage("Assessment Id is required.");
    }
}
