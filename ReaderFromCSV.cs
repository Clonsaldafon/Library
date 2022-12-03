using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace Library
{
    internal class ReaderFromCSV
    {
        public static string[] GetPaths(string pathOfProject, string[] fileNames, string folder = "Data", string type = "data.csv")
        {
            string[] paths = new string[fileNames.Length];

            for (int i = 0; i < fileNames.Length; i++)
            {
                paths[i] = $"{pathOfProject}\\Resources\\{folder}\\{fileNames[i]}-{type}";
            }

            return paths;
        }

        public static List<string[]> DataParser(string pathToCSVFile)
        {
            List<string[]> data = new List<string[]>();

            using (TextFieldParser parser = new TextFieldParser(pathToCSVFile))
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
    }
}
