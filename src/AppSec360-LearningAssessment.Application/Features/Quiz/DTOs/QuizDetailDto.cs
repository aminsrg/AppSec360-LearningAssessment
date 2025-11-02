using System;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.DTOs;

public class QuizDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool RandomizeQuestions { get; set; }
    public int QuestionCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
