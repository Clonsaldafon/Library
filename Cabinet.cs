using System;

namespace Library
{
    internal class Cabinet
    {
        public uint Id { get; set; }
        public uint[] ShelfNumbers { get; set; }

        public Cabinet(uint id, uint[] shelfNumbers)
        {
            Id = id;
            ShelfNumbers = shelfNumbers;
        }
    }
}