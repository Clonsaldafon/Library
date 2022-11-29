using System;

namespace Library
{
    internal class Library
    {
        public uint ReaderId { get; private set; }
        public uint BookId { get; private set; }
        public DateTime DateTaking { get; private set; }
        public DateTime DateReturn { get; private set; }

        public Library(uint readerId, uint bookId, DateTime dateTaking, DateTime dateReturn)
        {
            ReaderId = readerId;
            BookId = bookId;
            DateTaking = dateTaking;
            DateReturn = dateReturn;
        }
    }
}