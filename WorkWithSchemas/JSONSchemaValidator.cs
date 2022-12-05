using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Library.WorkWithSchemas
{
    internal class JSONSchemaValidator
    {
        public static bool IsValidToSchema(List<string[]> data, Schema schema, string fileName)
        {
            for (int i = 1; i < data.Count; i++)
            {
                string[] dataElements = data[i];

                for (int j = 0; j < dataElements.Length; j++)
                {
                    string dataElement = dataElements[j];

                    switch (schema.Elements[j].Type)
                    {
                        case "int":
                            if (!int.TryParse(dataElement, out var integer))
                            {
                                DisplayErrorMessage(i, j, dataElements, fileName);

                                return false;
                            }
                            break;
                        case "bool":
                            if (!bool.TryParse(dataElement, out var boolean))
                            {
                                DisplayErrorMessage(i, j, dataElements, fileName);
                                return false;
                            }
                            break;
                        case "dateTime":
                            if (!DateTime.TryParse(dataElement, out var date))
                            {
                                DisplayErrorMessage(i, j, dataElements, fileName);
                                return false;
                            }
                            break;
                        case "uint":
                            if (!uint.TryParse(dataElements[j], out var uinteger))
                            {
                                DisplayErrorMessage(i, j, dataElements, fileName);
                                return false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return true && IsColumnsValid(data[0], schema, fileName);
        }

        private static bool IsColumnsValid(string[] columns, Schema schema, string fileName)
        {;
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i] != schema.Elements[i].Name)
                {
                    DisplayErrorMessage(1, i, columns, fileName);
                    return false;
                }
            }
            return true;
        }

        private static void DisplayErrorMessage(int raw, int column, string[] line, string fileName)
        {
            string errorAccured = $"ERROR: [{fileName}] In raw:{raw}, column:{column + 1} - wrong type!\n";
            string correctionInfo = $"Line: {raw}. Element: {line[column]}";

            throw new FormatException(string.Concat(errorAccured, correctionInfo));
        }
    }
}
