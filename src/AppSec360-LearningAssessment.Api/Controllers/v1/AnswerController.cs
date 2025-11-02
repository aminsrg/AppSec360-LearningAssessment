using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AppSec360_LearningAssessment.Application.Features.Answer.Commands.CreateAnswer;
using AppSec360_LearningAssessment.Application.Features.Answer.Commands.UpdateAnswer;
using AppSec360_LearningAssessment.Application.Features.Answer.Commands.DeleteAnswer;
using AppSec360_LearningAssessment.Application.Features.Answer.Queries.GetAnswerById;
using AppSec360_LearningAssessment.Application.Features.Answer.Queries.GetAnswersList;

namespace AppSec360_LearningAssessment.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AnswerController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnswerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAnswersListQuery query)
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
        var result = await _mediator.Send(new GetAnswerByIdQuery(id));

        if (!result.IsSuccess)
        {
            return NotFound(new { errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAnswerCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAnswerCommand command)
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
        var result = await _mediator.Send(new DeleteAnswerCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return NoContent();
    }
}
