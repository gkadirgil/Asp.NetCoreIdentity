using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.Helper
{
    public static class EmailConfirmation
    {
        public static void EmailConfirmSendEmail(string link, string email)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtp = new SmtpClient();

            mail.From = new MailAddress("mymail@mail.com");
            mail.To.Add(email);
            mail.Subject = "Email Doğrulama";
            mail.Body = "<h2>Email adresinizi doğrulamak için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
            mail.Body += $"<a href='{link}'>şifre yenileme linki</a>";
            mail.IsBodyHtml = true;

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("mymail@mail.com", "abc123");

            smtp.Send(mail);
        }
    }
}
