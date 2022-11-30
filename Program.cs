using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Library
{
    enum Table
    {
        Book,
        Author,
        Cabinet,
        Reader,
        Library,
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string pathOfProject = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            string[] fileNames = new string[] { "book", "author", "cabinet", "reader", "library" };

            string[] csvPaths = GetPaths(pathOfProject, "Data", fileNames, "data.csv");
            string[] jsonPaths = GetPaths(pathOfProject, "Schemes", fileNames, "schema.json");

            List<string[]> booksData = CsvDataParser(csvPaths[(int)Table.Book]);
            List<string[]> authorsData = CsvDataParser(csvPaths[(int)Table.Author]);
            List<string[]> cabinetsData = CsvDataParser(csvPaths[(int)Table.Cabinet]);
            List<string[]> readersData = CsvDataParser(csvPaths[(int)Table.Reader]);
            List<string[]> libraryData = CsvDataParser(csvPaths[(int)Table.Library]);

            if (booksData is null || readersData is null)
            {
                return;
            }

            List<Book> books = CreateBooksList(booksData);
            List<Author> authors = CreateAuthorsList(authorsData);
            List<Cabinet> cabinets = CreateCabinetsList(cabinetsData);
            List<Reader> readers = CreateReadersList(readersData);

            Library library = CreateLibrary(booksData);
            library = AddDataFromLibraryData(library, libraryData);

            books = CheckTheAvailabilityOfBooks(library, books);

            for (int i = 0; i < library.BookIds.Length; i++)
            {
                Console.WriteLine($"{library.ReaderIds[i]} {library.BookIds[i]} {library.DatesTaking[i]} {library.DatesReturn[i]}");
            }
            Console.WriteLine();

            Console.WriteLine("Книги в наличии");
            for (int i = 0; i < library.BookIds.Length; i++)
            {
                if (books[i].IsAvailable || library.ReaderIds[i] == uint.MinValue)
                {
                    Console.WriteLine($"Автор: {authors[(int)books[i].AuthorId - 1].FullName} | Название: {books[i].Title}");
                }
            }
            Console.WriteLine();

            Console.WriteLine("Книги на руках");
            for (int i = 0; i < library.BookIds.Length; i++)
            {
                if (books[i].IsAvailable && library.DatesReturn[i] != DateOnly.MinValue)
                {
                    Console.WriteLine($"Автор: {authors[(int)books[i].AuthorId].FullName} | Название: {books[i].Title} | Читает: {readers[(int)library.ReaderIds[i]].FullName} | Взял: {library.DatesTaking[i]}");
                }
            }
            Console.WriteLine();
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
            List<Book> books = new List<Book>();

            foreach (string[] bookData in booksData)
            {
                books.Add(
                    new Book(uint.Parse(bookData[0]), uint.Parse(bookData[1]), uint.Parse(bookData[2]), uint.Parse(bookData[3]),
                        bookData[4], int.Parse(bookData[5]), bool.Parse(bookData[6])
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
                authors.Add(new Author(uint.Parse(authorData[0]), authorData[1]));
            }

            return authors;
        }

        static List<Cabinet> CreateCabinetsList(List<string[]> cabinetsData)
        {
            List<Cabinet> cabinets= new List<Cabinet>();

            foreach (string[] cabinetData in cabinetsData)
            {
                string[] shelfNumbers = cabinetData[1].Split(',');
                cabinets.Add(new Cabinet(uint.Parse(cabinetData[0]), new uint[] { uint.Parse(shelfNumbers[0]), uint.Parse(shelfNumbers[1]) }));
            }

            return cabinets;
        }

        static List<Reader> CreateReadersList(List<string[]> readersData)
        {
            List<Reader> readers = new List<Reader>();

            foreach (string[] readerData in readersData)
            {
                readers.Add(new Reader(uint.Parse(readerData[0]), readerData[1]));
            }

            return readers;
        }

        static Library CreateLibrary(List<string[]> booksData)
        {
            Library library = new Library(
                new uint[booksData.Count], new uint[booksData.Count], new DateOnly[booksData.Count], new DateOnly[booksData.Count]
            );

            for (int i = 0; i < booksData.Count; i++)
            {
                library.BookIds[i] = uint.Parse(booksData[i][0]);
                library.ReaderIds[i] = uint.MinValue;
                library.DatesTaking[i] = DateOnly.MinValue;
                library.DatesReturn[i] = DateOnly.MinValue;
            }

            return library;
        }

        static Library AddDataFromLibraryData(Library library, List<string[]> libraryData)
        {
            for (int i = 0; i < libraryData.Count; i++)
            {
                if (library.BookIds.Contains(uint.Parse(libraryData[i][1])))
                {
                    library.ReaderIds[uint.Parse(libraryData[i][0]) - 1] = uint.Parse(libraryData[i][0]);
                    library.DatesTaking[uint.Parse(libraryData[i][0]) - 1] = DateOnly.Parse(libraryData[i][2]);
                    library.DatesReturn[uint.Parse(libraryData[i][0]) - 1] = DateOnly.Parse(libraryData[i][3]);
                }
            }

            return library;
        }

        static List<Book> CheckTheAvailabilityOfBooks(Library library, List<Book> books)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (library.DatesReturn[books[i].Id - 1] != DateOnly.MinValue)
                {
                    books[i].SetAvailable(true);
                }
                else
                {
                    books[i].SetAvailable(false);
                }
            }

            return books;
        }
    }
}