using ECommerce.Infrastructure;
using Xunit;

namespace ECommerce.Test.Unit
{
    public class EmailServiceTests
    {
        [Fact]
        public async System.Threading.Tasks.Task SendEmail_DoesNotThrow()
        {
            var service = new EmailService();
            var ex = await Record.ExceptionAsync(() => service.SendEmailAsync("to@test.com", "subject", "body"));
            Assert.Null(ex);
        }
    }
}
