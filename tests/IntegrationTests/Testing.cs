using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace IntegrationTests
{
    [SingleThreaded]
    [SetUpFixture]
    public class Testing
    {
        public static IServiceScopeFactory ServiceScopeFactory { get; private set; }
        public static IConfigurationRoot Configuration { get; private set; }

        public static HttpClient Client { get; private set; }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            const string testSettings = "appsettings.Testing.json";
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent;
            if (directoryInfo == null) return;
            var rootDir = $"{directoryInfo?.Parent}/src/config";
            var builder = new ConfigurationBuilder()
                .SetBasePath(rootDir)
                .AddJsonFile(testSettings);

            Configuration = builder.Build();

            var services = new ServiceCollection();

            var startup = new Startup(Configuration);

            services.AddLogging(l => l.AddProvider(NullLoggerProvider.Instance));
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();

            startup.ConfigureServices(services);

            ServiceScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
            Client = new TestServer(new WebHostBuilder()
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    configurationBuilder.SetBasePath(rootDir);
                    configurationBuilder.AddJsonFile(testSettings);
                }).UseStartup<Startup>()).CreateClient();
        }
    }
}