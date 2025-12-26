using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.Service.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailCofiguration _EmailConfig;
        public EmailSender(EmailCofiguration config)
        {
            _EmailConfig = config;
        }


        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            await SendMessage(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", _EmailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder()
            {
                HtmlBody = message.Content,
                TextBody = "Please view this email in HTML mode."
            };


            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content};
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }

        private async Task SendMessage(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_EmailConfig.SmtpServer, _EmailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_EmailConfig.UserName, _EmailConfig.Password);

                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {

                    throw new Exception("Email send Failed", ex);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }

            }

        }
    }
}