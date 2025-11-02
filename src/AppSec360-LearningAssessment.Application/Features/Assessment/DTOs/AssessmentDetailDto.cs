using System;

namespace AppSec360_LearningAssessment.Application.Features.Assessment.DTOs;

public class AssessmentDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool RandomizeQuestions { get; set; }
    public int TimeLimitMinutes { get; set; }
    public int MaxAttemptsPerUser { get; set; }
    public double PassingScorePercent { get; set; }
    public int QuestionCount { get; set; }
    public int AttemptCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
