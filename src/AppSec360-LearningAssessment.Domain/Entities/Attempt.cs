using System;
using System.Collections.Generic;
using AppSec360_LearningAssessment.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace AppSec360_LearningAssessment.Domain.Entities;

/// <summary>
/// Represents a user's attempt at an assessment
/// </summary>
public class Attempt : BaseEntity
{
    /// <summary>
    /// Gets or sets the user ID
    /// </summary>
    [BsonElement("userId")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the assessment ID
    /// </summary>
    [BsonElement("assessmentId")]
    public Guid AssessmentId { get; set; }

    /// <summary>
    /// Gets or sets the start time of the attempt
    /// </summary>
    [BsonElement("startTime")]
    public DateTime StartTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the end time of the attempt
    /// </summary>
    [BsonElement("endTime")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets the score
    /// </summary>
    [BsonElement("score")]
    public double? Score { get; set; }

    /// <summary>
    /// Gets or sets whether the attempt is submitted
    /// </summary>
    [BsonElement("isSubmitted")]
    public bool IsSubmitted { get; set; } = false;

    /// <summary>
    /// Gets or sets the list of attempt answer IDs
    /// </summary>
    [BsonElement("attemptAnswerIds")]
    public List<Guid> AttemptAnswerIds { get; set; } = new List<Guid>();

    // Navigation properties
    // [BsonIgnore]
    // public Assessment? Assessment { get; set; }

    // [BsonIgnore]
    // public ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();
}
