using System.Threading.Tasks;
using FP.Monitoring.PrometheusNet.Business;
using FP.Monitoring.PrometheusNet.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace FP.Monitoring.PrometheusNet
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddSingleton<MeetupRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseGrpcMetrics();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<DemoService>();
                endpoints.MapMetrics();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

            var repo = app.ApplicationServices.GetRequiredService<MeetupRepository>();

            Task.Run(() =>
            {
                repo.CheckMeetups();
            });
        }
    }
}
