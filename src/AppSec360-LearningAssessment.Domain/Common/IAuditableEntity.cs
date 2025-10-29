namespace AppSec360_LearningAssessment.Domain.Common;

/// <summary>
/// Interface for entities that require audit trail tracking.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets when the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets who created the entity.
    /// </summary>
    string CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets when the entity was last updated.
    /// </summary>
    DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets who last updated the entity.
    /// </summary>
    string? UpdatedBy { get; set; }
}
