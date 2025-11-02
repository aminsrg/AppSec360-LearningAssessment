using System;
using System.Collections.Generic;
using AppSec360_LearningAssessment.Domain.Common;
using AppSec360_LearningAssessment.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace AppSec360_LearningAssessment.Domain.Entities;

/// <summary>
/// Represents a question in a quiz
/// </summary>
public class Question : BaseEntity
{
    /// <summary>
    /// Gets or sets the question text
    /// </summary>
    [BsonElement("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the question type
    /// </summary>
    [BsonElement("type")]
    public QuestionType Type { get; set; }

    /// <summary>
    /// Gets or sets the topic
    /// </summary>
    [BsonElement("topic")]
    public string? Topic { get; set; }

    /// <summary>
    /// Gets or sets the maximum points for this question
    /// </summary>
    [BsonElement("maxPoints")]
    public double MaxPoints { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the order index
    /// </summary>
    [BsonElement("orderIndex")]
    public int OrderIndex { get; set; }

    /// <summary>
    /// Gets or sets the quiz ID this question belongs to
    /// </summary>
    [BsonElement("quizId")]
    public Guid QuizId { get; set; }

    /// <summary>
    /// Gets or sets the list of answer IDs for this question
    /// </summary>
    [BsonElement("answerIds")]
    public List<Guid> AnswerIds { get; set; } = new List<Guid>();

    // Navigation properties
    // TODO: Uncomment after creating Answer entity
    // [BsonIgnore]
    // public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    // [BsonIgnore]
    // public Quiz? Quiz { get; set; }
}
