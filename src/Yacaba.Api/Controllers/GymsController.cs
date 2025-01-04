using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
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
        [ProducesResponseType(typeof(PageResult<Gym>), statusCode: (Int32)HttpStatusCode.OK)]
        public async Task<ActionResult<IAsyncEnumerable<Gym>>> Get(CancellationToken cancellationToken = default) {
            var command = new GymGetCollectionQuery();
            IQueryable<Gym> query = await _mediator.Send(command, cancellationToken);
            query = query.AsNoTracking().AsQueryable();
            return Ok(query);
        }

        [HttpGet("{key}")]
        [EnableQuery]
        [ProducesResponseType(typeof(SingleResult<Gym>), statusCode: (Int32)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: (Int32)HttpStatusCode.NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] Int64 key, CancellationToken cancellationToken = default) {
            var command = new GymGetByIdQuery(key);
            Gym? existingGym = await _mediator.Send(command, cancellationToken);
            if (existingGym == null) { return NotFound(); }
            return Ok(existingGym);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedAtActionResult), statusCode: (Int32)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: (Int32)HttpStatusCode.NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] GymCreateRequest request, CancellationToken cancellationToken = default) {
            var command = new GymCreateCommand(request);
            Gym newGym = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { key = newGym.Id }, newGym);
        }

        [HttpPut("{key}")]
        [ProducesResponseType((Int32)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: (Int32)HttpStatusCode.NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] Int64 key, [FromBody] GymUpdateRequest request, CancellationToken cancellationToken = default) {
            var command = new GymUpdateCommand(key, request);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{key}")]
        [ProducesResponseType((Int32)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: (Int32)HttpStatusCode.NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Int64 key, CancellationToken cancellationToken = default) {
            var command = new GymDeleteCommand(key);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

    }
}
