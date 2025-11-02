using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace AppSec360_LearningAssessment.Domain.Entities;

/// <summary>
/// Represents an assessment (quiz with additional assessment-specific properties)
/// </summary>
public class Assessment : Quiz
{
    /// <summary>
    /// Gets or sets the time limit in minutes (0 means no limit)
    /// </summary>
    [BsonElement("timeLimitMinutes")]
    public int TimeLimitMinutes { get; set; } = 0;

    /// <summary>
    /// Gets or sets the maximum number of attempts per user
    /// </summary>
    [BsonElement("maxAttemptsPerUser")]
    public int MaxAttemptsPerUser { get; set; } = 1;

    /// <summary>
    /// Gets or sets the passing score percentage
    /// </summary>
    [BsonElement("passingScorePercent")]
    public double PassingScorePercent { get; set; } = 60.0;

    /// <summary>
    /// Gets or sets the list of attempt IDs for this assessment
    /// </summary>
    [BsonElement("attemptIds")]
    public List<Guid> AttemptIds { get; set; } = new List<Guid>();

    // Navigation properties
    // [BsonIgnore]
    // public ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();
}
