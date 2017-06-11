using Autofac;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace AppChat.SignalRHostSelf
{
    public class SignalRService : ServiceControl
    {
        private IContainer _container;
        public SignalRService()
        {
            // Dependency Registrer
            var builder = new ContainerBuilder();

            WebApp.Start<Startup>("http://localhost:8050");

            var container = builder.Build();

            _container = container;
        }


        public bool Start(HostControl hostControl)
        {
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            return true;
        }
    }
}
