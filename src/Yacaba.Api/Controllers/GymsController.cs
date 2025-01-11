using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Yacaba.Api.Application.Commands;
using Yacaba.Api.Application.Queries;
using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;

namespace Yacaba.Api.Controllers {

    [ApiController]
    [Route("/api/gyms")]
    public class GymsController : ODataController {

        private readonly IMediator _mediator;

        public GymsController(
            IMediator mediator
        ) {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<Gym>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default) {
            var command = new GymGetCollectionQuery();
            IQueryable<Gym> query = await _mediator.Send(command, cancellationToken);
            query = query.AsNoTracking().AsQueryable();
            return Ok(query);
        }

        [HttpGet("{key}")]
        [EnableQuery]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] Int64 key, CancellationToken cancellationToken = default) {
            var command = new GymGetByIdQuery(key);
            Gym? existingGym = await _mediator.Send(command, cancellationToken);
            if (existingGym == null) { return NotFound(); }
            return Ok(existingGym);
        }

        [HttpPost]
        [EnableQuery]
        [ProducesResponseType(typeof(Gym), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] GymCreateRequest request, CancellationToken cancellationToken = default) {
            var command = new GymCreateCommand(request);
            Gym newGym = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { key = newGym.Id }, newGym);
        }

        [HttpPut("{key}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] Int64 key, [FromBody] GymUpdateRequest request, CancellationToken cancellationToken = default) {
            var command = new GymUpdateCommand(key, request);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{key}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Int64 key, CancellationToken cancellationToken = default) {
            var command = new GymDeleteCommand(key);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

    }
}
