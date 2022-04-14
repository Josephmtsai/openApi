using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using provider.InfraStructure.Log;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace provider.InfraStructure.Middleware
{
    public class RequestLogMiddleware
    {
        //Name of the Response Header, Custom Headers starts with "X-"  
        private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time-ms";
        // Handle to the next Middleware in the pipeline  
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLogMiddleware> _logger;
        public RequestLogMiddleware(RequestDelegate next, ILogger<RequestLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public Task InvokeAsync(HttpContext context)
        {
            // Start the Timer using Stopwatch  
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() =>
            {
                // Stop the timer information and calculate the time   
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                // Add the Response time information in the Response headers.   
                context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
                Log.LogExtensions.PerformanceLog(_logger, context.Request.Path.ToUriComponent(), responseTimeForCompleteRequest, "MiddleWare");
                return Task.CompletedTask;
            });
            // Call the next delegate/middleware in the pipeline   
            return this._next(context);
        }
    }
}
