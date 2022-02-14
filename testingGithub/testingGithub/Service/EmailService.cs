using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using testingGithub.Models;

namespace testingGithub.Service
{
    public class EmailService
    {

        private readonly SMTPConfigModel _smtpConfig;
        private const string TemplatePath = @"EmailTemplate/{0}.html";
        public EmailService(IOptions<SMTPConfigModel> smtpconfig)
        {
            _smtpConfig = smtpconfig.Value;
        }

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholder("Hello {{UserName}} this is test email subject from book store web app", userEmailOptions.Placeholder);
            userEmailOptions.Body = UpdatePlaceholder(GetEmailBody("TestEmail"),userEmailOptions.Placeholder);
            //userEmailOptions.ToEmails = new List<string>() { "test@gmail.com" }; 

            await SendEmail(userEmailOptions);
        }

        public async Task ConfirmationEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholder("Hello {{UserName}} this is Confirmation Mail from book store web app", userEmailOptions.Placeholder);
            userEmailOptions.Body = UpdatePlaceholder(GetEmailBody("ConfirmationEmail"), userEmailOptions.Placeholder);
            //userEmailOptions.ToEmails = new List<string>() { "test@gmail.com" }; 

            await SendEmail(userEmailOptions);
        }
        

        public async Task ForgetPasswordEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholder("Hello {{UserName}} this is Reset Password link from Facebook Store", userEmailOptions.Placeholder);
            userEmailOptions.Body = UpdatePlaceholder(GetEmailBody("ForgetPasswordUser"), userEmailOptions.Placeholder);
            //userEmailOptions.ToEmails = new List<string>() { "test@gmail.com" }; 

            await SendEmail(userEmailOptions);
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach(var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.Username, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.host,
                Port = _smtpConfig.port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);

        }




        
        private string GetEmailBody(string Templatename)
        {
            var body = File.ReadAllText(string.Format(TemplatePath, Templatename));
            return body;
        }


        public string UpdatePlaceholder(string text,List<KeyValuePair<string,string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach(var val in keyValuePairs)
                {
                    if (text.Contains(val.Key)){
                        text = text.Replace(val.Key, val.Value);
                    }
                }
            }

            return text;
        }




    }
}
