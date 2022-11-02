using System;

namespace Library
{
    internal class Author
    {
        public uint Id { get; set; }
        public string FullName { get; set; }

        public Author(uint id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }
    }
}