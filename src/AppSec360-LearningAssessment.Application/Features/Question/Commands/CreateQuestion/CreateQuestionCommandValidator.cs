using System;
using FluentValidation;

namespace AppSec360_LearningAssessment.Application.Features.Question.Commands.CreateQuestion;

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(v => v.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(1000).WithMessage("Text must not exceed 1000 characters.");

        RuleFor(v => v.Topic)
            .MaximumLength(255).WithMessage("Topic must not exceed 255 characters.");

        RuleFor(v => v.MaxPoints)
            .GreaterThan(0).WithMessage("MaxPoints must be greater than 0.");

        RuleFor(v => v.QuizId)
            .NotEqual(Guid.Empty).WithMessage("QuizId is required.");
    }
}
