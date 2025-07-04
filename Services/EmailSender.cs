using Microsoft.AspNetCore.Identity.UI.Services;

namespace uni_project_chat_app.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // For development purposes, we'll just log the email instead of sending it
            _logger.LogInformation("Email would be sent to: {Email}", email);
            _logger.LogInformation("Subject: {Subject}", subject);
            _logger.LogInformation("Message: {Message}", htmlMessage);
            
            return Task.CompletedTask;
        }
    }
} 