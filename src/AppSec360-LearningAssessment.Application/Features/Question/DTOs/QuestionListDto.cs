using System;
using AppSec360_LearningAssessment.Domain.Enums;

namespace AppSec360_LearningAssessment.Application.Features.Question.DTOs;

public class QuestionListDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public QuestionType Type { get; set; }
    public int OrderIndex { get; set; }
    public double MaxPoints { get; set; }
    public DateTime CreatedAt { get; set; }
}
