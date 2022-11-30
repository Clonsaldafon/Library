using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int numberOfColumns = 4;

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
                Console.WriteLine($"{Books[i].AuthorId} {Array.IndexOf(Authors.ToArray(), Books[i].AuthorId)}");
                /*Console.Write($"| {authorsStrings[i]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[0] - authorsStrings[Array.IndexOf(Authors.ToArray(), Books[i].AuthorId)].Length)))} ");
                Console.Write($"| {booksStrings[i]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[1] - booksStrings[i].Length)))} ");
                Console.Write($"| {readersStrings[i]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[2] - readersStrings[i].Length)))} ");
                Console.WriteLine($"| {datesTakingStrings[i]}{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxStringsLengths[3] - datesTakingStrings[i].Length)))} |");*/
            }
            //Console.WriteLine(string.Format("{{0, -{0}}}|", GetMaxStringLength(strings)));
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
    }
}
