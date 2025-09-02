namespace ECommerce.Infrastructure
{
    public class EmailService : IEmailService
    {
        // Stored procedure name for sending email
        private const string SendEmailSpName = "sp_SendEmail";

        public void SendEmail(string to, string subject, string body)
        {
            // Dapper usage will be implemented here
            // Example: Call stored procedure SendEmailSpName with parameters
        }
    }
}
