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
                c.BaseAddress = new Uri("http://visa-service.demo-apps.de");
            });

            services.AddHttpClient("master", c =>
            {
                c.BaseAddress = new Uri("http://master-service.demo-apps.de");
            });

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            services.AddTracing("http://otel.t.container-training.de", "PaymentService");
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
