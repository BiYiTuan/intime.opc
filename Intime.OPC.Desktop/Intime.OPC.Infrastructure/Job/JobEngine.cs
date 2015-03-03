using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using Intime.OPC.Domain;
using Intime.OPC.Infrastructure.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using OPCAPP.Common.Extensions;
using Quartz;
using Quartz.Impl;

namespace Intime.OPC.Infrastructure.Job
{
    [Export(typeof(JobEngine))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class JobEngine
    {
        [Import]
        private GlobalEventAggregator _eventAggregator;
        [ImportMany]
        private IEnumerable<IJob> _jobs { get; set; }
        private ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

        public void Start()
        {
            _schedulerFactory = new StdSchedulerFactory();
            _scheduler = _schedulerFactory.GetScheduler();

            SubscribeEvents();
        }

        public void Shutdown()
        {
            if (_scheduler != null && !_scheduler.IsShutdown)
            {
                _scheduler.PauseAll();
                _scheduler.Shutdown();
            }
        }

        private void StartJobs(IEnumerable<MenuGroup> authorizedMenuGroups)
        {
            foreach (var job in _jobs)
            {
                var attribute = job.GetType().GetCustomAttribute<JobHookAttribute>();
                if (attribute == null) continue;

                foreach (var authorizedMenuGroup in authorizedMenuGroups)
                {
                    var authorizedMenus = authorizedMenuGroup.Items;
                    foreach (var authorizedMenu in authorizedMenus)
                    {
                        if (string.Compare(attribute.MatchedAuthorizedViewName, authorizedMenu.Url, true, CultureInfo.InvariantCulture) == 0)
                        {
                            var jobDetail = JobBuilder.Create(job.GetType()).WithIdentity(job.ToString()).Build();
                            jobDetail.JobDataMap.Add("AuthorizedMenu", authorizedMenu);

                            var trigger = TriggerBuilder.Create()
                                            .WithIdentity(string.Format("Trigger{0}", job))
                                            .WithCronSchedule(string.Format("0 0/{0} * * * ?", attribute.Interval))
                                            .StartNow()
                                            .Build();

                            _scheduler.ScheduleJob(jobDetail, trigger);
                        }
                    }
                }
            }

            _scheduler.Start();
        }

        private void SubscribeEvents()
        {
            var authenticationEvent = _eventAggregator.GetEvent<AuthorizedFeatureRetrievedEvent>();
            authenticationEvent.Subscribe(OnAuthorizedFeatureRetrieved, ThreadOption.BackgroundThread);
        }

        private void OnAuthorizedFeatureRetrieved(IEnumerable<MenuGroup> authorizedMenuGroups)
        {
            if (_scheduler != null && _scheduler.IsStarted && !_scheduler.IsShutdown)
            {
                _scheduler.PauseAll();
                _scheduler.Shutdown();
            }

            if (authorizedMenuGroups != null)
            {
                _scheduler = _schedulerFactory.GetScheduler();
                StartJobs(authorizedMenuGroups);
            }
        }
    }
}
