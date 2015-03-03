using System.ComponentModel.Composition.Hosting;
using Intime.OPC.Infrastructure.Config;
using Intime.OPC.Infrastructure.Interfaces;
using Intime.OPC.Domain;
using Intime.OPC.Infrastructure.Events;
using Intime.OPC.Infrastructure.Job;

namespace Intime.OPC.Infrastructure
{
    public class AppEx
    {
        private static ILoginManager loginManager;
        public static IConfig Config { get; private set; }
        public static IContainer Container { get; private set; }

        public static ILoginModel LoginModel { get; private set; }

        public static void Init(CompositionContainer container)
        {
            Container = new MefContainer(container);
            loginManager = Container.GetInstance<ILoginManager>();
            Config = new DefaultConfig();

            //Start the job engine.
            Container.GetInstance<JobEngine>().Start();
        }

        public static bool Login(string userName, string password)
        {
            LoginModel = loginManager.Login(userName, password);

            PublishAuthenticationEvent(LoginModel);

            return loginManager.IsLogin;
        }

        public static void Logout()
        {
            loginManager.LogOut();
            PublishAuthenticationEvent(null);
        }

        private static void PublishAuthenticationEvent(ILoginModel loginModel)
        {
            var eventAggregator = Container.GetInstance<GlobalEventAggregator>();
            eventAggregator.GetEvent<AuthenticationEvent>().Publish(loginModel);
        }
    }
}