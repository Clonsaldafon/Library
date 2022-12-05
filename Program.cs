using Library.DB;
using Library.WorkWithSchemas;
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

            string[] csvPaths = ReaderFromFile.GetPaths(pathOfProject, fileNames, "Data", "data.csv");
            string[] jsonPaths = ReaderFromFile.GetPaths(pathOfProject, fileNames, "Schemas", "schema.json");

            List<Schema> schemas = ReaderFromFile.GetSchemas(jsonPaths);

            List<string[]> booksData = ReaderFromFile.DataParser(csvPaths[(int)Table.Books]);
            List<string[]> authorsData = ReaderFromFile.DataParser(csvPaths[(int)Table.Authors]);
            List<string[]> readersData = ReaderFromFile.DataParser(csvPaths[(int)Table.Readers]);
            List<string[]> recordsData = ReaderFromFile.DataParser(csvPaths[(int)Table.Records]);

            try
            {
                bool booksAreValid = JSONSchemaValidator.IsValidToSchema(booksData, schemas[0], $"{fileNames[0]}-schema.json");
                bool authorsAreValid = JSONSchemaValidator.IsValidToSchema(authorsData, schemas[1], $"{fileNames[1]}-schema.json");
                bool readersAreValid = JSONSchemaValidator.IsValidToSchema(readersData, schemas[2], $"{fileNames[2]}-schema.json");
                bool recordsAreValid = JSONSchemaValidator.IsValidToSchema(recordsData, schemas[3], $"{fileNames[3]}-schema.json");

                if (!(booksAreValid && authorsAreValid && readersAreValid && recordsAreValid))
                {
                    throw new FormatException("Format error!");
                }

                List<Author> authors = CreateAuthorsList(authorsData);
                List<Book> books = CreateBooksList(booksData);
                List<Reader> readers = CreateReadersList(readersData);
                List<Record> records = CreateRecordsList(recordsData);

                Database database = new Database(authors, books, readers, records);

                database.UpdateBooksAvailabilityData();

                string data = database.GetData();
                Console.WriteLine(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static List<Author> CreateAuthorsList(List<string[]> authorsData)
        {
            List<Author> authors = new List<Author>();

            for(int i = 1; i < authorsData.Count; i++)
            {
                uint id = uint.Parse(authorsData[i][0]);
                string fullName = authorsData[i][1];

                authors.Add(new Author(id, fullName));
            }

            return authors;
        }

        static List<Book> CreateBooksList(List<string[]> booksData)
        {
            List<Book> books = new List<Book>();
            
            for (int i = 1; i < booksData.Count; i++)
            {
                uint id = uint.Parse(booksData[i][0]);
                uint authorId = uint.Parse(booksData[i][1]);
                string title = booksData[i][2];
                int yearOfPublication = int.Parse(booksData[i][3]);
                uint cabinetNumber = uint.Parse(booksData[i][4]);
                uint shelfNumber = uint.Parse(booksData[i][5]);
                bool isAvailable = bool.Parse(booksData[i][6]);

                books.Add(new Book(id, authorId, title, yearOfPublication, cabinetNumber, shelfNumber, isAvailable));
            }

            return books;
        }

        static List<Reader> CreateReadersList(List<string[]> readersData)
        {
            List<Reader> readers = new List<Reader>();

            for (int i = 1; i < readersData.Count; i++)
            {
                uint id = uint.Parse(readersData[i][0]);
                string fullName = readersData[i][1];

                readers.Add(new Reader(id, fullName));
            }

            return readers;
        }

        static List<Record> CreateRecordsList(List<string[]> recordsData)
        {
            List<Record> records = new List<Record>();

            for (int i = 1; i < recordsData.Count; i++)
            {
                uint readerId = uint.Parse(recordsData[i][0]);
                uint bookId = uint.Parse(recordsData[i][1]);
                DateTime dateTaking = DateTime.Parse(recordsData[i][2]);
                DateTime dateReturn;
                if (recordsData[i].Length == 3)
                {
                    dateReturn = DateTime.MinValue;
                }
                else
                {
                    dateReturn = DateTime.Parse(recordsData[i][3]);
                }


                records.Add(new Record(readerId, bookId, dateTaking, dateReturn));
            }

            return records;
        }
    }
}