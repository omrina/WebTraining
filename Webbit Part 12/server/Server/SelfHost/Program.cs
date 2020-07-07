using System;
using System.Configuration;
using Microsoft.Owin.Hosting;
using SelfHost.Resources;
using Server;

namespace SelfHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var hostAddress = ConfigurationManager.AppSettings["HostAddress"];

                using (WebApp.Start<Startup>(hostAddress))
                {
                    Console.WriteLine(ServerConsoleMessages.ServerRunning, hostAddress);
                    Console.ReadLine();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(ServerConsoleMessages.ServerHasCrashed);
                // LOG HERE
            }
        }
    }
}
