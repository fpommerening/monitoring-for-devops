using Prometheus;

namespace FP.Monitoring.PrometheusNet.Business
{
    public class MyMetrics
    {
        internal static readonly Gauge MeetupsInFutureCount = Metrics
            .CreateGauge("meetup_in_future", "Number of meetups in future.");

        internal static readonly Counter MeetupsCount = Metrics.CreateCounter("meetup_total", "Number of meetups.", "location");

        internal static readonly Histogram GreetingDuration = Metrics
            .CreateHistogram("greeeting_duration_ms", "Histogram of greeting processing durations.");

    }
}
