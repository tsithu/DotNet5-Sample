using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DotNet5WebApi.Models;
namespace DotNet5WebApi.Controllers
{
    public class TransactionController : BaseController
    {
        public TransactionController(
            ILogger<TransactionController> logger, AppDbContext dbContext
        ) : base(logger, dbContext)
        {}

        // GET: api/transaction
        [HttpGet]
        public async Task<ActionResult<ICollection<Transaction>>> Get()
        {   
            var records = await DbContext.Transactions
                .Where(t => t.IsDeleted != true)
                .ToListAsync();

            if (records == null)
            {
                return NotFound();
            }

            return records;
        }

        // GET api/transaction/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> Get(int id)
        {
            var record = await DbContext.Transactions.FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return record;
        }

        // GET api/transaction/currency/:currency
        [HttpGet("currency/{currency}")]
        public async Task<ActionResult<ICollection<Transaction>>> ByCurrency(string currency)
        {
            var records = await DbContext.Transactions
                .Where(t => t.CurrencyCode == currency && t.IsDeleted != true)
                .ToListAsync();

            if (records == null)
            {
                return NotFound();
            }

            return records;
        }

        // GET api/transaction/status/:status
        [HttpGet("status/{status}")]
        public async Task<ActionResult<ICollection<Transaction>>> ByStatus(string status)
        {
            var records = await DbContext.Transactions
                .Where(t => t.Status == status && t.IsDeleted != true)
                .ToListAsync();

            if (records == null)
            {
                return NotFound();
            }

            return records;
        }

        // GET api/transaction/transaction-date/:start/:end
        [HttpGet("transaction-date/{start}/{end}")]
        public async Task<ActionResult<ICollection<Transaction>>> ByDateRange(
            DateTime start, DateTime end
        )
        {
            var records = await DbContext.Transactions
                .Where(t => t.TransactionDate >= start && t.TransactionDate <= end && t.IsDeleted != true)
                .ToListAsync();

            if (records == null)
            {
                return NotFound();
            }

            return records;
        }
        
        // Following is not included in specification
        // POST api/transaction
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Transaction transaction)
        {
            var existingRecord = await DbContext.Transactions
                .FirstOrDefaultAsync(record => record.Id == transaction.Id && record.IsDeleted != true);

            if (existingRecord != null)
            {
                return BadRequest($"{transaction.Code} already exist in transactions.");
            }

            DbContext.Add(transaction);
            await DbContext.SaveChangesAsync();

            return Ok(transaction);
        }

        // PUT api/transaction/:id ( Use to update entire resource. User PATCH for partial update )
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] Transaction transaction)
        {
            var existingRecord = await DbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            if (existingRecord != null)
            {
                DbContext.Entry(existingRecord).Context.Update(transaction);
                await DbContext.SaveChangesAsync();
            }
        }

        // DELETE api/transaction/:id
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var existingRecord = await DbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            if (existingRecord != null)
            {
                DbContext.Transactions.Remove(existingRecord);
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
