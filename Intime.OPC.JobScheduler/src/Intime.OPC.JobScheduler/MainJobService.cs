using System;
using System.ServiceProcess;
using Quartz;
using Quartz.Impl;

namespace Intime.OPC.JobScheduler
{
    public partial class MainJobService : ServiceBase
    {
        private IScheduler _scheduler;

        public MainJobService()
        {
            InitializeComponent();
        }

        public void Start()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = factory.GetScheduler();
            _scheduler.Start();
        }

        protected override void OnStop()
        {
            if (_scheduler != null &&
                !_scheduler.IsShutdown)
            {
                _scheduler.Shutdown();
            }
        }
    }
}
