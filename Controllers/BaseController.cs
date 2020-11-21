using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNet5WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected ILogger<TransactionController> Logger { get; init; }
        protected AppDbContext DbContext { get; init; }
        public BaseController(ILogger<TransactionController> logger, AppDbContext dbContext)
            => (Logger, DbContext) = (logger, dbContext);
    }
}