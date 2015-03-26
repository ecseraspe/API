// ---------------------------------------------------------------------------------------------------
// <copyright file="EmailQueue.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The EmailQueue class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Youffer.Resources.Enum;

    /// <summary>
    /// The email queue.
    /// </summary>
    [Table("EmailQueue")]
    public class EmailQueue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailQueue"/> class.
        /// </summary>
        public EmailQueue()
        {
            this.Created = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;

            // 0 = SMTP, 1 = ManDrill, 2 = MailChimp (not in current implementation)
            this.SendVia = (int)EmailSendVia.Smtp;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the from email id.
        /// </summary>
        public string FromEmailId { get; set; }

        /// <summary>
        /// Gets or sets the from name.
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        public string EmailSubject { get; set; }

        /// <summary>
        /// Gets or sets the send via.
        /// </summary>
        public short SendVia { get; set; }

        /// <summary>
        /// Gets or sets the target user id.
        /// </summary>
        public string TargetUserId { get; set; }

        /// <summary>
        /// Gets or sets the target email id.
        /// </summary>
        public string TargetEmailId { get; set; }

        /// <summary>
        /// Gets or sets the mail chimp template id.
        /// </summary>
        public long MailChimpTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the email html.
        /// </summary>
        public string EmailHTML { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is processed.
        /// </summary>
        public bool IsProcessed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is sent.
        /// </summary>
        public bool IsSent { get; set; }
    }
}
