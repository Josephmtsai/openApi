using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace provider.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RequestErrorController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult GetUnauthorized() {
            return Unauthorized("You have no permission");
        }

        [HttpGet]
        public IActionResult GetSystemError()
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "");
        }
    }
}
