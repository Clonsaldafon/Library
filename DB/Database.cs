using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DB
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

        public string GetData()
        {
            int numberOfColumns = 4;
            string[] columnsNames = new string[] { "Автор", "Название", "Читает", "Взял" };

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

            string columns = GetColumns(numberOfColumns, columnsNames, maxLengths);
            string dataInColumns = GetDataInColumns(authorsFullNames, booksTitles, readersFullNames, datesTaking, maxLengths);

            StringBuilder builder = new StringBuilder();

            builder.Append(columns);
            builder.Append(dataInColumns);

            return builder.ToString();
        }

        private string[] GetAuthorsFullNames()
        {
            string[] fullNames = new string[Authors.Count];

            for (int i = 0; i < Authors.Count; i++)
            {
                fullNames[i] = Authors[i].FullName;
            }

            return fullNames;
        }

        private string[] GetBooksTitles()
        {
            string[] titles = new string[Books.Count];

            for (int i = 0; i < Books.Count; i++)
            {
                titles[i] = Books[i].Title;
            }

            return titles;
        }

        private string[] GetReadersFullNames()
        {
            string[] fullNames = new string[Readers.Count];

            for (int i = 0; i < Readers.Count; i++)
            {
                fullNames[i] = Readers[i].FullName;
            }

            return fullNames;
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

        private string GetColumns(int numberOfColumns, string[] columnsNames, int[] maxLengths)
        {
            StringBuilder builder = new StringBuilder();

            if (numberOfColumns == columnsNames.Length)
            {
                for (int i = 0; i < numberOfColumns; i++)
                {
                    string emptyString = string.Concat(Enumerable.Repeat(" ", Math.Abs(maxLengths[i] - columnsNames[i].Length)));
                    builder.Append($"| {columnsNames[i]}{emptyString} ");
                }
                builder.Append("|\n");

                builder.Append(AddSeparator(numberOfColumns, "-", maxLengths));
            }

            return builder.ToString();
        }

        private string AddSeparator(int numberOfColumns, string separator, int[] maxLengths)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < numberOfColumns; i++)
            {
                builder.Append($"| {string.Concat(Enumerable.Repeat(separator, maxLengths[i]))} ");
            }
            builder.Append("|\n");

            return builder.ToString();
        }

        private string GetDataInColumns(string[] authorsFullNames, string[] booksTitles, string[] readersFullNames,
                                        string[] datesTaking, int[] maxLengths)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < Books.Count; i++)
            {
                uint authorId = GetAuthorIdByBookId(Books[i].AuthorId);
                uint readerId = GetReaderIdByBookId(Books[i].Id);
                int recordIndex = GetRecordIndexByBookId(Books[i].Id);

                builder.Append($"| {authorsFullNames[(int)authorId - 1]}{GetEmptyString(maxLengths[0], authorsFullNames, authorId)} ");
                builder.Append($"| {booksTitles[(int)Books[i].Id - 1]}{GetEmptyString(maxLengths[1], booksTitles, Books[i].Id)} ");

                if (!Books[i].IsAvailable)
                {
                    builder.Append($"| {readersFullNames[(int)readerId - 1]}{GetEmptyString(maxLengths[2], readersFullNames, readerId)} ");
                    builder.Append($"| {datesTaking[recordIndex]}{GetEmptyString(maxLengths[3], datesTaking, readerId)} |\n");
                }
                else
                {
                    builder.Append($"| {GetEmptyString(maxLengths[2])} ");
                    builder.Append($"| {GetEmptyString(maxLengths[3])} |\n");
                }
            }

            return builder.ToString();
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

        private uint GetReaderIdByBookId(uint id)
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

        private int GetRecordIndexByBookId(uint id)
        {
            for (int i = 0; i < Records.Count; i++)
            {
                if (Records[i].BookId == id)
                {
                    return i;
                }
            }

            return -1;
        }

        private string GetEmptyString(int maxLength, string[] data, uint id)
        {
            return $"{string.Concat(Enumerable.Repeat(" ", Math.Abs(maxLength - data[id - 1].Length)))}";
        }

        private string GetEmptyString(int maxLength)
        {
            return $"{string.Concat(Enumerable.Repeat(" ", maxLength))}";
        }

        public void UpdateBooksAvailabilityData()
        {
            foreach (Record record in Records)
            {
                if (record.DateReturn != DateTime.MinValue)
                {
                    Books[(int)record.BookId - 1].SetAvailable(true);
                }
                else
                {
                    Books[(int)record.BookId - 1].SetAvailable(false);
                }
            }
        }
    }
}
