﻿
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace FP.Monitoring.All.Common;

public class Instrumentation : IDisposable
{
    internal const string ActivitySourceName = "fp.monitoring.demo.instrumentationlibrary";
    internal const string MeterName = "fp.monitoring.demo.instrumentationlibrary";
    private readonly Meter meter;
    public ActivitySource ActivitySource { get; } 

    public Instrumentation()
    {
        string? version = typeof(Instrumentation).Assembly.GetName().Version?.ToString();
        ActivitySource = new ActivitySource(ActivitySourceName, version);
        meter = new Meter(MeterName, version);
        
    }

    public ObservableGauge<T> CreateObservableGauge<T>(string name, Func<T> observeValue, string? unit = null, string? description = null) where T : struct
    {
        return meter.CreateObservableGauge(name, observeValue, unit, description);
    }
    
    public ObservableGauge<T> CreateObservableGauge<T>(string name, Func<IEnumerable<Measurement<T>>> observeValues, string? unit = null, string? description = null) where T : struct
    {
        return meter.CreateObservableGauge(name, observeValues, unit, description);
    }
    
    public Counter<T> CreateCounter<T>(string name, string? unit = null, string? description = null) where T : struct
    {
        return meter.CreateCounter<T>(name, unit, description);
    }

    public void Dispose()
    {
        ActivitySource?.Dispose();
        meter?.Dispose();
    }
    
    
}