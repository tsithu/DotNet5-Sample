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
using System.Text.Json;
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
                var filePath = await SaveFile(file);
                var parsedData = ParseData<Transaction>(filePath, new TransactionValidator(), Hook4Transaction);
                var effectedRecords = await SaveToDatabase<Transaction>(parsedData);
                
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
        private dynamic Hook4Transaction(string parserType, dynamic payload) {
            switch (parserType)
            {
                case "csv":
                    var csv = payload as CsvHelper.CsvReader;
                    csv.Configuration.RegisterClassMap<CsvTransactionMap>();
                    return null;
                case "xml":
                    return new DotNet5WebApi.Library.XmlTransactionMap();
                default:
                    return null;
            }
        }
        private async Task<string> SaveFile(IFormFile file) {
            var fileName = file?.FileName;
            var folderName = Path.Combine(Directory.GetCurrentDirectory(), Config["UploadDir"]);
            var fileFullPath = Path.Combine(folderName, fileName);

            using (var stream = System.IO.File.Create(fileFullPath))
            {
                await file.CopyToAsync(stream);
            }

            return fileFullPath;
        }
        private IEnumerable<T> ParseData<T>(string filePath, DotNet5WebApi.Library.Validator validator, Func<string, dynamic, dynamic> hook) {
            var parserFactory = new ParserFactory();
            var transactionParser = parserFactory.GetDataParser<T>(filePath, validator);
            var records = transactionParser.Parse(hook);
            return records;
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
