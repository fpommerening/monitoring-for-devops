using System;
using System.Threading;
using System.Threading.Tasks;
using FP.Monitoring.PrometheusNet.Business;
using Microsoft.Extensions.Hosting;

namespace FP.Monitoring.PrometheusNet.Services
{
    public class MetricsService : BackgroundService
    {
        private readonly MeetupRepository _meetupRepository;

        public MetricsService(MeetupRepository meetupRepository)
        {
            _meetupRepository = meetupRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                MyMetrics.MeetupsInFutureCount.Set(_meetupRepository.Count(m => m.Start > DateTime.UtcNow));
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
