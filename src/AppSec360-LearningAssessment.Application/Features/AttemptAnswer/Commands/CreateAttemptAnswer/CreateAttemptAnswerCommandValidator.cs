using FluentValidation;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.CreateAttemptAnswer;

public class CreateAttemptAnswerCommandValidator : AbstractValidator<CreateAttemptAnswerCommand>
{
    public CreateAttemptAnswerCommandValidator()
    {
        RuleFor(v => v.QuestionId)
            .NotEmpty().WithMessage("Question Id is required.");

        RuleFor(v => v.AttemptId)
            .NotEmpty().WithMessage("Attempt Id is required.");

        RuleFor(v => v.TextAnswer)
            .MaximumLength(2000).WithMessage("Text answer must not exceed 2000 characters.");
    }
}
