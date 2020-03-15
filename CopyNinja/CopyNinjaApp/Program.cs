using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
            }
        }

        public static int GetAvailablePort(int startingPort)
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();

            //getting active connections
            var tcpConnectionPorts = properties.GetActiveTcpConnections()
                                .Where(n => n.LocalEndPoint.Port >= startingPort)
                                .Select(n => n.LocalEndPoint.Port);

            //getting active tcp listners - WCF service listening in tcp
            var tcpListenerPorts = properties.GetActiveTcpListeners()
                                .Where(n => n.Port >= startingPort)
                                .Select(n => n.Port);

            //getting active udp listeners
            var udpListenerPorts = properties.GetActiveUdpListeners()
                                .Where(n => n.Port >= startingPort)
                                .Select(n => n.Port);

            var port = Enumerable.Range(startingPort, ushort.MaxValue)
                .Where(i => !tcpConnectionPorts.Contains(i))
                .Where(i => !tcpListenerPorts.Contains(i))
                .Where(i => !udpListenerPorts.Contains(i))
                .FirstOrDefault();

            return port;
        }
    }
}
