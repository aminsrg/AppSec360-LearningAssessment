using FluentValidation.Results;

namespace AppSec360_LearningAssessment.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when validation failures occur.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; }

    /// <summary>
    /// Initializes a new instance of the ValidationException class.
    /// </summary>
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// Initializes a new instance of the ValidationException class with validation failures.
    /// </summary>
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
