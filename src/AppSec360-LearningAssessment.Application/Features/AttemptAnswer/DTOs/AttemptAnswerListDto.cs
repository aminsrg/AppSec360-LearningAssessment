using System;
using System.Collections.Generic;

namespace AppSec360_LearningAssessment.Application.Features.AttemptAnswer.DTOs;

public class AttemptAnswerListDto
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public Guid AttemptId { get; set; }
    public List<Guid> SelectedAnswerIds { get; set; } = new List<Guid>();
    public string? TextAnswer { get; set; }
}
