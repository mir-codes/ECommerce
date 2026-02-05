using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Infrastructure
{
    public class EmailService : IEmailService
    {
        private const string SendEmailSpName = "sp_SendEmail";

        public Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            // TODO: Integrate provider (SMTP/SendGrid) or stored procedure call.
            _ = SendEmailSpName;
            return Task.CompletedTask;
        }
    }
}
