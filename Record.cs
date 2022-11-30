using System;

namespace Library
{
    internal class Record
    {
        public uint ReaderId { get; private set; }
        public uint BookId { get; private set; }
        public DateOnly DateTaking { get; private set; }
        public DateOnly DateReturn { get; private set; }

        public Record(uint readerId, uint bookId, DateOnly dateTaking, DateOnly dateReturn)
        {
            ReaderId = readerId;
            BookId = bookId;
            DateTaking = dateTaking;
            DateReturn = dateReturn;
        }
    }
}