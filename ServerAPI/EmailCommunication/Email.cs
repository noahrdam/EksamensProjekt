using System.Net.Mail;
using System.Net;

namespace ServerAPI.EmailCommunication
{
    public class Email : IEmail
    {
        public void SendEmail(string toEmail, string subject, string body)
        {
            string fromMail = "testkodemail@gmail.com";
            string fromPassword = "ktwk jykf bmvp ahyd";
            string smtpServer = "smtp.gmail.com";
            int port = 587;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromMail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(smtpServer)
            {
                Port = port,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtp.Send(mail);
        }
    }
}
