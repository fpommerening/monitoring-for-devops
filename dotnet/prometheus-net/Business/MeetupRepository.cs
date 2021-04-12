using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FP.Monitoring.PrometheusNet.Business
{
    public class MeetupRepository
    {
        private List<Meetup> _meetups = new List<Meetup>();

        public async Task AddMeetup(string title, string speaker, string loaction, DateTime start, DateTime end)
        {
            _meetups.Add(new Meetup
            {
                Title = title,
                Speaker = speaker,
                Location = loaction,
                Start = start,
                End = end
            });

            MyMetrics.MeetupsCount.WithLabels(loaction).Inc();

            await Task.Delay(TimeSpan.FromMilliseconds(500));
        }

        public void CheckMeetups()
        {
            while (true)
            {
                MyMetrics.MeetupsInFutureCount.Set(_meetups.Count(x => x.Start > DateTime.UtcNow));
                System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }
    }
}
