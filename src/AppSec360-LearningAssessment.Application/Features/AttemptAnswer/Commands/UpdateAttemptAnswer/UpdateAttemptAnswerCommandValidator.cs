using FluentValidation;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.UpdateAttemptAnswer;

public class UpdateAttemptAnswerCommandValidator : AbstractValidator<UpdateAttemptAnswerCommand>
{
    public UpdateAttemptAnswerCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("AttemptAnswer Id is required.");

        RuleFor(v => v.TextAnswer)
            .MaximumLength(2000).WithMessage("Text answer must not exceed 2000 characters.");
    }
}
