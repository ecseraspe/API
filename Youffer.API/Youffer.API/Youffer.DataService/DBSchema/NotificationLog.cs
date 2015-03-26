// ---------------------------------------------------------------------------------------------------
// <copyright file="NotificationLog.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-8</date>
// <summary>
//     The NotificationLog class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class NotificationLog
    /// </summary>
    [Table("NotificationLog")]
    public class NotificationLog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationLog"/> class.
        /// </summary>
        public NotificationLog()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the repository Id.
        /// </summary>
        [NotMapped]
        public object RepositoryId
        {
            get
            {
                return this.Id;
            }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the sent to identifier.
        /// </summary>
        /// <value>
        /// The sent to identifier.
        /// </value>
        public string SentToId { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the type of the notification.
        /// </summary>
        /// <value>
        /// The type of the notification.
        /// </value>
        public string NotificationType { get; set; }

        /// <summary>
        /// Gets or sets the type of the os.
        /// </summary>
        /// <value>
        /// The type of the os.
        /// </value>
        public int OSType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
