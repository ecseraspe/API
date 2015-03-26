// ---------------------------------------------------------------------------------------------------
// <copyright file="NotificationLogDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-8</date>
// <summary>
//     The NotificationLogDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;

    /// <summary>
    /// Class NotificationLogDto
    /// </summary>
    public class NotificationLogDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationLogDto"/> class.
        /// </summary>
        public NotificationLogDto()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsSuccess = true;
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
