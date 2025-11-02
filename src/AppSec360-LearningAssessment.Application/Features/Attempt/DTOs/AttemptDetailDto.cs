using System;

namespace AppSec360_LearningAssessment.Application.Features.Attempt.DTOs;

public class AttemptDetailDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid AssessmentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public double? Score { get; set; }
    public bool IsSubmitted { get; set; }
    public int AnswerCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
