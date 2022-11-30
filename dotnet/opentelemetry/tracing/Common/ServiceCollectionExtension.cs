using System;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FP.Monitoring.Trace.Common
{
    public static class ServiceCollectionExtension
    {
        public static void AddTracing(this IServiceCollection services, string url, string servicename)
        {
            services.AddOpenTelemetryTracing((builder) => builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(servicename))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource(DemoActivitySource.ActivitySourceName)
                .AddOtlpExporter(otlpOptions =>
                {
                    otlpOptions.Endpoint = new Uri(url);
                })
            );
        }
    }
}
