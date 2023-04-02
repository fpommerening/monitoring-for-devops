using FP.Monitoring.All.Common;
using FP.Monitoring.All.Common.Models;
using FP.Monitoring.All.PaymentService;
using Microsoft.AspNetCore.Mvc;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<CardProvider>();

builder.Services.AddHttpClient("visa", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["VisaServiceUrl"]);
});

builder.Services.AddHttpClient("master", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["MasterServiceUrl"]);
});

TelemetryBuilder
    .ForConfiguration("PaymentService", builder.Configuration["OpenTelemetryUrl"])
    .AddTracing(builder.Services)
    .AddMetrics(builder.Services)
    .AddLogging(builder.Logging)
    .Build();


var app = builder.Build();

app.MapGet("/", () => "Trace-Paymentservice");

app.MapPut("/payments", async ([FromBody] Payment payment, [FromServices] CardProvider cardProvider) =>
{
    if (string.IsNullOrEmpty(payment.Name))
    {
        return Results.BadRequest("Missing Name");
    }
    await cardProvider.Validate(payment.Type, payment.Number);
    return Results.Accepted();
});
app.Run();
