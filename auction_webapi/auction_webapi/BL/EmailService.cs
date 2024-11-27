using auction_webapi.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace auction_webapi.BL
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    client.EnableSsl = _emailSettings.EnableSsl;

                    var mail = new MailMessage();
                    mail.From = new MailAddress(_emailSettings.Username);
                    mail.To.Add(new MailAddress(recipientEmail));
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    await client.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw;
            }
        }
    }
    }
