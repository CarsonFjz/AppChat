using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.SignalRHostSelf
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:8050"))
            {
                Console.WriteLine("Server running ok");
                Console.ReadLine();
            }
        }
    }
}
