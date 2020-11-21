using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNet5WebApi.Models;
using DotNet5WebApi.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
namespace DotNet5WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private ILogger<UploadController> Logger { get; init; }
        private IConfiguration Config { get; init; }
        private AppDbContext DbContext { get; init; }
        public UploadController(ILogger<UploadController> logger, IConfiguration config, AppDbContext dbContext)
         => (Logger, Config, DbContext) = (logger, config, dbContext);

        [HttpPost, RequestSizeLimit(1_000_000)]
        public async Task<ActionResult<int>> Index([FromForm] IFormFile file)
        {
            try
            {
                var fileName = file?.FileName;
                Logger.LogInformation(String.Format("File Name: {0} .", fileName));
                var folderName = Path.Combine(Directory.GetCurrentDirectory(), Config["UploadDir"]);
                var filePath =  Path.Combine(folderName, fileName);
                var validator = new Validator();
                var parserFactory = new ParserFactory();
                var transactionParser = parserFactory.GetDataParser<Transaction>(filePath, validator);
                var transactionList = transactionParser.Parse((parserType, obj) => {
                    switch(parserType) {
                        case "csv":
                            obj.Configuration.RegisterClassMap<TransactionMap>();
                            break;
                        case "xml":
                            // Do Something if needed
                            break;
                        default:
                            break;
                    }
                });

                var effectedRecords = await SaveToDatabase<Transaction>(transactionList);
                
                return Ok(effectedRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        private async Task<int> SaveToDatabase<T>(IEnumerable<T> records) {
            var count = 0;

            await using (var context = DbContext) {
                
                foreach (T record in records)
                {
                    context.Add(record);
                }

                count = await context.SaveChangesAsync();
            };

            return count;
        }
    }
}
