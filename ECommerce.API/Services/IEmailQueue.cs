using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.API.Services
{
    public interface IEmailQueue
    {
        Task QueueEmailAsync(string to, string subject, string body, CancellationToken cancellationToken);
    }
}
