using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace AppChat.SignalRHostSelf
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<SignalRService>();
                x.RunAsLocalSystem();

                x.SetDescription("SignalR Service");
                x.SetDisplayName("SignalR Service");
                x.SetServiceName("SignalR");
            });
            Console.ReadLine();
        }
    }

    
}
