using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Library.WorkWithSchemas
{
    internal class Schema
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "columns")]
        public List<SchemaElement> Elements = new List<SchemaElement>();
    }
}
