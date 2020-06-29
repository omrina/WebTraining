﻿using System;
using System.Configuration;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Logging;
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
                // Console.WriteLine(exception.Message);
                // TODO: logger!?
                LoggerFactory.Default.Create("Asd").WriteCritical(exception.Message, exception);
            }
            
        }
    }
}