using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Pizza_Bot
{
    public class Mail
    {
        private readonly string _mailAddressTo;
        private readonly string _mailAddressFrom = "pizzabot675@gmail.com";
        private readonly string _password = "A987654321";
        private readonly string _displayName = "PIZZA_BOT";
        private readonly string _messageSubject;
        private readonly string _messageBody;
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;

        public Mail(string emailAddress, string messageSubject, string messageBody)
        {
            _mailAddressTo = emailAddress;
            _messageSubject = messageSubject;
            _messageBody = messageBody;
        }

        public void Send()
        {
            MailAddress from = new MailAddress(_mailAddressFrom, _displayName);
            MailAddress to = new MailAddress(_mailAddressTo);

            MailMessage message = new MailMessage(from, to);
            message.Subject = _messageSubject;
            message.Body = _messageBody;

            using (SmtpClient smtp = new SmtpClient(_smtpHost, _smtpPort))
            {
                smtp.Credentials = new NetworkCredential(_mailAddressFrom, _password);
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }
    }
}
