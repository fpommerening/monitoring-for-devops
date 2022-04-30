using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FP.Monitoring.PrometheusNet.Business
{
    public class MeetupRepository
    {
        private List<Meetup> _meetups = new();

        public async Task AddMeetup(string title, string speaker, string location, DateTime start, DateTime end)
        {
            _meetups.Add(new Meetup
            {
                Title = title,
                Speaker = speaker,
                Location = location,
                Start = start,
                End = end
            });

            MyMetrics.MeetupsCount.WithLabels(location).Inc();

            await Task.Delay(TimeSpan.FromMilliseconds(500));
        }

        public int Count(Func<Meetup, bool> predicate)
        {
            return _meetups.Count(predicate);
        }
    }
}
