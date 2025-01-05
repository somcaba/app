using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.Routing;
using Microsoft.OData;

namespace Yacaba.Core.Odata.LinkProvider {
    public record class LinkProviderContext<TType> (
        ODataResource Resource,
        ResourceContext ResourceContext,
        TType Entity,
        IServiceProvider ServiceProvider,
        LinkGenerator LinkGenerator
    ) { }
}
