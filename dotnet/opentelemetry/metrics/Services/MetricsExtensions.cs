using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace FP.Monitoring.OpenTelemetryMetrics.Services
{
    public static class MetricsExtensions
    {
        public static async Task<T> MeterDuration<T>(this Histogram<double> instance, Func<Task<T>> call)
        {
            return await instance.MeterDuration(call, new TagList());
        }

        public static async Task<T> MeterDuration<T>(this Histogram<double> instance, Func<Task<T>> call, TagList tagList)
        {
            var timer = new Stopwatch();
            try
            {
                timer.Start();
                return await call();
            }
            finally
            {
                timer.Stop();
                instance.Record(timer.Elapsed.TotalMilliseconds, tagList);
            }
        }
    }
}
