using System;
using System.Threading.Tasks;
using FP.Monitoring.PrometheusNet.Business;
using FP.Monitoring.PrometheusNet.Contract;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Prometheus;

namespace FP.Monitoring.PrometheusNet.Services
{
    public class DemoService : Contract.DemoService.DemoServiceBase
    {
        private readonly ILogger<DemoService> _logger;
        private readonly MeetupRepository _meetupRepository;

        public DemoService(ILogger<DemoService> logger, MeetupRepository meetupRepository)
        {
            _logger = logger;
            _meetupRepository = meetupRepository;
        }

        public override async Task<GreetAttendeeResponse> GreetAttendee(GreetAttendeeRequest request, ServerCallContext context)
        {
            using (MyMetrics.GreetingDuration.NewTimer())
            {
                var greeting = $"Hallo to {string.Join(',', request.Names)}";
                await Task.Delay(TimeSpan.FromSeconds(new Random().Next(1, 5)));
                return new GreetAttendeeResponse
                {
                    Message = greeting
                };
            }

           
        }

        public override async Task<ScheduleMeetupResponse> ScheduleMeetup(ScheduleMeetupRequest request, ServerCallContext context)
        {
            await _meetupRepository.AddMeetup(request.Title, request.Speaker, request.Location,
                request.Start.ToDateTime(), request.End.ToDateTime());

            return new ScheduleMeetupResponse
            {
                CreatedAt = DateTime.UtcNow.ToTimestamp()
            };
        }
    }
}
