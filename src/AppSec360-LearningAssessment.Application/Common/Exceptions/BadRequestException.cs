namespace AppSec360_LearningAssessment.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when a bad request is made.
/// </summary>
public class BadRequestException : Exception
{
    /// <summary>
    /// Initializes a new instance of the BadRequestException class.
    /// </summary>
    public BadRequestException(string message)
        : base(message)
    {
    }
}
