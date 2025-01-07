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
    [Route("/api/organisations")]
    public class OrganisationsController : ODataController {

        private readonly IMediator _mediator;

        public OrganisationsController(
            IMediator mediator
        ) {
            _mediator = mediator;
        }

        [HttpGet(Name = "Organisation.GetAll")]
        [EnableQuery]
        [ProducesResponseType(typeof(PageResult<Organisation>), statusCode: (Int32)HttpStatusCode.OK)]
        public async Task<ActionResult<IAsyncEnumerable<Organisation>>> Get(CancellationToken cancellationToken = default) {
            var command = new OrganisationGetCollectionQuery();
            IQueryable<Organisation> query = await _mediator.Send(command, cancellationToken);
            query = query.AsNoTracking().AsQueryable();
            return Ok(query);
        }

        [HttpGet("{key}", Name = "Organisation.GetById")]
        [EnableQuery]
        public async Task<ActionResult<SingleResult<Organisation>>> GetById([FromRoute] Int64 key, CancellationToken cancellationToken = default) {
            var command = new OrganisationGetByIdQuery(key);
            Organisation? existingOrganisation = await _mediator.Send(command, cancellationToken);
            if (existingOrganisation == null) { return NotFound(); }
            return Ok(existingOrganisation);
        }

        [HttpPost(Name = "Organisation.Create")]
        public async Task<ActionResult<CreatedAtActionResult>> Post([FromBody] OrganisationCreateRequest request, CancellationToken cancellationToken = default) {
            var command = new OrganisationCreateCommand(request);
            Organisation newOrganisation = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { key = newOrganisation.Id }, newOrganisation);
        }

        [HttpPut("{key}", Name = "Organisation.Update")]
        public async Task<ActionResult<NoContentResult>> Put([FromRoute] Int64 key, [FromBody] OrganisationUpdateRequest request, CancellationToken cancellationToken = default) {
            var command = new OrganisationUpdateCommand(key, request);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{key}", Name = "Organisation.Delete")]
        public async Task<ActionResult<NoContentResult>> Delete([FromRoute] Int64 key, CancellationToken cancellationToken = default) {
            var command = new OrganisationDeleteCommand(key);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

    }
}
