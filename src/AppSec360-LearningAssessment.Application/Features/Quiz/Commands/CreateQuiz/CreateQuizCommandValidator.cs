using FluentValidation;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Commands.CreateQuiz;

public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
{
    public CreateQuizCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title must not exceed 255 characters.");

        RuleFor(v => v.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
