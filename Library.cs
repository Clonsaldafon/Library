using System;

namespace Library
{
    internal class Library
    {
        public uint[] ReaderIds { get; private set; }
        public uint[] BookIds { get; private set; }
        public DateOnly[] DatesTaking { get; private set; }
        public DateOnly[] DatesReturn { get; private set; }

        public Library(uint[] readerIds, uint[] bookIds, DateOnly[] datesTaking, DateOnly[] datesReturn)
        {
            ReaderIds = readerIds;
            BookIds = bookIds;
            DatesTaking = datesTaking;
            DatesReturn = datesReturn;
        }
    }
}