using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using provider.InfraStructure.Extension;
using provider.InfraStructure.Log;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace provider.InfraStructure.ActionFilter
{
    public class ResponseLogActionFilter : IAsyncResultFilter
    {
        private static readonly int resultSizelimit = 1024;

        private readonly ILogger<ResponseLogActionFilter> _logger;

        public ResponseLogActionFilter(ILogger<ResponseLogActionFilter> ilogger)
        {
            _logger = ilogger;
            LogExtensions.ServerLog(_logger, "Consturcter", "Initial", "", "");
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var readableStream = await context.HttpContext.CloneBodyAsync(() => next());
            //suppose the response body contains text-based content
            using (var sr = new StreamReader(readableStream))
            {
                var responseText = await sr.ReadToEndAsync();
                LogExtensions.RequestLog(_logger, "OutPut", context.HttpContext.Request.Path, "", responseText, "");
            }
        }


        //private string paramsReader(ActionExecutingContext context)
        //{
        //    string paseredStr = string.Empty;
        //    var method = context.HttpContext.Request.Method.ToString();

        //    switch (method)
        //    {
        //        case "POST":
        //        case "PUT":
        //            var safeObject = context.ActionArguments[context.ActionArguments.Keys.ElementAt(0)];
        //            paseredStr = JsonConvert.SerializeObject(safeObject, Formatting.None, requestJsonConverter);
        //            break;

        //        case "GET":
        //        default:
        //            paseredStr = context.HttpContext.Request.QueryString.ToString();
        //            break;
        //    }
        //    return paseredStr;
        //}
        //private string StrSizeCheck(string resultStr)
        //{
        //    var stringSize = ASCIIEncoding.ASCII.GetByteCount(resultStr);
        //    if (stringSize > resultSizelimit)
        //        resultStr = string.Format("Result size - {0} bytes", stringSize);
        //    return resultStr;
        //}
    }
}
