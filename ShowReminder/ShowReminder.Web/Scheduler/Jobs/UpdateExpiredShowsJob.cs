using System.Threading.Tasks;
using Quartz;
using ShowReminder.Web.Manager;

namespace ShowReminder.Web.Scheduler.Jobs
{
    public class UpdateExpiredShowsJob : IJob
    {

        private readonly TrackedShowManager _trackedShowManager;

        public UpdateExpiredShowsJob(TrackedShowManager trackedShowManager)
        {
            this._trackedShowManager = trackedShowManager;
        }

        public Task Execute(IJobExecutionContext context)
        {
            this._trackedShowManager.UpdateExpiredShows();
            return Task.CompletedTask;
        }

    }
}
