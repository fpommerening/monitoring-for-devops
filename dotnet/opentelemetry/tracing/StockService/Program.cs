using FP.Monitoring.Trace.Common;
using FP.Monitoring.Trace.StockService.Business;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ProductRepository>();


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