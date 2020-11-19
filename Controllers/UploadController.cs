using System;
using System.IO;
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
        private readonly ILogger<UploadController> _logger;
        private readonly IConfiguration _config;

        public UploadController(ILogger<UploadController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Index()
        {
            try
            {
                /*                                
                TODO: get file and save
                */
                var fileName = "upload-test.csv"; // Request.Form.Files[0];
                var folderName = Path.Combine(Directory.GetCurrentDirectory(), this._config["UploadDir"]);
                var file =  Path.Combine(folderName, fileName);
                var validator = new Validator();
                var parserFactory = new ParserFactory();
                var transactionParser = parserFactory.GetDataParser<Transaction>(file, validator);
                var transactionList = transactionParser.Parse();
                /*                                
                TODO: To Save Parsed Data
                */
                _logger.LogInformation(file);
                _logger.LogInformation(transactionList.ToString());
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
