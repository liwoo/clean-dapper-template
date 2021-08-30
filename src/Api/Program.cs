using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;

namespace Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory())?.FullName}/config")
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration, sectionName: "Serilog")
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostContext, config) => { config.ReadFrom.Configuration(hostContext.Configuration); })
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    builder.SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory())?.FullName}/config");
                    builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
                    }

                    if (hostContext.HostingEnvironment.IsEnvironment("testing"))
                    {
                        builder.AddJsonFile("appsettings.Testing.json", optional: true, reloadOnChange: true);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}