using System.Threading;
using System.Threading.Tasks;
using ECommerce.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly EmailQueue _emailQueue;
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailBackgroundService> _logger;

        public EmailBackgroundService(EmailQueue emailQueue, IEmailService emailService, ILogger<EmailBackgroundService> logger)
        {
            _emailQueue = emailQueue;
            _emailService = emailService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var message in _emailQueue.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await _emailService.SendEmailAsync(message.To, message.Subject, message.Body, stoppingToken);
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Failed to send email to {Recipient}", message.To);
                }
            }
        }
    }
}
