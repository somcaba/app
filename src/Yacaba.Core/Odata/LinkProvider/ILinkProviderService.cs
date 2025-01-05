using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.OData;

namespace Yacaba.Core.Odata.LinkProvider {
    public interface ILinkProviderService {

        IEnumerable<IInstanceLink>? GenerateLinks(ODataResource resource, ResourceContext resourceContext);

    }
}
