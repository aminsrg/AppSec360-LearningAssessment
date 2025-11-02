using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.CreateAttemptAnswer;
using AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.UpdateAttemptAnswer;
using AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Commands.DeleteAttemptAnswer;
using AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Queries.GetAttemptAnswerById;
using AppSec360_LearningAssessment.Application.Features.AttemptAnswer.Queries.GetAttemptAnswersList;

namespace AppSec360_LearningAssessment.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AttemptAnswerController : ControllerBase
{
    private readonly IMediator _mediator;

    public AttemptAnswerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAttemptAnswersListQuery query)
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
        var result = await _mediator.Send(new GetAttemptAnswerByIdQuery(id));

        if (!result.IsSuccess)
        {
            return NotFound(new { errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAttemptAnswerCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAttemptAnswerCommand command)
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
        var result = await _mediator.Send(new DeleteAttemptAnswerCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return NoContent();
    }
}
