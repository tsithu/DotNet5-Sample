using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNet5WebApi.Models;
using DotNet5WebApi.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
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
                var folderName = Path.Combine(Directory.GetCurrentDirectory(), Config["UploadDir"]);
                var filePath =  Path.Combine(folderName, fileName);
                var validator = new DotNet5WebApi.Library.Validator();
                var parserFactory = new ParserFactory();
                var transactionParser = parserFactory.GetDataParser<Transaction>(filePath, validator);
                var transactionList = transactionParser.Parse((parserType, payload) => {
                    switch(parserType) {
                        case "csv":
                            var csv = payload as CsvHelper.CsvReader;
                            csv.Configuration.RegisterClassMap<CsvTransactionMap>();
                            return null;
                        case "xml":
                            return new DotNet5WebApi.Library.XmlTransactionMap();
                        default:
                            return null;
                    }
                });

                // Logger.LogInformation(JsonSerializer.Serialize(transactionList));
                var effectedRecords = await SaveToDatabase<Transaction>(transactionList);
                
                return Ok(effectedRecords);
            }
            catch (ValidationException errors) {
                return StatusCode(400, $"Bad Request: {errors}");
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
