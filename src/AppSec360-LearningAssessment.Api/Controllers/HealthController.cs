using Microsoft.AspNetCore.Mvc;

namespace AppSec360_LearningAssessment.Api.Controllers;

/// <summary>
/// Health check controller.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Gets the health status of the service.
    /// </summary>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "Healthy",
            timestamp = DateTime.UtcNow,
            service = "AppSec360-LearningAssessment"
        });
    }
}
