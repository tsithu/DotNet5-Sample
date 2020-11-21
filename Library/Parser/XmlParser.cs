using System;
using System.Collections.Generic;
namespace DotNet5WebApi.Library
{
    public class XmlParser<T> : IParser<T>
    {
        public string File { get; init; }
        public Validator Validator { get; init; }

        public XmlParser(string file, Validator validator)
            => (File, Validator) = (file, validator);
        public IEnumerable<T> Parse(Action<string, dynamic> callback = null) {
            var list = new List<T>();

            return list;
        }
    }
}