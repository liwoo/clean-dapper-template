using System.Reflection;
using Application.Common.Interfaces;
using FluentMigrator.Runner;
using Infrastructure.Email;
using Infrastructure.HttpClient;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<ISendEmails, FakeEmailClient>();
            services.AddSingleton<IHttpClient, FakeHttpClient>();
            services.AddSingleton<ITeamRepository, DapperTeamRepository>();

            services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddSQLite()
                        .WithGlobalConnectionString(configuration.GetConnectionString("Database"))
                        .ScanIn(Assembly.GetExecutingAssembly()).For.All()
                ).AddLogging(config => config.AddFluentMigratorConsole());

            return services;
        }
    }
}