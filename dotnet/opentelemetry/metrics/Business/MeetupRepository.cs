namespace FP.Monitoring.OpenTelemetryMetrics.Business
{
    public class MeetupRepository
    {
        public MeetupRepository()
        {
            MyMetrics.Metrics.CreateObservableGauge("meetup_in_future",
                description: "Number of meetups in future.", observeValue: () => _meetups.Count(x=>x.Start > DateTime.UtcNow));
        }

        private List<Meetup> _meetups = new();

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

            MyMetrics.MeetupsCount.Add(1, KeyValuePair.Create<string, object>("Location", loaction)!);
            await Task.Delay(TimeSpan.FromMilliseconds(500));
        }

    }
}
