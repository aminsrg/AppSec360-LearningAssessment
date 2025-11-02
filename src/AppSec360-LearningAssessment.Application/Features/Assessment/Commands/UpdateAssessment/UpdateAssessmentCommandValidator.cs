using FluentValidation;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.Commands.UpdateAssessment;

public class UpdateAssessmentCommandValidator : AbstractValidator<UpdateAssessmentCommand>
{
    public UpdateAssessmentCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Assessment Id is required.");

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title must not exceed 255 characters.");

        RuleFor(v => v.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(v => v.TimeLimitMinutes)
            .GreaterThanOrEqualTo(0).WithMessage("Time limit must be 0 or greater.");

        RuleFor(v => v.MaxAttemptsPerUser)
            .GreaterThanOrEqualTo(1).WithMessage("Maximum attempts must be at least 1.");

        RuleFor(v => v.PassingScorePercent)
            .GreaterThanOrEqualTo(0).WithMessage("Passing score must be at least 0.")
            .LessThanOrEqualTo(100).WithMessage("Passing score must be at most 100.");
    }
}
