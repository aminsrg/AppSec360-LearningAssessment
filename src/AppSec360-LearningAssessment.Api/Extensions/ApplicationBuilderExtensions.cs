using AppSec360_LearningAssessment.Api.Middleware;

namespace AppSec360_LearningAssessment.Api.Extensions;

/// <summary>
/// Extension methods for IApplicationBuilder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configures the middleware pipeline.
    /// </summary>
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();
        return app;
    }
}
