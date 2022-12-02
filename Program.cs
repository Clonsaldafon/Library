using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    enum Table
    {
        Books,
        Authors,
        Cabinets,
        Readers,
        Records,
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string pathOfProject = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            string[] fileNames = new string[] { "book", "author", "cabinet", "reader", "library" };

            string[] csvPaths = GetPaths(pathOfProject, "Data", fileNames, "data.csv");
            /*string[] jsonPaths = GetPaths(pathOfProject, "Schemes", fileNames, "schema.json");*/

            List<string[]> booksData = CsvDataParser(csvPaths[(int)Table.Books]);
            List<string[]> authorsData = CsvDataParser(csvPaths[(int)Table.Authors]);
            List<string[]> readersData = CsvDataParser(csvPaths[(int)Table.Readers]);
            List<string[]> recordsData = CsvDataParser(csvPaths[(int)Table.Records]);

            List<Book> books = CreateBooksList(booksData);
            List<Author> authors = CreateAuthorsList(authorsData);
            List<Reader> readers = CreateReadersList(readersData);
            List<Record> records = CreateRecordsList(recordsData);

            Database database = new Database(authors, books, readers, records);

            database.WriteData();
        }

        static string[] GetPaths(string pathOfProject, string folder, string[] fileNames, string type)
        {
            string[] paths = new string[fileNames.Length];

            for (int i = 0; i < fileNames.Length; i++)
            {
                paths[i] = $"{pathOfProject}\\Resources\\{folder}\\{fileNames[i]}.{type}";
            }

            return paths;
        }

        static List<string[]> CsvDataParser(string pathToCsvFile)
        {
            List<string[]> data = new List<string[]>();

            using (TextFieldParser parser = new TextFieldParser(pathToCsvFile))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    data.Add(fields);
                }
            }

            return data;
        }

        static List<Book> CreateBooksList(List<string[]> booksData)
        {
            Console.WriteLine(string.Join(", ", booksData[0]));
            List<Book> books = new List<Book>();

            foreach (string[] bookData in booksData)
            {
                books.Add(
                    new Book(
                        uint.Parse(bookData[0]),
                        uint.Parse(bookData[1]),
                        bookData[2],
                        int.Parse(bookData[3]),
                        new Dictionary<uint, uint>()
                        {
                            { uint.Parse(bookData[4]), uint.Parse(bookData[5]) },
                        },
                        bool.Parse(bookData[6])
                        )
                    );
            }

            return books;
        }

        static List<Author> CreateAuthorsList(List<string[]> authorsData)
        {
            List<Author> authors = new List<Author>();

            foreach (string[] authorData in authorsData)
            {
                authors.Add(
                    new Author(
                        uint.Parse(authorData[0]),
                        authorData[1]
                        )
                    );
            }

            return authors;
        }

        static List<Reader> CreateReadersList(List<string[]> readersData)
        {
            List<Reader> readers = new List<Reader>();

            foreach (string[] readerData in readersData)
            {
                readers.Add(
                    new Reader(
                        uint.Parse(readerData[0]),
                        readerData[1]
                        )
                    );
            }

            return readers;
        }

        static List<Record> CreateRecordsList(List<string[]> recordsData)
        {
            List<Record> records = new List<Record>();

            for (int i = 0; i < recordsData.Count; i++)
            {
                records.Add(
                    new Record(
                        uint.Parse(recordsData[i][0]),
                        uint.Parse(recordsData[i][1]),
                        DateTime.Parse(recordsData[i][2]),
                        DateTime.Parse(recordsData[i][3])
                        )
                    );
            }

            return records;
        }
    }
}