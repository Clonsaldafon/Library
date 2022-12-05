using Library.WorkWithSchemas;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class ReaderFromFile
    {
        public static string[] GetPaths(string pathOfProject, string[] fileNames, string folder, string type)
        {
            string[] paths = new string[fileNames.Length];

            for (int i = 0; i < fileNames.Length; i++)
            {
                paths[i] = $"{pathOfProject}\\Resources\\{folder}\\{fileNames[i]}-{type}";
            }

            return paths;
        }

        public static List<Schema> GetSchemas(string[] pathsToJSONFiles)
        {
            List<Schema> schemas = new List<Schema>();

            for (int i = 0; i < pathsToJSONFiles.Length; i++)
            {
                Schema schema = JsonConvert.DeserializeObject<Schema>(File.ReadAllText(pathsToJSONFiles[i]));
                schemas.Add(schema);
            }

            return schemas;
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
