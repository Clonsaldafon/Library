using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    }
}