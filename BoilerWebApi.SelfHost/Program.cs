using Microsoft.Owin.Hosting;
using System;

namespace BoilerWebApi.SelfHost
{
    internal static class Program
    {
        private const string Scheme = "http://";
        private const string Server = "localhost";
        private const string Port = "8080";

        private static void Main()
        {
            var webSite = string.Format("{0}{1}:{2}", Scheme, Server, Port);
            using (WebApp.Start<Startup>(webSite))
            {
                Console.WriteLine("Web Server is running on port: " + Port);
                Console.WriteLine("Press[ENTER] to quit.");
                Console.ReadLine();
            }
        }
    }
}
