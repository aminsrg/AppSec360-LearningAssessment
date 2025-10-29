using AppSec360_LearningAssessment.Application.Common.Interfaces;

namespace AppSec360_LearningAssessment.Infrastructure.Services;

/// <summary>
/// Service for date and time operations.
/// </summary>
public class DateTimeService : IDateTime
{
    /// <summary>
    /// Gets the current local date and time.
    /// </summary>
    public DateTime Now => DateTime.Now;

    /// <summary>
    /// Gets the current UTC date and time.
    /// </summary>
    public DateTime UtcNow => DateTime.UtcNow;
}
