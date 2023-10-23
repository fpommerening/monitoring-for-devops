using FP.Monitoring.External;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

var openTelemetryUrl = builder.Configuration["OpenTelemetryUrl"]!;
var servicename = builder.Configuration["Servicename"]!;

builder.Services.AddOpenTelemetry()
    .ConfigureResource(rb => rb.AddEnvironmentVariableDetector().AddService(servicename))
    .WithTracing(traceBuilder =>
    {
        traceBuilder.SetSampler(new AlwaysOnSampler());
        traceBuilder.AddAspNetCoreInstrumentation();
        traceBuilder.AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri(openTelemetryUrl);
        });
    });

builder.Services.AddSingleton<DelayGenerator>();

var app = builder.Build();


app.Map("/min", ([FromServices] DelayGenerator delayGenerator, [FromBody] ValueRequest request) =>
{
    delayGenerator.Min = request.Value;
});

app.Map("/max", ([FromServices] DelayGenerator delayGenerator, [FromBody] ValueRequest request) =>
{
    delayGenerator.Max = request.Value;
});

app.MapGet("/", async ([FromServices] DelayGenerator delayGenerator) =>
{
    await delayGenerator.WaitAsync();
    return "OK";
});

app.Run();
