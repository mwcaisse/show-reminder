using System;
using System.Text;
using System.Threading.Tasks;
using Quartz;
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

            var body = new StringBuilder();

            foreach (var show in shows)
            {
                body.Append("<p>New Episode for ").Append(show.Name).Append("</p>");
                body.Append("<p>Episode Name:").Append(show.LastEpisode.Name).Append("</p>");
                body.Append("<p>Episode Number: S").Append(show.LastEpisode.SeasonNumber).Append("E")
                    .Append(show.LastEpisode.EpisodeNumber).Append("</p>");
                body.Append("<p>Episode Air Date:").Append(show.LastEpisode.AirDate).Append("</p>");
                body.Append("<br />");
            }

            await _emailManager.SendEmail("New Shows!", body.ToString());
          
        }
        
        
    }
}