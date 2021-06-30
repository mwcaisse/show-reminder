using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using ShowReminder.Data.Entity;
using ShowReminder.Web.Manager;
using ShowReminder.Web.Mapper;

namespace ShowReminder.Web.Scheduler.Jobs
{
    public class SendEmailRecentlyAiredShowsJob : IJob
    {
        private readonly TrackedShowManager _trackedShowManager;
        private readonly EmailManager _emailManager;

        public SendEmailRecentlyAiredShowsJob(TrackedShowManager trackedShowManager, EmailManager emailManager)
        {
            this._trackedShowManager = trackedShowManager;
            this._emailManager = emailManager;
        }

        public async Task Execute(IJobExecutionContext context)
        {

            var shows = _trackedShowManager.GetShowsAiredAfter(DateTime.Now.AddDays(-2));
            if (shows.Any())
            {

                var body = new StringBuilder();
                body.Append("<h1> New Shows! </p>");

                foreach (var show in shows)
                {
                    body.Append($"<p>New Episode for {show.Name}</p>");
                    body.Append($"<p>Episode Name: {show.LastEpisode.Name}</p>");
                    body.Append(
                        $"<p>Episode Number: {show.LastEpisode.SeasonNumber:D2}E{show.LastEpisode.EpisodeNumber:D2}</p>");

                    if (show.LastEpisode.AirDate.HasValue)
                    {
                        body.Append($"<p>Episode Air Date: {show.LastEpisode.AirDate.Value.Date}</p>");
                    }
                    body.Append($"<p><a href=\"{ConstructTorrentSearchUrl(show)}\">Link To Download (rarbg)</a></p>");
                    body.Append("<br />");
                }
                await _emailManager.SendEmail("New Shows!", body.ToString());
            }
        }

        public static string ConstructTorrentSearchUrl(TrackedShow show)
        {
            return
                "https://rarbg.to/torrents.php?category=18;41;49" +
                $"&search={show.Name.Replace(" ", "+")}+1080p" +
                $"+s{show.LastEpisode.SeasonNumber:D2}e{show.LastEpisode.EpisodeNumber:D2}&order=seeders&by=DESC";

        }
        
        
    }
}