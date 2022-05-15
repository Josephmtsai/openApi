using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using provider.InfraStructure.Log;
using provider.Model.Log;

namespace provider.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly ILogger<LoggerController> _logger;
        public LoggerController(ILogger<LoggerController> ilogger)
        {
            _logger = ilogger;
        }

        [HttpPost]
        public void LogException(ExceptionModel model) {
            LogExtensions.ErrorLog(_logger, "VueError", $"Name:{model.Name} \n Path: {model.Location}\n Stack: {model.StackTrace}" , model.Message);
        }
    }
}
