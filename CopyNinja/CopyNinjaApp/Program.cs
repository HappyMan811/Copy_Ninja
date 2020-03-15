using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CopyNinjaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"ServerRegistrationManager.exe";

            var managerInstall = @"install CopyNinjaExtension.dll -codebase";

            var managerUnInstall = @"uninstall CopyNinjaExtension.dll";

            ServerRegistrationManager(path, managerUnInstall);

            ServerRegistrationManager(path, managerInstall);

            var json = File.ReadAllText("config.json");

            var objConfig = (Config)JsonConvert.DeserializeObject(json, typeof(Config));

            using (WebApp.Start<Startup>(objConfig.Url))
            {
                Console.WriteLine("Web Server is running.");
                Console.WriteLine("enter exit to close");
                Console.ReadLine();
            }

            ServerRegistrationManager(path, managerUnInstall);

            Thread.Sleep(1000);
        }


        private static void ServerRegistrationManager(string path, string args)
        {
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = path,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = args
            };

            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                process.Close();
            }
        }
    }
}
