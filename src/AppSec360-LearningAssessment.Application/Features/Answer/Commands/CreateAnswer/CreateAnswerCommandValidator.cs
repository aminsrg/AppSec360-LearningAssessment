using System;
using FluentValidation;

namespace AppSec360_LearningAssessment.Application.Features.Answer.Commands.CreateAnswer;

public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
{
    public CreateAnswerCommandValidator()
    {
        RuleFor(v => v.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(500).WithMessage("Text must not exceed 500 characters.");

        RuleFor(v => v.Feedback)
            .MaximumLength(500).WithMessage("Feedback must not exceed 500 characters.");

        RuleFor(v => v.QuestionId)
            .NotEqual(Guid.Empty).WithMessage("QuestionId is required.");
    }
}
