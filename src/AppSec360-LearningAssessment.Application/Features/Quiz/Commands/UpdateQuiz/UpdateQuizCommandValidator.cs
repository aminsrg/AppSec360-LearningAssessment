using System;
using FluentValidation;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.Commands.UpdateQuiz;

public class UpdateQuizCommandValidator : AbstractValidator<UpdateQuizCommand>
{
    public UpdateQuizCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEqual(Guid.Empty).WithMessage("Id is required.");

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title must not exceed 255 characters.");

        RuleFor(v => v.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
