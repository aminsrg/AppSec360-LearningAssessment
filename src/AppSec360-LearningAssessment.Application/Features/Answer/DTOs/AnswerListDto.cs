using System;

namespace AppSec360_LearningAssessment.Application.Features.Answer.DTOs;

public class AnswerListDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public int OrderIndex { get; set; }
    public DateTime CreatedAt { get; set; }
}
