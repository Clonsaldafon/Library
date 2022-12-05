using System;

namespace Library.DB
{
    internal class Record
    {
        public uint ReaderId { get; private set; }
        public uint BookId { get; private set; }
        public DateTime DateTaking { get; private set; }
        public DateTime DateReturn { get; private set; }

        public Record(uint readerId, uint bookId, DateTime dateTaking, DateTime dateReturn)
        {
            ReaderId = readerId;
            BookId = bookId;
            DateTaking = dateTaking;
            DateReturn = dateReturn;
        }
    }
}