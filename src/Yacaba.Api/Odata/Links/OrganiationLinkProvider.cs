using Microsoft.AspNetCore.Routing;
using Yacaba.Core.Odata.LinkProvider;
using Yacaba.Domain.Models;

namespace Yacaba.Api.Odata.Links {
    public class OrganiationLinkProvider : ILinkProvider<Organisation> {
        public Task<IEnumerable<IInstanceLink>> ExecuteAsync(LinkProviderContext<Organisation> linkProviderContext, CancellationToken cancellationToken) {
            var links = new List<IInstanceLink> {
                new DefaultInstanceLink("gyms", linkProviderContext.LinkGenerator.GetUriByAction(linkProviderContext.ResourceContext.Request.HttpContext, action: "Get", controller: "gyms", values: new RouteValueDictionary {
                    { "$filter", $"organisation/id eq {linkProviderContext.Entity.Id}" },
                }, options: new LinkOptions{LowercaseUrls = true, AppendTrailingSlash = false, LowercaseQueryStrings = true}))
            };

            return Task.FromResult(links.AsEnumerable());
        }
    }
}
