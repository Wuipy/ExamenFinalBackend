using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LibraryService.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        config.AddUserSecrets<Program>(optional: true);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    var port = Environment.GetEnvironmentVariable("PORT");
                    if (!string.IsNullOrWhiteSpace(port))
                    {
                        webBuilder.UseUrls($"http://0.0.0.0:{port}");
                    }
                });
    }
}
