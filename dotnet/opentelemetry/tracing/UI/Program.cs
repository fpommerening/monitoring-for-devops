using FP.Monitoring.Trace.Common;
using FP.Monitoring.Trace.UI.Business;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddTracing(builder.Configuration["OpenTelemetryUrl"], "UI");

builder.Services.AddSingleton<OrderRepository>();

builder.Services.AddHttpClient("stockservice", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["StockServiceUrl"]);
});

builder.Services.AddHttpClient("paymentservice", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["PaymentServiceUrl"]);
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Product}/{action=Index}/{id?}");
});

app.Run();