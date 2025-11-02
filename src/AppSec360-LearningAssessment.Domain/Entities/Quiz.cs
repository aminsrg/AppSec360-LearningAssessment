using System;
using System.Collections.Generic;
using AppSec360_LearningAssessment.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace AppSec360_LearningAssessment.Domain.Entities;

/// <summary>
/// Represents a quiz with questions
/// </summary>
public class Quiz : BaseEntity
{
    /// <summary>
    /// Gets or sets the quiz title
    /// </summary>
    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quiz description
    /// </summary>
    [BsonElement("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets whether questions should be randomized
    /// </summary>
    [BsonElement("randomizeQuestions")]
    public bool RandomizeQuestions { get; set; } = false;

    /// <summary>
    /// Gets or sets the list of question IDs for this quiz
    /// </summary>
    [BsonElement("questionIds")]
    public List<Guid> QuestionIds { get; set; } = new List<Guid>();

    // Navigation properties
    // TODO: Uncomment after creating Question entity
    // [BsonIgnore]
    // public ICollection<Question> Questions { get; set; } = new List<Question>();
}
