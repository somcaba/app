
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Yacaba.Core.Odata.LinkProvider {
    public interface ILinkProvider {

        Type Type { get; }
        Task<IEnumerable<IInstanceLink>> ExecuteAsync(ODataResource resource, ResourceContext resourceContext, CancellationToken cancellationToken);

    }

    public interface ILinkProvider<TType> : ILinkProvider {

        Type ILinkProvider.Type => typeof(TType);
        Task<IEnumerable<IInstanceLink>> ILinkProvider.ExecuteAsync(ODataResource resource, ResourceContext resourceContext, CancellationToken cancellationToken) =>
            ExecuteAsync(new LinkProviderContext<TType>(
                Resource: resource,
                ResourceContext: resourceContext,
                Entity: (TType)resourceContext.ResourceInstance,
                ServiceProvider: resourceContext.Request.HttpContext.RequestServices,
                LinkGenerator: resourceContext.Request.HttpContext.RequestServices.GetRequiredService<LinkGenerator>()
            ), cancellationToken: cancellationToken);

        Task<IEnumerable<IInstanceLink>> ExecuteAsync(LinkProviderContext<TType> linkProviderContext, CancellationToken cancellationToken);

    }

}
