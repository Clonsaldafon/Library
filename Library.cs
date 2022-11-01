using System;

namespace Library
{
    internal class Library
    {
        public uint ReaderId { get; set; }
        public uint BookId { get; set; }
        public DateTime DateTaking { get; set; }
        public DateTime DateReturn { get; set; }
    }
}