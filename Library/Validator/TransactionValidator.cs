using System;
using System.Text;
using DotNet5WebApi.Models;

namespace DotNet5WebApi.Library
{
    public class TransactionValidator : Validator
    {
        public override (bool, string) Verify(dynamic record)
        {
            var transaction = record as Transaction;
            var sbErrors = new StringBuilder();
            var data = record as Transaction;
            var isValid = true;

            if (record != null)
            {
                if (String.IsNullOrEmpty(transaction.Code)) 
                {
                    sbErrors.AppendLine("Required: \"Code\" field cannot be empty.");
                }
                if (transaction.TransactionDate == DateTime.MinValue)
                {
                    sbErrors.AppendLine("Invalid: \"TransactionDate\" field is not valid.");
                }
                if (String.IsNullOrEmpty(transaction.CurrencyCode))
                {
                    sbErrors.AppendLine("Required: \"CurrencyCode\" field cannot be empty.");
                }
                if (String.IsNullOrEmpty(transaction.Status))
                {
                    sbErrors.AppendLine("Invalid: \"Status\" field is not valid.");
                    sbErrors.AppendLine("Required: \"Status\" field cannot be empty.");
                }
                isValid = sbErrors.Length == 0;
                if (!isValid) {
                    sbErrors.Insert(0, String.Format("Start Error On Transaction. Code => {0}{1}", transaction.Code, Environment.NewLine));
                    sbErrors.AppendLine(String.Format("End Error On Transaction. Code => {0}", transaction.Code));
                }
            }

            return (isValid, sbErrors.ToString());
        }
    }
}