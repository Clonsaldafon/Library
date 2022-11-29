using System;

namespace Library
{
    internal class Cabinet
    {
        public uint Id { get; private set; }
        public uint[] ShelfNumbers { get; private set; }

        public Cabinet(uint id, uint[] shelfNumbers)
        {
            Id = id;
            ShelfNumbers = shelfNumbers;
        }
    }
}