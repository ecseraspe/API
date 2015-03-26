// ---------------------------------------------------------------------------------------------------
// <copyright file="CRMNotifications.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-14</date>
// <summary>
//     The CRMNotifications class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.CRMModel
{
    using System;

    /// <summary>
    /// Class CRMNotifications
    /// </summary>
    public class CRMNotifications : BaseCrmModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CRMNotifications" /> class.
        /// </summary>
        public CRMNotifications()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the notification no.
        /// </summary>
        public string NotificationNo { get; set; }

        /// <summary>
        /// Gets or sets the contact id
        /// </summary>
        public string ContactId { get; set; }

        /// <summary>
        /// Gets or sets the type of the notification.
        /// </summary>
        public string NotificationType { get; set; }

        /// <summary>
        /// Gets or sets the organisationId
        /// </summary>
        public string OrganisationsId { get; set; }

        /// <summary>
        /// Gets or sets the created on
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Modified on
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
