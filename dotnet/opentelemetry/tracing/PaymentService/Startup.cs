using System;
using FP.Monitoring.Trace.Common;
using FP.Monitoring.Trace.PaymentService.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FP.Monitoring.Trace.PaymentService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<CardProvider>();

            services.AddHttpClient("visa", c =>
            {
                c.BaseAddress = new Uri(Configuration["VisaServiceUrl"]);
            });

            services.AddHttpClient("master", c =>
            {
                c.BaseAddress = new Uri(Configuration["MasterServiceUrl"]);
            });

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            services.AddTracing(Configuration["OpenTelemetryUrl"], "PaymentService");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
