using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    internal class Database
    {
        public List<Author> Authors { get; private set; }
        public List<Book> Books { get; private set; }
        public List<Reader> Readers { get; private set; }
        public List<Record> Records { get; private set; }

        public Database(List<Author> authors, List<Book> books, List<Reader> readers, List<Record> records)
        {
            Authors = authors;
            Books = books;
            Readers = readers;
            Records = records;
        }

        public void WriteData()
        {
            string[] authorsStrings = GetStrings(Authors);
            string[] booksStrings = GetStrings(Books);
            string[] readersStrings = GetStrings(Records, true);
            string[] datesTakingStrings = GetStrings(Records, false);

            int[] maxStringsLengths = new int[]
            {
                GetMaxStringLength(authorsStrings),
                GetMaxStringLength(booksStrings),
                GetMaxStringLength(readersStrings),
                GetMaxStringLength(datesTakingStrings)
            };

            Console.Write($"| Автор{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[0] - "Автор".Length)))} ");
            Console.Write($"| Название{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[1] - "Название".Length)))} ");
            Console.Write($"| Читает{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[2] - "Читает".Length)))} ");
            Console.WriteLine($"| Взял{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[3] - "Взял".Length)))} |");
            Console.Write($"| {string.Concat(Enumerable.Repeat("-", maxStringsLengths[0]))} ");
            Console.Write($"| {string.Concat(Enumerable.Repeat("-", maxStringsLengths[1]))} ");
            Console.Write($"| {string.Concat(Enumerable.Repeat("-", maxStringsLengths[2]))} ");
            Console.WriteLine($"| {string.Concat(Enumerable.Repeat("-", maxStringsLengths[3]))} |");

            for (int i = 0; i < Books.Count; i++)
            {
                uint authorId = GetAuthorIdByBookId(Books[i].AuthorId);
                uint readerId = GetRecordReaderIdByBookId(Books[i].Id);

                Console.Write($"| {authorsStrings[authorId - 1]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[0] - authorsStrings[authorId - 1].Length)))} ");
                Console.Write($"| {booksStrings[i]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[1] - booksStrings[i].Length)))} ");

                if (readerId > 0 && readersStrings.Contains(Readers[(int)readerId].FullName))
                {
                    Console.Write($"| {readersStrings[readerId - 1]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[2] - readersStrings[readerId - 1].Length)))} ");
                    Console.WriteLine($"| {datesTakingStrings[readerId - 1]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[3] - datesTakingStrings[readerId - 1].Length)))} |");
                }
                else
                {
                    Console.Write($"| {string.Concat(Enumerable.Repeat(" ", maxStringsLengths[2]))} ");
                    Console.WriteLine($"| {string.Concat(Enumerable.Repeat(" ", maxStringsLengths[3]))} |");
                }
            }
        }

        private string[] GetStrings(List<Author> authors)
        {
            string[] strings = new string[authors.Count];

            for (int i = 0; i < authors.Count; i++)
            {
                strings[i] = authors[i].FullName;
            }

            return strings;
        }

        private string[] GetStrings(List<Book> books)
        {
            string[] strings = new string[books.Count];

            for (int i = 0; i < books.Count; i++)
            {
                strings[i] = books[i].Title;
            }

            return strings;
        }

        private string[] GetStrings(List<Record> records, bool isReaders)
        {
            string[] strings = new string[records.Count];

            if (isReaders)
            {
                for (int i = 0; i < records.Count; i++)
                {
                    strings[i] = Readers[(int)records[i].ReaderId - 1].FullName;
                }
            }
            else
            {
                for (int i = 0; i < records.Count; i++)
                {
                    strings[i] = records[i].DateTaking.ToString();
                }
            }


            return strings;
        }

        private uint GetAuthorIdByBookId(uint id)
        {
            foreach (Author author in Authors)
            {
                if (author.Id == id)
                {
                    return author.Id;
                }
            }

            return uint.MinValue;
        }

        private uint GetRecordReaderIdByBookId(uint id)
        {
            foreach (Record record in Records)
            {
                if (record.BookId == id)
                {
                    return record.ReaderId;
                }
            }

            return uint.MinValue;
        }

        private int GetMaxStringLength(string[] strings)
        {
            int maxStringLength = int.MinValue;

            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i].Length > maxStringLength)
                {
                    maxStringLength = strings[i].Length;
                }
            }

            return maxStringLength;
        }

        public void UpdateBooksAvailabilityData()
        {
            foreach (Record record in Records)
            {
                if (DateTime.Compare(record.DateReturn, DateTime.Now) < 0)
                {
                    Books[(int)record.BookId - 1].SetAvailable(true);
                }
            }
        }
    }
}
