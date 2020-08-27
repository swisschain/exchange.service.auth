using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace Swisschain.Auth.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.ConfigureKestrel(options =>
                        {
                            options.Listen(IPAddress.Any, 5000, o => o.Protocols =
                                HttpProtocols.Http1);
                            options.Listen(IPAddress.Any, 5001, o => o.Protocols =
                                HttpProtocols.Http2);
                        });
                        webBuilder.UseUrls("http://*:5000", "http://*:5001");
                        webBuilder.UseStartup<Startup>();
                    });
    }
}
