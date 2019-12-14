using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Quartz;
using Quartz.Impl;
using ShowReminder.Web.Scheduler.Jobs;

namespace ShowReminder.Web.Scheduler
{
    public class QuartzScheduler
    {
        private IScheduler _scheduler;

        private readonly IServiceProvider _container;

        public QuartzScheduler(IServiceProvider container)
        {
            this._container = container;
        }

        public void Start()
        {
            if (null != _scheduler)
            {
                throw new InvalidOperationException("Scheduler is already started.");
            }

            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = new QuartzJobFactory(_container);
            _scheduler.Start().Wait();

            RegisterJobs();
        }

        protected void RegisterJobs()
        {
            var updateExpiredShowsJob = JobBuilder.Create<UpdateExpiredShowsJob>()
                .WithIdentity("update_expired_shows")
                .Build();

            var updatedExpiredShowsTrigger = TriggerBuilder.Create()
                .WithIdentity("update_expired_shows_trigger")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInHours(1).RepeatForever())
                .Build();

            _scheduler.ScheduleJob(updateExpiredShowsJob, updatedExpiredShowsTrigger).Wait();

            var emailRecentlyAiredShowsJob = JobBuilder.Create<SendEmailRecentlyAiredShowsJob>()
                .WithIdentity("send_email_recently_aired_shows")
                .Build();

            var emailRecentlyAriedShowsTrigger = TriggerBuilder.Create()
                .WithIdentity("send_email_recently_aired_shows_trigger")
                .StartNow()
                .WithCronSchedule("0 0 5 * * ?") // everyday at 5 am
                //.WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();

            _scheduler.ScheduleJob(emailRecentlyAiredShowsJob, emailRecentlyAriedShowsTrigger).Wait();

        }

        public void Stop()
        {
            if (null == _scheduler)
            {
                return;
            }
            if (!_scheduler.Shutdown(waitForJobsToComplete: true).Wait(TimeSpan.FromSeconds(30)))
            {
                // didn't shutdown after 30 seconds. force the shutdown
                _scheduler.Shutdown(waitForJobsToComplete: false);
            }
            _scheduler = null;
        }

        public void RunJobNow(Type jobType)
        {
            var job = JobBuilder.Create(jobType).Build();
            var trigger = TriggerBuilder.Create()
                .StartNow()
                .Build();
            _scheduler.ScheduleJob(job, trigger);
        }

    }
}
