using System.Text.Json;
using System.Text.Json.Serialization;

namespace Yacaba.Core.Odata.Json {
    public class CamelCaseJsonStringEnumConverter : JsonStringEnumConverter {
        public CamelCaseJsonStringEnumConverter() : base(JsonNamingPolicy.CamelCase, true) { }
    }
}
