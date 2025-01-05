using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Yacaba.Core.Odata.LinkProvider;

namespace Yacaba.Core.Odata.Serializer {
    public class CustomODataSerializerProvider : ODataSerializerProvider {

        //private IServiceProvider _rootProvider;
        private readonly ILinkProviderService? _linkProviderService;

        public CustomODataSerializerProvider(IServiceProvider rootContainer) : base(rootContainer) {
            //_rootProvider = rootContainer;
            _linkProviderService = rootContainer.GetService<ILinkProviderService>();
        }

        public override IODataEdmTypeSerializer GetEdmTypeSerializer(IEdmTypeReference edmType) {
            if (edmType.Definition.TypeKind == EdmTypeKind.Entity) {
                return new CustomODataResourceSerializer(this, _linkProviderService);
            }

            return base.GetEdmTypeSerializer(edmType);
        }
    }
}
