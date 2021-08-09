using System.Threading.Tasks;
using Application.Common.DTOs;

namespace Application.Common.Interfaces
{
    public interface ISendEmails
    {
        public Task SendEmail(EmailDto emailDto);
    }
}