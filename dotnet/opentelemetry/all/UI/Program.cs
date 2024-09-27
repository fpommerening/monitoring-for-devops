using FP.Monitoring.All.Common;
using FP.Monitoring.All.Contract;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;
using UI.Business;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddTransient<OrderRepository>();

TelemetryBuilder
    .ForConfiguration("UI", builder.Configuration["OpenTelemetryUrl"])
    .AddTracing(builder.Services, true, false, true)
    .AddMetrics(builder.Services, true, true, false, false)
    .AddLogging(builder.Logging)
    .Build();

builder.Services.AddHttpClient("stockservice", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["StockServiceUrl"]);
});

builder.Services.AddHttpClient("paymentservice", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["PaymentServiceUrl"]);
});

builder.Services.AddGrpcClient<CustomerServices.CustomerServicesClient>(o =>
{
    o.Address = new Uri(builder.Configuration["CustomerServiceUrl"]);
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();