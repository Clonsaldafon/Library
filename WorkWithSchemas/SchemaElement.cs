using Newtonsoft.Json;

namespace Library.WorkWithSchemas
{
    internal class SchemaElement
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; private set; }

        [JsonProperty(PropertyName = "referencedTable")]
        public string ReferencedTable { get; private set; }

        [JsonProperty(PropertyName = "isPrimary")]
        public bool IsPrimary { get; private set; }
    }
}
