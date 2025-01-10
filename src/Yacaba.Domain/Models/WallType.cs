using System.Text.Json.Serialization;
using Yacaba.Core.Odata.Json;

namespace Yacaba.Domain.Models {

    [JsonConverter(typeof(CamelCaseJsonStringEnumConverter))]
    public enum WallType {
        Bloc = 1,
        Lead = 2
    }
}
