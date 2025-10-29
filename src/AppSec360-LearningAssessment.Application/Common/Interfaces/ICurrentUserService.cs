namespace AppSec360_LearningAssessment.Application.Common.Interfaces;

/// <summary>
/// Provides access to the current user context from API Gateway.
/// The gateway authenticates requests and forwards user ID via X-User-Id header.
/// Used for audit trail (CreatedBy, UpdatedBy, DeletedBy fields).
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the current user's unique identifier from X-User-Id header.
    /// Returns null if header is not present.
    /// </summary>
    string? UserId { get; }
}
