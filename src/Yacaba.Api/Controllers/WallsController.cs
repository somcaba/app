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
    [Route("/api/walls")]
    public class WallsController : ODataController {

        private readonly IMediator _mediator;

        public WallsController(
            IMediator mediator
        ) {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<Wall>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default) {
            var command = new WallGetCollectionQuery();
            IQueryable<Wall> query = await _mediator.Send(command, cancellationToken);
            query = query.AsNoTracking().AsQueryable();
            return Ok(query);
        }

        [HttpGet("{key}")]
        [EnableQuery]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] Int64 key, CancellationToken cancellationToken = default) {
            var command = new WallGetByIdQuery(key);
            Wall? existingWall = await _mediator.Send(command, cancellationToken);
            if (existingWall == null) { return NotFound(); }
            return Ok(existingWall);
        }

        [HttpPost]
        [EnableQuery]
        [ProducesResponseType(typeof(Wall), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] WallCreateRequest request, CancellationToken cancellationToken = default) {
            var command = new WallCreateCommand(request);
            Wall newWall = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { key = newWall.Id }, newWall);
        }

        [HttpPut("{key}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] Int64 key, [FromBody] WallUpdateRequest request, CancellationToken cancellationToken = default) {
            var command = new WallUpdateCommand(key, request);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{key}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Int64 key, CancellationToken cancellationToken = default) {
            var command = new WallDeleteCommand(key);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

    }
}
