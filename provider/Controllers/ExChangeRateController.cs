using Microsoft.AspNetCore.Mvc;


namespace provider.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExChangeRateController : ControllerBase
    {
        [HttpGet]
        
        public string GetExchangeRate()
        {
            return "OK";
        }
    }
}
