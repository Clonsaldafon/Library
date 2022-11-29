using System;

namespace Library
{
    internal class Library
    {
        public uint[] ReaderIds { get; private set; }
        public uint[] BookIds { get; private set; }
        public DateTime[] DatesTaking { get; private set; }
        public DateTime[] DatesReturn { get; private set; }

        public Library(uint[] readerIds, uint[] bookIds, DateTime[] datesTaking, DateTime[] datesReturn)
        {
            ReaderIds = readerIds;
            BookIds = bookIds;
            DatesTaking = datesTaking;
            DatesReturn = datesReturn;
        }
    }
}