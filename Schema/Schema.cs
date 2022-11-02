using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library.Schema
{
    internal class Schema
    {
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "columns")]
        public List<Element> Elements = new List<Element>();
    }
}