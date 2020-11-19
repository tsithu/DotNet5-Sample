using System;
using System.Collections.Generic;

namespace DotNet5WebApi.Library
{
    public class XmlParser<T> : IParser<T>
    {
        public string File { get; init; }
        public Validator Validator { get; init; }

        public XmlParser(string file, Validator validator) {
            File = file;
            Validator = validator;
        }

        public List<T> Parse() {
            var list = new List<T>();

            return list;
        }
    }
}