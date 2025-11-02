using System;
using System.Collections.Generic;
using AppSec360_LearningAssessment.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace AppSec360_LearningAssessment.Domain.Entities;

/// <summary>
/// Represents a user's answer to a question in an attempt
/// </summary>
public class AttemptAnswer : BaseEntity
{
    /// <summary>
    /// Gets or sets the question ID
    /// </summary>
    [BsonElement("questionId")]
    public Guid QuestionId { get; set; }

    /// <summary>
    /// Gets or sets the attempt ID
    /// </summary>
    [BsonElement("attemptId")]
    public Guid AttemptId { get; set; }

    /// <summary>
    /// Gets or sets the selected answer IDs (for multiple choice questions)
    /// </summary>
    [BsonElement("selectedAnswerIds")]
    public List<Guid> SelectedAnswerIds { get; set; } = new List<Guid>();

    /// <summary>
    /// Gets or sets the text answer (for text-based questions)
    /// </summary>
    [BsonElement("textAnswer")]
    public string? TextAnswer { get; set; }

    // Navigation properties
    // [BsonIgnore]
    // public Attempt? Attempt { get; set; }

    // [BsonIgnore]
    // public Question? Question { get; set; }
}
