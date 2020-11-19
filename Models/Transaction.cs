using System;

namespace DotNet5WebApi.Models
{
    public class Transaction : ModelBase
    {
        int Id { get; set; }
        DateTime TransactionDate { get; set; }
        string Code { get; set; }
        string CurrencyCode { get; set; }
        decimal Amount { get; set; }
        string Payment { get; set; }
        string Status { get; set; }
    }
}