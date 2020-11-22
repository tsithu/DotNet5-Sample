using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DotNet5WebApi.Models;
namespace DotNet5WebApi.Library
{
    public class CsvParser<T> :  IParser<T>
    {
        public string File { get; init; }
        public Validator Validator { get; init; }

        public CsvParser(string file, Validator validator)
            => (File, Validator) = (file, validator);
        public IEnumerable<T> Parse(Func<string, dynamic, dynamic> callback = null) {
            using (var reader = new StreamReader(File))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                if (callback != null) callback("csv", csv);
                var records = csv.GetRecords<T>()?.ToList();

                if (Validator != null)
                {
                    var sbErrors = new StringBuilder();
                    foreach (var record in records)
                    {
                        var (isValid, errors) = Validator.Verify(record as ModelBase);
                        if (!isValid)
                        {
                            sbErrors.AppendLine(errors);
                        }
                    }
                    if (sbErrors.Length > 0)
                    {
                        throw new ValidationException(sbErrors.ToString());
                    }
                }

                return records;
            }
        }
    }
}