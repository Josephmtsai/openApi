using NLog;
using NLog.LayoutRenderers;
using System;
using System.Text;

namespace provider.InfraStructure.Log
{
    [LayoutRenderer("elapsed-time")]
    public class ElapsedTimeLayoutRenderer : LayoutRenderer
    {
        private DateTime? _lastTimeStamp;

        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var lastTimeStamp = _lastTimeStamp ?? logEvent.TimeStamp;
            var elapsedTime = logEvent.TimeStamp - lastTimeStamp;
            var elapsedTimeString = $"{elapsedTime.TotalSeconds:f4}";
            builder.Append($"{elapsedTimeString}");
            _lastTimeStamp = logEvent.TimeStamp;
        }
    }
}
