using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ECommerce.API.Services
{
    public class EmailQueue : IEmailQueue
    {
        private readonly Channel<EmailMessage> _queue = Channel.CreateUnbounded<EmailMessage>();

        public async Task QueueEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
        {
            await _queue.Writer.WriteAsync(new EmailMessage(to, subject, body), cancellationToken);
        }

        public ChannelReader<EmailMessage> Reader => _queue.Reader;
    }

    public record EmailMessage(string To, string Subject, string Body);
}
