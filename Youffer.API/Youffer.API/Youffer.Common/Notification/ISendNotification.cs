// ---------------------------------------------------------------------------------------------------
// <copyright file="ISendNotification.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-23</date>
// <summary>
//     The ISendNotification class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface ISendNotification
    /// </summary>
    public interface ISendNotification
    {
        /// <summary>
        /// Sends the mark purchased notification.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="message">The message.</param>
        void SendMarkPurchasedNotification(string contactId, string message);
    }
}
