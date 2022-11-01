using System;

namespace Library
{
    internal class Reader
    {
        public uint Id { get; set; }
        public string FullName { get; set; }

        public Reader(uint id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }
    }
}