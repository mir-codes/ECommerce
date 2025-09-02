using ECommerce.Infrastructure;
using Xunit;

namespace ECommerce.Test.Unit
{
    public class EmailServiceTests
    {
        [Fact]
        public void SendEmail_DoesNotThrow()
        {
            var service = new EmailService();
            var ex = Record.Exception(() => service.SendEmail("to@test.com", "subject", "body"));
            Assert.Null(ex);
        }
    }
}
