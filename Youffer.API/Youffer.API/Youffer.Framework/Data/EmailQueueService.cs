// ---------------------------------------------------------------------------------------------------
// <copyright file="EmailQueueService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The EmailQueueService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;

    using Youffer.Common.DataService;
    using Youffer.Common.Helper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.Constants;
    using Youffer.Resources.Enum;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The email queue service.
    /// </summary>
    public class EmailQueueService : IEmailQueueService
    {
        /// <summary>
        /// The email queue.
        /// </summary>
        private readonly IRepository<EmailQueue> emailQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailQueueService" /> class.
        /// </summary>
        /// <param name="emailQueue">The email queue.</param>
        public EmailQueueService(IRepository<EmailQueue> emailQueue)
        {
            this.emailQueue = emailQueue;
        }

        /// <summary>
        /// The send activation email.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public bool SendActivationEmail(ApplicationUserDto user)
        {
            try
            {
                var emailQueueItem = new EmailQueue
                                         {
                                             TargetUserId = user.Id,
                                             MailChimpTemplateId = AppSettings.Get<long>("UserActivationMailChimpId", 0),
                                             EmailSubject = AppSettings.Get<string>("UserActivationEmailSubject", "Subject not defined"),
                                             SendVia = (int)EmailSendVia.ManDrill
                                         };

                this.emailQueue.Insert(emailQueueItem);
                return this.emailQueue.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}