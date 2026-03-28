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
            message.From.Add(new MailboxAddress("Initti(No-Reply)", "nimrasajid8903@gmail.com"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = "Reset Your Password - Initti";

            message.Body = new TextPart("html")
            {
                //Text = $"<p>Link will expired in 30 mins. Click the link to reset your password:</p><a href = '{link}' > Reset Password </ a > "
                Text = $@"
    <div style='font-family: sans-serif; max-width: 600px; color: #333;'>
        <h2>Password Reset Request</h2>
        <p>Hello,</p>
        <p>We received a request to reset the password for your account. Click the button below to choose a new password:</p>
        
        <div style='margin: 30px 0;'>
            <a href='{link}' 
               style='background-color: #000; color: #fff; padding: 12px 25px; text-decoration: none; border-radius: 50px; font-weight: bold;'>
               Reset Password
            </a>
        </div>

        <p style='font-size: 0.9em; color: #666;'>
            <strong>Note:</strong> This link will expire in 30 minutes.
        </p>
        <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;' />
        <p style='font-size: 0.8em; color: #999;'>
            If you did not request a password reset, please ignore this email or contact support if you have concerns.
        </p>
    </div>"
            };

            //connecting with smtp and sending email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("nimrasajid8903@gmail.com", "msahhivtkurcaqxk");
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendVerificationEmail(string to, string link)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Initti(No-Reply)", "nimrasajid8903@gmail.com"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = "Verify Account - Initti";

            message.Body = new TextPart("html")
            {
                //Text = $"<p>Link will expired in 30 mins. Click the link to reset your password:</p><a href = '{link}' > Reset Password </ a > "
                Text = $@"<!DOCTYPE html>
<html>
<head>
    <style>
        .email-container {{ font-family: 'Segoe UI', Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e2e8f0; border-radius: 8px; overflow: hidden; }}
        .header {{ background-color: #0a66c2; color: white; padding: 25px; text-align: center; }}
        .body {{ padding: 30px; line-height: 1.6; color: #334155; }}
        .button {{ display: inline-block; padding: 14px 28px; background-color: #0a66c2; color: white !important; text-decoration: none; border-radius: 4px; font-weight: bold; margin-top: 20px; }}
        .footer {{ background-color: #f8fafc; padding: 15px; text-align: center; font-size: 12px; color: #64748b; }}
        .highlight {{ color: #0a66c2; font-weight: 600; }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <div class=""header"">
            <h1 style=""margin:0;"">Verify your account</h1>
        </div>
        <div class=""body"">
            <p>Hello <strong>{to}</strong>,</p>
            <p>Thank you for joining our platform! We are excited to have you on board.</p>
            <p>To complete your registration and <span class=""highlight"">verify your account</span>, please proceed with choosing a payment plan. Activating a plan ensures you have uninterrupted access to our professional features and secure services.</p>
            <div style=""text-align: center;"">
                <a href=""{link}"" class=""button"">Verify Account</a>
            </div>
            <p style=""margin-top: 25px; font-size: 0.9rem; color: #64748b;"">
                If you did not create an account using this email address, please ignore this message.
            </p>
        </div>
        <div class=""footer"">
            &copy; 2026 Initti. All rights reserved. <br>
            Verification pending for: {to}
        </div>
    </div>
</body>
</html>"
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
