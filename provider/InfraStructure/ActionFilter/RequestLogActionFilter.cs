using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using provider.InfraStructure.Json;
using provider.InfraStructure.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace provider.InfraStructure.ActionFilter
{
    public class RequestLogActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<RequestLogActionFilter> _logger;

        public RequestLogActionFilter(ILogger<RequestLogActionFilter> ilogger)
        {
            _logger = ilogger;
            LogExtensions.ServerLog(_logger, "Consturcter", "Initial", "", "");
        }

        public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
        {
            LogExtensions.RequestLog(_logger, context.HttpContext.Request.Method.ToString(), context.HttpContext.Request.Path, "", paramsReader(context));
            await next();            
        }

        private string paramsReader(ActionExecutingContext context)
        {
            string paseredStr = string.Empty;
            var method = context.HttpContext.Request.Method.ToString();

            switch (method)
            {
                case "POST":
                case "PUT":
                    if (context.ActionArguments.Keys.Count > 0 ) {
                        var safeObject = context.ActionArguments[context.ActionArguments.Keys.ElementAt(0)];
                        paseredStr = JsonConvert.SerializeObject(safeObject, Formatting.None, new RequestJsonConverter());
                    }
                    break;

                case "GET":
                default:
                    paseredStr = context.HttpContext.Request.QueryString.ToString();
                    break;
            }
            return paseredStr;
        }
    }
}
