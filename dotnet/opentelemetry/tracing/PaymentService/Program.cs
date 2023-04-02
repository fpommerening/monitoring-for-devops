using FP.Monitoring.Trace.PaymentService.Controllers;
using FP.Monitoring.Trace.Common;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<CardProvider>();

builder.Services.AddHttpClient("visa", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["VisaServiceUrl"]);
});

builder.Services.AddHttpClient("master", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["MasterServiceUrl"]);
});

builder.Services.AddTracing(builder.Configuration["OpenTelemetryUrl"], "PaymentService");
    
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();