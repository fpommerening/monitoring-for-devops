
using System.Diagnostics;

namespace FP.Monitoring.All.Common;

public class Instrumentation : IDisposable
{
    internal const string ActivitySourceName = "fp.monitoring.demo.instrumentationlibrary";
    public ActivitySource ActivitySource { get; } 

    public Instrumentation()
    {
        string? version = typeof(Instrumentation).Assembly.GetName().Version?.ToString();
        ActivitySource = new ActivitySource(ActivitySourceName, version);
    }

    public void Dispose()
    {
        ActivitySource?.Dispose();
    }
}