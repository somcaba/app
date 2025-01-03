using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacaba.Api.Controllers {
    internal class _not_workng {
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
