// ---------------------------------------------------------------------------------------------------
// <copyright file="EmailService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2015-01-15</date>
// <summary>
//     The EmailService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Youffer.Common.Helper;
    using Youffer.Resources.Constants;

    /// <summary>
    /// The EmailService
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        /// <summary>
        /// SendAsync email
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>Task obj</returns>
        public Task SendAsync(IdentityMessage message)
        {
            string displayName = AppSettings.Get(ConfigConstants.EmailDisplayName, "Youffer");
            string credentialUserName = AppSettings.Get(ConfigConstants.EmailUserName, "tft.legacy@gmail.com");
            string sentFrom = AppSettings.Get(ConfigConstants.EmailFromName, "youffer@gmail.com");
            string pwd = AppSettings.Get(ConfigConstants.EmailPassword, "tftus@123");
            string smtpClientName = AppSettings.Get(ConfigConstants.SmtpClientName, "smtp.gmail.com");
            int smtpPortNo = AppSettings.Get(ConfigConstants.PortNumber, 587);

            SmtpClient client = new SmtpClient(smtpClientName)
                {
                    Port = smtpPortNo,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                };

            NetworkCredential credentials = new NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            var mail = new MailMessage(new MailAddress(sentFrom, displayName), new MailAddress(message.Destination))
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    Sender = new MailAddress(sentFrom),
                    IsBodyHtml = true
                };

            return client.SendMailAsync(mail);
        }
    }
}
