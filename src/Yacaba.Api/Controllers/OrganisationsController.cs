using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Yacaba.Domain.Models;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Controllers {

    public class OrganisationsController : ODataController {

        private static readonly List<Organisation> _datas =
        [
            new Organisation{Id = new OrganisationId(1), Name = "Org 1"},
            new Organisation{Id = new OrganisationId(2), Name = "Org 2"},
        ];
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OrganisationsController(
            IMapper mapper,
            IMediator mediator
        ) {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(PageResult<Organisation>), statusCode: (Int32)HttpStatusCode.OK)]
        public ActionResult<IAsyncEnumerable<Organisation>> Get([FromServices] IOrganisationStore store, CancellationToken cancellationToken = default) {
            //var query = new GetOrganisionCollectionQuery();
            //IAsyncEnumerable<Organisation> stream = _mediator.CreateStream(query, cancellationToken);
            IQueryable<Organisation> query = store.GetAll().AsNoTracking().AsQueryable();
            return Ok(query);
        }

        [HttpGet("{key}")]
        [EnableQuery]
        [ProducesResponseType(typeof(SingleResult<Organisation>), statusCode: (Int32)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: (Int32)HttpStatusCode.NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public IActionResult Get(OrganisationId key, [FromServices] IOrganisationStore store, CancellationToken cancellationToken = default) {
            Organisation? c = store.GetAll().AsNoTracking().AsQueryable().Where(d => d.Id == key).FirstOrDefault();
            if (c == null) { return NotFound(); }
            return Ok(c);
        }

        //[HttpGet]
        //[EnableQuery]
        //[ProducesResponseType(typeof(PageResult<OrganisationDto>), statusCode: (Int32)HttpStatusCode.OK)]
        //public ActionResult<IAsyncEnumerable<OrganisationDto>> Get([FromServices] IOrganisationStore store, CancellationToken cancellationToken = default) {
        //    //var query = new GetOrganisionCollectionQuery();
        //    //IAsyncEnumerable<Organisation> stream = _mediator.CreateStream(query, cancellationToken);
        //    IQueryable<Organisation> query = store.GetAll().AsNoTracking().AsQueryable();
        //    AutoMapper.Extensions.ExpressionMapping.Impl.ISourceInjectedQueryable<OrganisationDto> result = query.UseAsDataSource(_mapper).For<OrganisationDto>();

        //    //ICollection<RequestDTO> requests = await context.Request.GetItemsAsync(mapper, r => r.Id > 0 && r.Id < 3, null, new List<Expression<Func<IQueryable<RequestDTO>, IIncludableQueryable<RequestDTO, object>>>>() { item => item.Include(s => s.Assignee) });
        //    //ICollection<UserDTO> users = await context.User.GetItemsAsync<UserDTO, User>(mapper, u => u.Id > 0 && u.Id < 4, q => q.OrderBy(u => u.Name));
        //    //int count = await context.Request.Query<RequestDTO, Request, int, int>(mapper, q => q.Count(r => r.Id > 1));

        //    return Ok(result);
        //}

        //[HttpGet("{key}")]
        //[EnableQuery]
        //[ProducesResponseType(typeof(SingleResult<OrganisationDto>), statusCode: (Int32)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ProblemDetails), statusCode: (Int32)HttpStatusCode.NotFound)]
        //[ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        //[ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        //public IActionResult Get(Int64 key, [FromServices] IOrganisationStore store, CancellationToken cancellationToken = default) {
        //    var organisationId = (OrganisationId)key;

        //    Organisation? c = store.GetAll().AsNoTracking().AsQueryable().Where(d => d.Id == key).FirstOrDefault();
        //    if (c == null) { return NotFound(); }

        //    OrganisationDto result = _mapper.Map<OrganisationDto>(c);

        //    return Ok(result);
        //}

    }
}
