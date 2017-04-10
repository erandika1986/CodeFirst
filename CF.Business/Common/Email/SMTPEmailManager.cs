using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Common
{
    public static class SMTPEmailManager
    {
        public static async Task<bool> SendEmailAsync(EmailMessage email)
        {
            return await SendSMTPEmailAsync(email);
        }

        private static async Task<bool> SendSMTPEmailAsync(EmailMessage email)
        {
            try
            {

                MailMessage message = new MailMessage();
                message.From = new MailAddress(Constants.SmtpServerEmail, "CF System");
                email.ToUsersEmail.ForEach(t =>
                {
                    message.To.Add(t);
                });
                message.Subject = email.Subject;
                message.Body = email.Body;
                message.IsBodyHtml = email.IsBodyHtml;

                SmtpClient client = new SmtpClient(Constants.SmtpServer);
                if (Constants.SmtpServerPort > 0)
                {
                    client.Port = Constants.SmtpServerPort;
                }
                if (Constants.NetworkCredentialIsRequired)
                {
                    client.Credentials = new NetworkCredential(Constants.SmtpServerEmail, Constants.SmtpServerEmailPassword);
                }

                client.EnableSsl = Constants.EnableSsl;
                await client.SendMailAsync(message);

                message.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //private static async Task<bool> SendGridEmailAsync(EmailMessage email)
        //{
        //    try
        //    {
        //        var message = new SendGridMessage();
        //        foreach (var destination in email.ToUsersEmail)
        //        {
        //            message.AddTo(destination);
        //        }

        //        message.From = new System.Net.Mail.MailAddress(
        //                            Constants.SmtpServerEmail, "CF System");
        //        message.Subject = email.Subject;
        //        message.Text = email.Body;
        //        message.Html = email.Body;

        //        var credentials = new NetworkCredential(
        //                   Constants.GridEmailAccount,
        //                   Constants.GridEmailPassword
        //                   );

        //        // Create a Web transport for sending email.
        //        var transportWeb = new SendGrid.Web(credentials);

        //        // Send the email.
        //        if (transportWeb != null)
        //        {
        //            await transportWeb.DeliverAsync(message);
        //        }
        //        else
        //        {
        //            Trace.TraceError("Failed to create Web transport.");
        //            await Task.FromResult(0);
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

    }
}
