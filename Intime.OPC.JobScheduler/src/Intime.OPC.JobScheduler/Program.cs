using System;
using System.ServiceProcess;

namespace Intime.OPC.JobScheduler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(String[] args)
        {
            if (args.Length > 0)
            {
                var job = new MainJobService();
                job.Start();
                Console.ReadLine();
            }
            else
            {

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new MainJobService()
                };
                ServiceBase.Run(ServicesToRun);
            }


        }
    }
}
