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
            string[] authorsFullNames = GetAuthorsFullNames();
            string[] booksTitles = GetBooksTitles();
            string[] readersFullNames = GetReadersFullNames();
            string[] datesTaking = GetDatesTaking();

            int[] maxLengths = new int[]
            {
                GetMaxLength(authorsFullNames),
                GetMaxLength(booksTitles),
                GetMaxLength(readersFullNames),
                GetMaxLength(datesTaking)
            };

            Console.Write($"| Автор{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxLengths[0] - "Автор".Length)))} ");
            Console.Write($"| Название{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxLengths[1] - "Название".Length)))} ");
            Console.Write($"| Читает{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxLengths[2] - "Читает".Length)))} ");
            Console.WriteLine($"| Взял{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxLengths[3] - "Взял".Length)))} |");
            Console.Write($"| {string.Concat(Enumerable.Repeat("-", maxLengths[0]))} ");
            Console.Write($"| {string.Concat(Enumerable.Repeat("-", maxLengths[1]))} ");
            Console.Write($"| {string.Concat(Enumerable.Repeat("-", maxLengths[2]))} ");
            Console.WriteLine($"| {string.Concat(Enumerable.Repeat("-", maxLengths[3]))} |");

            for (int i = 0; i < Books.Count; i++)
            {
                uint authorId = GetAuthorIdByBookId(Books[i].AuthorId);
                uint readerId = GetRecordReaderIdByBookId(Books[i].Id);

                Console.Write($"| {authorsFullNames[authorId - 1]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxLengths[0] - authorsFullNames[authorId - 1].Length)))} ");
                Console.Write($"| {booksTitles[i]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxLengths[1] - booksTitles[i].Length)))} ");

                if (readerId > 0 && readersFullNames.Contains(Readers[(int)readerId].FullName))
                {
                    Console.Write($"| {readersFullNames[readerId - 1]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxLengths[2] - readersFullNames[readerId - 1].Length)))} ");
                    Console.WriteLine($"| {datesTaking[readerId - 1]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxLengths[3] - datesTaking[readerId - 1].Length)))} |");
                }
                else
                {
                    Console.Write($"| {string.Concat(Enumerable.Repeat(" ", maxLengths[2]))} ");
                    Console.WriteLine($"| {string.Concat(Enumerable.Repeat(" ", maxLengths[3]))} |");
                }
            }
        }

        private string[] GetAuthorsFullNames()
        {
            string[] strings = new string[Authors.Count];

            for (int i = 0; i < Authors.Count; i++)
            {
                strings[i] = Authors[i].FullName;
            }

            return strings;
        }

        private string[] GetBooksTitles()
        {
            string[] strings = new string[Books.Count];

            for (int i = 0; i < Books.Count; i++)
            {
                strings[i] = Books[i].Title;
            }

            return strings;
        }

        private string[] GetReadersFullNames()
        {
            string[] strings = new string[Records.Count];

            for (int i = 0; i < Records.Count; i++)
            {
                strings[i] = Readers[(int)Records[i].ReaderId - 1].FullName;
            }

            return strings;
        }

        private string[] GetDatesTaking()
        {
            string[] strings = new string[Records.Count];

            for (int i = 0; i < Records.Count; i++)
            {
                strings[i] = Records[i].DateTaking.ToString();
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

        private int GetMaxLength(string[] data)
        {
            int maxStringLength = int.MinValue;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Length > maxStringLength)
                {
                    maxStringLength = data[i].Length;
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
