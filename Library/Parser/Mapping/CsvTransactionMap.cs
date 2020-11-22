using System;
using CsvHelper.Configuration;
using DotNet5WebApi.Models;
using System.Text.Json;
using System.Globalization;

namespace DotNet5WebApi.Library {
    public class CsvTransactionMap : ClassMap<Transaction>
    {
        public CsvTransactionMap()
        {
            // "Invoice0000001","1,000.00","USD","20/02/2019 12:33:16","Approved"
            Map(t => t.Code).Index(0);
            Map(t => t.Amount).Index(1);
            Map(t => t.CurrencyCode).Index(2);
            Map(t => t.TransactionDate).Index(3)
                ?.TypeConverterOption.Format("dd/MM/yyyy HH:mm:ss");
            Map(t => t.Status)
                ?.ConvertUsing(
                row =>
                {
                    var status = row.GetField(4);
                    switch (status?.ToLower()) {
                        case "approved":
                            return "A";
                        case "failed":
                            return "R";
                        case "finished":
                            return "D";
                        default:
                            return "";
                    }
                });
        }
    }
}