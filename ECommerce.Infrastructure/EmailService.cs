using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure
{
    public class EmailService : IEmailService
    {
        // Stored procedure name for sending email
        private const string SendEmailSpName = "sp_SendEmail";

        public Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
        {
            // Dapper usage will be implemented here
            // Example: Call stored procedure SendEmailSpName with parameters
            return Task.CompletedTask;
        }
    }
}
