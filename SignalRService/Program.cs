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
                x.Service<SignalRService>(s=> 
                {
                    s.ConstructUsing(name => new SignalRService());
                });

            });
        }
    }

    public class SignalRService
    {
        public SignalRService()
        {
            WebApp.Start<Startup>("http://localhost:8050");
            Console.WriteLine("Server running ok");
            Console.ReadLine();
        }
    }
}
