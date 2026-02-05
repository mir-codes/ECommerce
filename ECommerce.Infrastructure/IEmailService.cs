using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken);
    }
}
