using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
namespace DotNet5WebApi.Library
{
    public class XmlParser<T> : IParser<T>
    {
        public string File { get; init; }
        public Validator Validator { get; init; }

        public XmlParser(string file, Validator validator)
            => (File, Validator) = (file, validator);
        public IEnumerable<T> Parse(Func<string, dynamic, dynamic> hook = null) {
            var records = new List<T>();
            
            if (hook != null) {
                var xml = new XmlDocument();
                xml.Load(File);
                var root = xml.FirstChild;
                
                if (root != null && root.HasChildNodes)
                {
                    var transactions = xml.GetElementsByTagName("Transaction");

                    foreach (XmlElement transactionElement in transactions)
                    {
                        IMap mapper = hook("xml", null);
                        records.Add(mapper.Map(transactionElement));
                    }
                }
            }     

            
            
            return records;
        }
    }
}