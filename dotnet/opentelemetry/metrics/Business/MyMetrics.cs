using System.Diagnostics.Metrics;

namespace FP.Monitoring.OpenTelemetryMetrics.Business
{

    public class MyMetrics
    {
        internal static readonly Meter Metrics = new("MyMeetupMetrics");

        internal static readonly Counter<int> MeetupsCount = Metrics.CreateCounter<int>("meetup_total", description: "Number of meetups.");

        internal static readonly Histogram<double> GreetingDuration = Metrics.CreateHistogram<double>(
            "greeeting_duration", unit: "ms",
            description: "Histogram of greeting processing durations.");

    }
}
