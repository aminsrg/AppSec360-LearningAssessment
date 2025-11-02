using System;
using FluentValidation;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Commands.UpdateAnswer;

public class UpdateAnswerCommandValidator : AbstractValidator<UpdateAnswerCommand>
{
    public UpdateAnswerCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEqual(Guid.Empty).WithMessage("Id is required.");

        RuleFor(v => v.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(500).WithMessage("Text must not exceed 500 characters.");

        RuleFor(v => v.Feedback)
            .MaximumLength(500).WithMessage("Feedback must not exceed 500 characters.");
    }
}
