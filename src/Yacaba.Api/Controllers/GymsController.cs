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
        public async Task<ActionResult<PageResult<Gym>>> Get(CancellationToken cancellationToken = default) {
            var command = new GymGetCollectionQuery();
            IQueryable<Gym> query = await _mediator.Send(command, cancellationToken);
            query = query.AsNoTracking().AsQueryable();
            return Ok(query);
        }

        [HttpGet("{key}")]
        [EnableQuery]
        public async Task<ActionResult<Gym>> GetById([FromRoute] Int64 key, CancellationToken cancellationToken = default) {
            var command = new GymGetByIdQuery(key);
            Gym? existingGym = await _mediator.Send(command, cancellationToken);
            if (existingGym == null) { return NotFound(); }
            return Ok(existingGym);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedAtActionResult>> Post([FromBody] GymCreateRequest request, CancellationToken cancellationToken = default) {
            var command = new GymCreateCommand(request);
            Gym newGym = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { key = newGym.Id }, newGym);
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<NoContentResult>> Put([FromRoute] Int64 key, [FromBody] GymUpdateRequest request, CancellationToken cancellationToken = default) {
            var command = new GymUpdateCommand(key, request);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{key}")]
        public async Task<ActionResult<NoContentResult>> Delete([FromRoute] Int64 key, CancellationToken cancellationToken = default) {
            var command = new GymDeleteCommand(key);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

    }
}
