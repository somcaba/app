using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Yacaba.Core.Odata.LinkProvider;

namespace Yacaba.Core.Odata.Serializer {

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class CustomODataResourceSerializer : ODataResourceSerializer {

        private readonly ILinkProviderService? _linkProviderService;

        private readonly String _PROPERTY_RELATION = "rel";
        private readonly String _PROPERTY_LINK = "href";
        private readonly String _PROPERTY_TYPE = "type";
        private readonly String _PROPERTY_PARAMETERIZED = "parameterized";
        private readonly String _PROPERTY_COLLECTION = "Collection";
        private readonly String _ANNOTATION_NAME = "hateoas.links";

        public CustomODataResourceSerializer(
            IODataSerializerProvider provider,
            ILinkProviderService? linkProviderService
        ) : base(provider) {
            _linkProviderService = linkProviderService;
        }

        public override ODataResource CreateResource(SelectExpandNode selectExpandNode, ResourceContext resourceContext) {
            ODataResource resource = base.CreateResource(selectExpandNode, resourceContext);

            IEnumerable<IInstanceLink>? links = _linkProviderService?.GenerateLinks(resource, resourceContext);
            if (links != null && links.Any()) {
                AddLinksToRessource(resource, links);
            }

            //if (resource != null) {
            //    // var resourceContextPropDictionary = GetPropertiesDictionary(resourceContext.ResourceInstance);
            //    Type clrType = MySelectExpandBinder.GetClrType(resourceContext.EdmModel, resourceContext.StructuredType);
            //    PropertyInfo[] properties = clrType.GetProperties();

            //    foreach (ODataPropertyInfo? prop in resource.Properties) {
            //        String extraPropertyName = $"{prop.Name}Name";
            //        PropertyInfo? propertyInfo = properties.FirstOrDefault(c => c.Name.Equals(extraPropertyName, StringComparison.OrdinalIgnoreCase));
            //        if (propertyInfo == null) {
            //            continue;
            //        }

            //        Object value = resourceContext.GetPropertyValue(extraPropertyName);
            //        if (value != null) {
            //            prop.InstanceAnnotations.Add(new ODataInstanceAnnotation("lookup.name", new ODataPrimitiveValue(value)));
            //        } else {
            //            prop.InstanceAnnotations.Add(new ODataInstanceAnnotation("lookup.name", new ODataNullValue()));
            //        }

            //        //var propNameToLower = prop.Name.ToLower();

            //        //if (resourceContextPropDictionary.TryGetValue($"{propNameToLower}name", out object lookupNamePropValue))
            //        //{
            //        //    prop.InstanceAnnotations.Add(new ODataInstanceAnnotation("lookup.name", new ODataPrimitiveValue(lookupNamePropValue)));
            //        //}
            //    }
            //}

            return resource;
        }

        private void AddLinksToRessource(ODataResource resource, IEnumerable<IInstanceLink> links) {
            var items = new List<ODataValue>();
            foreach (IInstanceLink link in links) {
                var typeProperties = new List<ODataProperty> {
                            new ODataProperty { Name = _PROPERTY_RELATION, Value = link.Relation },
                            new ODataProperty { Name = _PROPERTY_LINK, Value = $"{link.Uri}" },
                        };
                if (link.Type != null && link.Type != InstanceLinkType.None) {
                    typeProperties.Add(new ODataProperty { Name = _PROPERTY_TYPE, Value = Enum.Parse<InstanceLinkType>(link.Type.ToString()!.ToLower()) });
                }
                typeProperties.Add(new ODataProperty { Name = _PROPERTY_PARAMETERIZED, Value = link.UriParameterizable });

                var resourceValue = new ODataResourceValue { TypeName = $"{typeof(InstanceLink).FullName}", Properties = typeProperties };
                items.Add(resourceValue);
            }

            var linksCollection = new ODataCollectionValue { TypeName = $"{_PROPERTY_COLLECTION}({typeof(InstanceLink).FullName})", Items = items };
            var instanceAnnotation = new ODataInstanceAnnotation(_ANNOTATION_NAME, linksCollection);
            resource.InstanceAnnotations.Add(instanceAnnotation);
        }

        //private Dictionary<String, Object> GetPropertiesDictionary(Object obj) {
        //    Dictionary<String, Object> propertiesDictionary = [];
        //    PropertyInfo[] properties = obj.GetType().GetProperties();

        //    foreach (PropertyInfo property in properties) {
        //        Object value = property.GetValue(obj);
        //        propertiesDictionary.Add(property.Name.ToLower(), value);
        //    }

        //    return propertiesDictionary;
        //}

        public static Type GetClrType(IEdmModel edmModel, IEdmStructuredType type) {
            ClrTypeAnnotation annotation = edmModel.GetAnnotationValue<ClrTypeAnnotation>(type);
            if (annotation != null) { return annotation.ClrType; }

            throw new InvalidOperationException($"Cannot find the CLR type for {type.FullTypeName()}");
        }

    }
}
