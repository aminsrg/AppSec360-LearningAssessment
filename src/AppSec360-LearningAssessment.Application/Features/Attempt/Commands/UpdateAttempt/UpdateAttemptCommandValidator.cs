using FluentValidation;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.Commands.UpdateAttempt;

public class UpdateAttemptCommandValidator : AbstractValidator<UpdateAttemptCommand>
{
    public UpdateAttemptCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Attempt Id is required.");

        RuleFor(v => v.Score)
            .GreaterThanOrEqualTo(0).When(v => v.Score.HasValue)
            .WithMessage("Score must be 0 or greater.");
    }
}
