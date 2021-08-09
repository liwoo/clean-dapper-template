using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Email
{
    public class FakeEmailClient : ISendEmails
    {
        private readonly ILogger<FakeEmailClient> _logger;

        public FakeEmailClient(ILogger<FakeEmailClient> logger)
        {
            _logger = logger;
        }

        public Task SendEmail(EmailDto emailDto)
        {
            _logger.LogInformation("Sending {@Email}...", emailDto);
            return Task.CompletedTask;
        }
    }
}