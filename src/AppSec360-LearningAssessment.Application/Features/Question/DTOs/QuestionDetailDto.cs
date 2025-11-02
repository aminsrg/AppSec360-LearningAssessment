using System;
using AppSec360_LearningAssessment.Domain.Enums;

namespace AppSec360_LearningAssessment.Application.Features.Question.DTOs;

public class QuestionDetailDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public QuestionType Type { get; set; }
    public string? Topic { get; set; }
    public double MaxPoints { get; set; }
    public int OrderIndex { get; set; }
    public Guid QuizId { get; set; }
    public int AnswerCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
