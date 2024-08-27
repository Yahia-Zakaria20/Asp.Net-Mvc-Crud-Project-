
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Demo.PL.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Net;
using static Demo.PL.Servisec.EmailServisec.EmailSetting;





namespace Demo.PL.Servisec.EmailServisec
{
    public class EmailSetting : IEmailSetting
    {
        #region Test
        private readonly IConfiguration _config;

        public EmailSetting(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task SendEmailAsyn(string to, string Subject, string body)
        {
            var SenderEmail = _config["MailSettings:EmailUsername"];
            var password = _config["MailSettings:EmailPassword"];

            var emailmassage = new MailMessage();
            emailmassage.From = new MailAddress(_config["MailSettings:EmailUsername"], _config["MailSettings:DisplayName"]);
            emailmassage.To.Add(to);
            emailmassage.Subject = Subject;
            emailmassage.Body = $"<html><body>{body}</body></html>";
            emailmassage.IsBodyHtml = true;

            var smtp = new SmtpClient(_config["MailSettings:EmailHost"], 587)
            {
                Credentials = new NetworkCredential(SenderEmail, password),
                EnableSsl = true

            };

            await smtp.SendMailAsync(emailmassage);
        }
        #endregion


    }
}
