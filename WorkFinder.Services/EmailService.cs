using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implemenation for Emails
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Send password reset email link to given email address
        /// </summary>
        /// <param name="to"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public async Task SendPasswordResetEmail(string to, string link)
        {
            //message to sent
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("WorkFinder(No-Reply)", "nimrasajid8903@gmail.com"));
            message.To.Add(new MailboxAddress("",to));
            message.Subject = "Reset Password";

            message.Body = new TextPart("html")
            {
                Text = $"<p>Link will expired in 30 mins. Click the link to reset your password:</p><a href = '{link}' > Reset Password </ a > "
            };

            //connecting with smtp and sending email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("nimrasajid8903@gmail.com", "msahhivtkurcaqxk");
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
