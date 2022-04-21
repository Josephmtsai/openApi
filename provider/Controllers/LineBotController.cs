using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using provider.InfraStructure.Log;
using provider.Model.LineBot;

namespace provider.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        private readonly LineBotConfig _lineBotConfig;
        private readonly ILogger<LineBotController> _logger;
        public LineBotController(LineBotConfig lineBotConfig, ILogger<LineBotController> ilogger) 
        {
            _lineBotConfig = lineBotConfig;
            _logger = ilogger;
        }
        [HttpPost]
        public IActionResult LineBotCallBack(dynamic request)
        {
            return Ok(request);
        }

        [HttpGet]
        public IActionResult LineBotTest()
        {
            LogExtensions.ServerLog(_logger, "Config", _lineBotConfig.ChannelAccessToken);
            return Ok("Testing");
        }
    }
}
