using Microsoft.Extensions.Logging;

namespace provider.InfraStructure.Log
{
    public static class LogExtensions
    {
        public static void ServerLog<T>(this ILogger<T> logger, string action, string status, string subStatus, string message)
        {
            logger.LogInformation($"[SERVER][{action}][{status}][{subStatus}]{message}");
        }
        public static void PerformanceLog<T>(this ILogger<T> logger, string method, long miliSeconds, string message)
        {
            logger.LogInformation($"[PERFORMANCE][{method}][{miliSeconds}]{message}");
        }

        public static void EventLog<T>(this ILogger<T> logger, string method, int miliSeconds, string message)
        {
            logger.LogInformation($"[SERVER][{method}][{miliSeconds}]{message}");
        }

        public static void RequestLog<T>(this ILogger<T> logger, string httpMethod, string method, string refereralUrl, string data, string message =null)
        {
            logger.LogInformation($"[Request][{httpMethod}][{method}][{refereralUrl}]{data} - {message}");
        }

    }
}
