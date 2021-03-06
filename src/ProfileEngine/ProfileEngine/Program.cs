﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace ProfileEngine
{
    static class Program
    {
        static string applicationName = System.Diagnostics.Process.GetCurrentProcess().ProcessName.Replace(".vshost", "");
        static string applicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        static string applicationTitle = "My Hybrid Service";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>        
        static void Main()
        {
            if (Environment.CommandLine.Contains("-service"))
            {
                if (ServiceCheck(true) == false)
                {
                    ServiceController controller = new ServiceController(applicationName);
                    controller.Start();
                    return;
                }

                ServiceBase[] services = new ServiceBase[] { new ServiceWrapper() };
                ServiceBase.Run(services);
            }
            else if (Environment.CommandLine.Contains("-noservice"))
            {
                if (ServiceCheck(false))
                {
                    ServiceController controller = new ServiceController(applicationName);
                    if (controller.Status == ServiceControllerStatus.Running) controller.Stop();
                    ServiceInstaller.UnInstallService(applicationName);
                }
            }
            else
            {
                ConsoleApplication application = new ConsoleApplication(applicationPath, true);
            }
        }

        static bool ServiceCheck(bool autoInstall)
        {
            bool installed = false;

            ServiceController[] controllers = ServiceController.GetServices();
            foreach (ServiceController con in controllers)
            {
                if (con.ServiceName == applicationName)
                {
                    installed = true;
                    break;
                }
            }

            if (installed) return true;

            if (autoInstall)
            {
                ServiceInstaller.InstallService("\"" + applicationPath + "\\" + applicationName + ".exe\" -service", applicationName, applicationTitle, true, false);
            }

            return false;
        }
    }
}