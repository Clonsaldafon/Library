using System;

namespace Library
{
    internal class Reader
    {
        public uint Id { get; private set; }
        public string FullName { get; private set; }

        public Reader(uint id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }
    }
}