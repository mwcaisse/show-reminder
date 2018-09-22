using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace ShowReminder.Web.Scheduler
{
    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _container;
        private readonly Dictionary<IJob, IServiceScope> _jobScopes;

        public QuartzJobFactory(IServiceProvider container)
        {
            this._container = container;
            _jobScopes = new Dictionary<IJob, IServiceScope>();
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var scope = _container.CreateScope();
            var job = scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
            _jobScopes.Add(job, scope);
            return job;
        }

        public void ReturnJob(IJob job)
        {
            // Dipose of the scope for this job
            _jobScopes[job].Dispose();
            // if the job implements IDisposable, dispose of it when job is over
            (job as IDisposable)?.Dispose();
        }
    }
}
