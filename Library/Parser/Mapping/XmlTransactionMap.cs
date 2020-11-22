using System;
using System.Globalization;
using DotNet5WebApi.Models;
using System.Xml;

namespace DotNet5WebApi.Library {
    public class XmlTransactionMap : IMap
    {
        public XmlTransactionMap()
        {

        }
        public dynamic Map(dynamic transactionElement) {
            var newTran = new Transaction();

            newTran.Code = (transactionElement as XmlElement).GetAttribute("id");

            if (transactionElement.HasChildNodes)
            {
                foreach (XmlElement childNode in transactionElement.ChildNodes)
                {
                    switch (childNode.Name)
                    {
                        case "TransactionDate":
                            DateTime tranDate;
                            var dtFormat = "yyyy-MM-ddTHH:mm:ss";
                            var isValid = DateTime.TryParseExact(childNode.InnerText, dtFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out tranDate);
                            newTran.TransactionDate = isValid ? tranDate : DateTime.MinValue;
                            break;
                        case "PaymentDetails":
                            // Amount
                            var amountNode = childNode.GetElementsByTagName("Amount")?[0];
                            var amount = 0d;
                            Double.TryParse(amountNode.InnerText, out amount);
                            newTran.Amount = (decimal)amount;
                            // CurrencyCode
                            var currencyCodeNode = childNode.GetElementsByTagName("CurrencyCode")?[0];
                            newTran.CurrencyCode = currencyCodeNode.InnerText;
                            break;
                        case "Status":
                            switch (childNode.InnerText)
                            {
                                case "Approved":
                                    newTran.Status = "A";
                                    break;
                                case "Rejected":
                                    newTran.Status = "R";
                                    break;
                                case "Done":
                                    newTran.Status = "D";
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                }
            }

            return newTran as Transaction;
        }
}
}