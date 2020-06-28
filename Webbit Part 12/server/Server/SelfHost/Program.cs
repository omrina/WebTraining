using System;
using System.Configuration;
using Microsoft.Owin.Hosting;
using Server;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(ConfigurationManager.AppSettings["HostAddress"]))
            {
                Console.WriteLine($"server listening at port 80");
                Console.ReadLine();
            }
        }
    }
}
