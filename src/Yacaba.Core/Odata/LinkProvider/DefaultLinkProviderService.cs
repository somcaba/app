using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.OData;

namespace Yacaba.Core.Odata.LinkProvider {
    public class DefaultLinkProviderService : ILinkProviderService {

        //private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<ILinkProvider> _linkProviders;

        public DefaultLinkProviderService(
            //IServiceProvider serviceProvider,
            IEnumerable<ILinkProvider> linkProviders
        ) {
            //_serviceProvider = serviceProvider;
            _linkProviders = linkProviders;
        }

        public IEnumerable<IInstanceLink>? GenerateLinks(ODataResource resource, ResourceContext resourceContext) {
            Type type = resourceContext.ResourceInstance.GetType();
            ILinkProvider? linkProvider = GetLinkProviderFromType(type);
            if (linkProvider == null) { return null; }
            return linkProvider.ExecuteAsync(resource, resourceContext, cancellationToken: resourceContext.Request.HttpContext.RequestAborted).GetAwaiter().GetResult();
        }

        private ILinkProvider? GetLinkProviderFromType(Type type) {
            ILinkProvider? linkProvider = _linkProviders.SingleOrDefault(p => p.Type == type);
            return linkProvider;
        }

    }
}
