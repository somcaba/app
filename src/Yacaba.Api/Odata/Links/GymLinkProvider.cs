using Microsoft.AspNetCore.Routing;
using Yacaba.Core.Odata.LinkProvider;
using Yacaba.Domain.Models;

namespace Yacaba.Api.Odata.Links {
    public class GymLinkProvider : ILinkProvider<Gym> {
        public Task<IEnumerable<IInstanceLink>> ExecuteAsync(LinkProviderContext<Gym> linkProviderContext, CancellationToken cancellationToken) {
            var links = new List<IInstanceLink> {
                new DefaultInstanceLink("walls", linkProviderContext.LinkGenerator.GetUriByRouteValues(linkProviderContext.ResourceContext.Request.HttpContext, routeName: "api/walls", values: new RouteValueDictionary {
                    { "$filter", $"gym/id eq {linkProviderContext.Entity.Id}" },
                }, options: new LinkOptions{LowercaseUrls = true, AppendTrailingSlash = false, LowercaseQueryStrings = true}))
            };

            return Task.FromResult(links.AsEnumerable());
        }
    }
}
