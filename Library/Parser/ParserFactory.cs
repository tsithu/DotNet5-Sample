using System;
using System.IO;
namespace DotNet5WebApi.Library
{
    public class ParserFactory
    {
        public IParser<T> GetDataParser<T>(string file, Validator validator) {
            var extension = Path.GetExtension(file);

            switch (extension?.ToLower()) {
                case ".csv":
                    return new CsvParser<T>(file, validator);
                case ".xml":
                    return new XmlParser<T>(file, validator);
                default:
                    throw new ArgumentException("Unsupported format.");
            }
        }
    }
}