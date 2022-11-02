using System;
using Newtonsoft.Json;

namespace Library.Schema
{
    internal class Element
    {
        public string Name { get; private set; }

        public string Type { get; private set; }

        public string ReferencedTable { get; private set; }

        public bool IsPrimary { get; private set; }
    }
}