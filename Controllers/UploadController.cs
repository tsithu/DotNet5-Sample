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

namespace DotNet5WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private ILogger<UploadController> Logger { get; init; }
        private IConfiguration Config { get; init; }
        public UploadController(ILogger<UploadController> logger, IConfiguration config)
         => (Logger, Config) = (logger, config);


        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Index()
        {
            try
            {
                /*                                
                TODO: get file and save
                */
                var fileName = "upload-test.csv"; // Request.Form.Files[0];
                var folderName = Path.Combine(Directory.GetCurrentDirectory(), Config["UploadDir"]);
                var file =  Path.Combine(folderName, fileName);
                var validator = new Validator();
                var parserFactory = new ParserFactory();
                var transactionParser = parserFactory.GetDataParser<Transaction>(file, validator);
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
                /*                                
                TODO: To Save Parsed Data
                */
                Logger.LogInformation(file);
                Logger.LogInformation(JsonSerializer.Serialize(transactionList));
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
