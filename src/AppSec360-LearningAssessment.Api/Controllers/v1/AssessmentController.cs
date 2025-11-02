using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AppSec360_LearningAssessment.Application.Features.Assessment.Commands.CreateAssessment;
using AppSec360_LearningAssessment.Application.Features.Assessment.Commands.UpdateAssessment;
using AppSec360_LearningAssessment.Application.Features.Assessment.Commands.DeleteAssessment;
using AppSec360_LearningAssessment.Application.Features.Assessment.Queries.GetAssessmentById;
using AppSec360_LearningAssessment.Application.Features.Assessment.Queries.GetAssessmentsList;

namespace AppSec360_LearningAssessment.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AssessmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssessmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAssessmentsListQuery query)
    {
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetAssessmentByIdQuery(id));

        if (!result.IsSuccess)
        {
            return NotFound(new { errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssessmentCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAssessmentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new { errors = new[] { "Route Id and command Id must match." } });
        }

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteAssessmentCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return NoContent();
    }
}
