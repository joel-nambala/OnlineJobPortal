using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;

namespace OnlineJobPortal
{
    public class Components
    {
        public static void SendEmailAlerts(string recipient, string subject, string body)
        {
            try
            {
                string senderEmail = "wanjala.n.joel@gmail.com";
                string senderPassword = "sjxo tphf xoxg zsgx";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(senderEmail);
                message.Subject = subject;
                message.To.Add(new MailAddress(recipient));
                message.Body = $"<html><body>{body}</body></html>";
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 25;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        public static bool ValidatePassword(string password)
        {
            bool b = false;
            string pattern = "908hjj";
            if (Regex.IsMatch(password, pattern))
            {
                b = true;
            }
            return b;
        }
    }
}