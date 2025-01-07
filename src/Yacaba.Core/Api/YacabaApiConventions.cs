using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OData.Results;

namespace Yacaba.Core.Api {
    public static class YacabaApiConventions {

        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public static void Get([ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] params object[] p) { }

        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public static void Post([ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] params object[] p ) {}

        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public static void Put([ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] params object[] p) { }

        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType((Int32)HttpStatusCode.BadRequest)]
        [ProducesResponseType((Int32)HttpStatusCode.InternalServerError)]
        public static void Delete([ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] params object[] p) { }

    }
}
