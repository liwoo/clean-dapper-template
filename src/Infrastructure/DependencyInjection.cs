using Application.Common.Interfaces;
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
            return services;
        }
    }
}