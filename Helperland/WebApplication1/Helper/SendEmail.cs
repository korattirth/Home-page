using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;

namespace WebApplication1.Helper
{
    public class SendEmail
    {
        public void SendMail(string To ,string subject, string body)
        {
                var host = "smtp.gmail.com";
                var port = 587;
                var username = "korattirth2001@gmail.com";
                var password = "korat@2001";
                var enable = true;

            var smtpClient = new System.Net.Mail.SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = enable,
                Credentials = new NetworkCredential(username, password)
            };

             MailMessage obj = new MailMessage("korattirth2001@gmail.com",To);
                obj.Subject = subject;
                obj.Body = body;
                obj.IsBodyHtml = true;
                smtpClient.Send(obj);
        }
    }
}
