using System;
using System.Collections.Generic;

namespace DotNet5WebApi.Library
{
    public class CsvParser<T> :  IParser<T>
    {
        public string File { get; init; }
        public Validator Validator { get; init; }

        public CsvParser(string file, Validator validator)
            => (File, Validator) = (file, validator);

        public List<T> Parse() {
            var list = new List<T>();

            return list;
        }
    }
}