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
    [Route("api/organisations")]
    public class OrganisationsController : ODataController {

        private readonly IMediator _mediator;

        public OrganisationsController(
            IMediator mediator
        ) {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(PageResult<Organisation>), statusCode: (Int32)HttpStatusCode.OK)]
        public async Task<ActionResult<IAsyncEnumerable<Organisation>>> GetAsync(CancellationToken cancellationToken = default) {
            var command = new OrganisionGetCollectionQuery();
            IQueryable<Organisation> query = await _mediator.Send(command, cancellationToken);
            query = query.AsNoTracking().AsQueryable();
            return Ok(query);
        }

        [HttpGet("{key}")]
        [EnableQuery]
        [ProducesResponseType(typeof(SingleResult<Organisation>), statusCode: (Int32)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: (Int32)HttpStatusCode.NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Int64 key, CancellationToken cancellationToken = default) {
            var command = new OrganisionGetByIdQuery(key);
            Organisation? existingOrganisation = await _mediator.Send(command, cancellationToken);
            if (existingOrganisation == null) { return NotFound(); }
            return Ok(existingOrganisation);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedAtActionResult), statusCode: (Int32)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: (Int32)HttpStatusCode.NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateAsync(OrganisationCreateRequest request, CancellationToken cancellationToken = default) {
            var command = new OrganisationCreateCommand(request);
            Organisation newOrganisation = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction("GetById", new { key = newOrganisation.Id }, newOrganisation);
        }

        [HttpPut("{key}")]
        [ProducesResponseType((Int32)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: (Int32)HttpStatusCode.NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateAsync(Int64 key, OrganisationUpdateRequest request, CancellationToken cancellationToken = default) {
            var command = new OrganisationUpdateCommand(key, request);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{key}")]
        [ProducesResponseType((Int32)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: (Int32)HttpStatusCode.NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Int64 key, CancellationToken cancellationToken = default) {
            var command = new OrganisationDeleteCommand(key);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

    }
}
