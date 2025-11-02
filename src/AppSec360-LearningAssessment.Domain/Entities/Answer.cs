using System;
using AppSec360_LearningAssessment.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace AppSec360_LearningAssessment.Domain.Entities;

/// <summary>
/// Represents an answer to a question
/// </summary>
public class Answer : BaseEntity
{
    /// <summary>
    /// Gets or sets the answer text
    /// </summary>
    [BsonElement("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether this is a correct answer
    /// </summary>
    [BsonElement("isCorrect")]
    public bool IsCorrect { get; set; }

    /// <summary>
    /// Gets or sets the feedback for this answer
    /// </summary>
    [BsonElement("feedback")]
    public string? Feedback { get; set; }

    /// <summary>
    /// Gets or sets the order index
    /// </summary>
    [BsonElement("orderIndex")]
    public int OrderIndex { get; set; }

    /// <summary>
    /// Gets or sets the question ID this answer belongs to
    /// </summary>
    [BsonElement("questionId")]
    public Guid QuestionId { get; set; }

    // Navigation properties
    // [BsonIgnore]
    // public Question? Question { get; set; }
}
