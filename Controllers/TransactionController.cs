using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNet5WebApi.Models;

namespace DotNet5WebApi.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Transaction> Get()
        {   
            var transactionList = new List<Transaction>();
            
            return transactionList;
        }
    }
}
