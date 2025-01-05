namespace Yacaba.Core.Odata.LinkProvider {

    public interface IInstanceLink {
        String Relation { get; }
        String? Uri { get; }
        InstanceLinkType? Type { get; }
        Boolean UriParameterizable { get; }
    }

    public record DefaultInstanceLink(String Relation, String? Uri, InstanceLinkType? Type = null, Boolean UriParameterizable = false) : IInstanceLink { }

    public record ParameterizedInstanceLink : DefaultInstanceLink {
        public ParameterizedInstanceLink(String Relation, String? Uri, Dictionary<Int32, String> ParameterMapping, InstanceLinkType? Type = null) : base(Relation, TransformUriToParameterizedUri(Uri, ParameterMapping), Type, true) { }

        private static String? TransformUriToParameterizedUri(String? uri, Dictionary<Int32, String> parameterMapping) {
            if (uri == null) { return null; }
            String result = uri;
            foreach (KeyValuePair<Int32, String> mapping in parameterMapping) {
                result = result.Replace($"{mapping.Value}={mapping.Key}", $"{{{mapping.Value}}}", StringComparison.InvariantCultureIgnoreCase);
            }
            return result;
        }

    }

    public record InstanceLink (
        String Name,
        String Uri,
        InstanceLinkType Type = InstanceLinkType.Get
    ) { }


    public enum InstanceLinkType {
        None,
        Get,
        Post,
        Put,
        Delete,
        Patch
    }

}
