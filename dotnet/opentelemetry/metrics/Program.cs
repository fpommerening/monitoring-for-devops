using System.Reflection;
using FP.Monitoring.OpenTelemetryMetrics.Business;
using FP.Monitoring.OpenTelemetryMetrics.Services;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";
var serviceName = "OpenTelemetryDemo";

var resourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName, serviceVersion: assemblyVersion, serviceInstanceId: Environment.MachineName);


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MeetupRepository>();

builder.Services.AddGrpc(options => options.Interceptors.Add<MeticsInterceptor>());


builder.Services.AddOpenTelemetryMetrics(options =>
{
    options.SetResourceBuilder(resourceBuilder);
    options.AddAspNetCoreInstrumentation();
    options.AddMeter("MyMeetupMetrics");
    options.AddMeter("gRPCMetrics");
    options.AddView("greeeting_duration", new ExplicitBucketHistogramConfiguration {Boundaries = new double[] {50,100,500, 1_000, 2_000, 5_000, 10_000}});
    options.AddPrometheusExporter();
});

var app = builder.Build();
app.MapGrpcService<DemoService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();