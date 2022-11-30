using System.Diagnostics;

namespace FP.Monitoring.Trace.Common
{
    public static class DemoActivitySource
    {
        public const string ActivitySourceName = "fp.monitoring.demo.instrumentationlibrary";

        public static ActivitySource ActivitySource = new ActivitySource(ActivitySourceName, "semver1.0.0");
    }
}
