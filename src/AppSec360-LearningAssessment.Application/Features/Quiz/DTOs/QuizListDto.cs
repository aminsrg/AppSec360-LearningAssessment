using System;

namespace AppSec360_LearningAssessment.Application.Features.Quiz.DTOs;

public class QuizListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int QuestionCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
