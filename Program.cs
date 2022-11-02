using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

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

        static void Main(string[] args)
        {
            string pathOfProject = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            string[] csvPaths = new string[]
            {
                $"{pathOfProject}\\Resources\\Data\\book.data.csv",
                $"{pathOfProject}\\Resources\\Data\\author.data.csv",
                $"{pathOfProject}\\Resources\\Data\\cabinet.data.csv",
                $"{pathOfProject}\\Resources\\Data\\reader.data.csv",
                $"{pathOfProject}\\Resources\\Data\\library.data.csv"
            };
            string[] jsonPaths = new string[]
            {
                $"{pathOfProject}\\Resources\\Schemes\\book.schema.json",
                $"{pathOfProject}\\Resources\\Schemes\\author.schema.json",
                $"{pathOfProject}\\Resources\\Schemes\\cabinet.schema.json",
                $"{pathOfProject}\\Resources\\Schemes\\reader.schema.json",
                $"{pathOfProject}\\Resources\\Schemes\\library.schema.json",
            };

            List<string[]> booksData = CsvDataParser(csvPaths[(int)Table.Book]);
            List<string[]> authorsData = CsvDataParser(csvPaths[(int)Table.Author]);
            List<string[]> cabinetsData = CsvDataParser(csvPaths[(int)Table.Cabinet]);
            List<string[]> readersData = CsvDataParser(csvPaths[(int)Table.Reader]);
            List<string[]> libraryData = CsvDataParser(csvPaths[(int)Table.Library]);

            foreach (string[] data in libraryData)
            {
                Console.WriteLine(string.Join(", ", data));
            }
        }
    }
}