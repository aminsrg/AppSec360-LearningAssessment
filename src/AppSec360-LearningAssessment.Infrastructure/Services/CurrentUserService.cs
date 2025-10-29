using AppSec360_LearningAssessment.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AppSec360_LearningAssessment.Infrastructure.Services;

/// <summary>
/// Service for accessing the current user context.
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Gets the current user's ID from the X-User-Id header.
    /// </summary>
    public string? UserId => _httpContextAccessor.HttpContext?.Request.Headers["X-User-Id"].FirstOrDefault();
}
