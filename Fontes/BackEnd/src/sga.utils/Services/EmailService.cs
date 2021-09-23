using Microsoft.Extensions.Options;
using sga.utils.Interfaces;
using sga.utils.models;
using sga.utils.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace sga.utils
{
    public class EmailService : IEmailSender
    {
        private readonly EmailSettings _EmailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _EmailSettings = emailSettings.Value;
        }

        public string PrepareEmail(string _pathHtml, List<FieldEmail> fieldsEmail)
        {
            System.Text.StringBuilder htmlContent = new System.Text.StringBuilder();
            string line;
            try
            {
                using (System.IO.StreamReader htmlReader = new System.IO.StreamReader(_EmailSettings.FolderTemplate + _pathHtml))
                {
                    while ((line = htmlReader.ReadLine()) != null)
                    {
                        htmlContent.Append(line);
                    }
                }

                foreach (var fieldEmail in fieldsEmail)
                {
                    htmlContent = htmlContent.Replace(fieldEmail.Field, fieldEmail.Value);
                }
                return Convert.ToString(htmlContent);
            }
            catch (Exception objError)
            {
                throw objError;
            }
        }

        public async Task SendEmailAsync(string subject, string recipientName,
                string recipientEmail, string body)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = _EmailSettings.Domain;
            client.EnableSsl = _EmailSettings.Ssl;
            client.Port = _EmailSettings.Port;
            client.Credentials = new System.Net.NetworkCredential(_EmailSettings.UserName,
                                                                    Generator.Base64Decode(_EmailSettings.Password));
            MailMessage mail = new MailMessage();
            mail.Sender = new System.Net.Mail.MailAddress(_EmailSettings.SenderEmail, _EmailSettings.SenderName);
            mail.From = new MailAddress(_EmailSettings.SenderEmail, _EmailSettings.SenderName);
            mail.To.Add(new MailAddress(recipientEmail, recipientName));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                mail = null;
                throw ex;
            }
            finally
            {
                mail = null;
            }
        }
    }
}