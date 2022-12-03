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
        Readers,
        Records
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string pathOfProject = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            string[] fileNames = new string[] { "books", "authors", "readers", "records" };

            string[] csvPaths = GetPaths(pathOfProject, "Data", fileNames, "data.csv");
            /*string[] jsonPaths = GetPaths(pathOfProject, "Schemas", fileNames, "schema.json");*/

            List<string[]> booksData = CsvDataParser(csvPaths[(int)Table.Books]);
            List<string[]> authorsData = CsvDataParser(csvPaths[(int)Table.Authors]);
            List<string[]> readersData = CsvDataParser(csvPaths[(int)Table.Readers]);
            List<string[]> recordsData = CsvDataParser(csvPaths[(int)Table.Records]);

            List<Book> books = CreateBooksList(booksData);
            List<Author> authors = CreateAuthorsList(authorsData);
            List<Reader> readers = CreateReadersList(readersData);
            List<Record> records = CreateRecordsList(recordsData);

            Database database = new Database(authors, books, readers, records);

            database.UpdateBooksAvailabilityData();

            string data = database.GetData();
            Console.WriteLine(data);
        }

        static string[] GetPaths(string pathOfProject, string folder, string[] fileNames, string type)
        {
            string[] paths = new string[fileNames.Length];

            for (int i = 0; i < fileNames.Length; i++)
            {
                paths[i] = $"{pathOfProject}\\Resources\\{folder}\\{fileNames[i]}-{type}";
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
            List<Book> books = new List<Book>();

            foreach (string[] bookData in booksData)
            {
                uint id = uint.Parse(bookData[0]);
                uint authorId = uint.Parse(bookData[1]);
                string title = bookData[2];
                int yearOfPublication = int.Parse(bookData[3]);
                Dictionary<uint, uint> cabinetAndShelfNumbers = new Dictionary<uint, uint>()
                {
                    { uint.Parse(bookData[4]), uint.Parse(bookData[5]) }
                };
                bool isAvailable = bool.Parse(bookData[6]);

                books.Add(new Book(id, authorId, title, yearOfPublication, cabinetAndShelfNumbers, isAvailable));
            }

            return books;
        }

        static List<Author> CreateAuthorsList(List<string[]> authorsData)
        {
            List<Author> authors = new List<Author>();

            foreach (string[] authorData in authorsData)
            {
                uint id = uint.Parse(authorData[0]);
                string fullName = authorData[1];

                authors.Add(new Author(id, fullName));
            }

            return authors;
        }

        static List<Reader> CreateReadersList(List<string[]> readersData)
        {
            List<Reader> readers = new List<Reader>();

            foreach (string[] readerData in readersData)
            {
                uint id = uint.Parse(readerData[0]);
                string fullName = readerData[1];

                readers.Add(new Reader(id, fullName));
            }

            return readers;
        }

        static List<Record> CreateRecordsList(List<string[]> recordsData)
        {
            List<Record> records = new List<Record>();

            for (int i = 0; i < recordsData.Count; i++)
            {
                uint readerId = uint.Parse(recordsData[i][0]);
                uint bookId = uint.Parse(recordsData[i][1]);
                DateTime dateTaking = DateTime.Parse(recordsData[i][2]);
                DateTime dateReturn = DateTime.Parse(recordsData[i][3]);

                records.Add(new Record(readerId, bookId, dateTaking, dateReturn));
            }

            return records;
        }
    }
}