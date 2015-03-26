// ---------------------------------------------------------------------------------------------------
// <copyright file="IPushMessageService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-10</date>
// <summary>
//     The IPushMessageService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.Notification
{
    using System.Collections.Generic;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The IpushService interface
    /// </summary>
    public interface IPushMessageService
    {
        /// <summary>
        /// Send Message Notification to Android Device
        /// </summary>
        /// <param name="gcmId">The GCMID </param>
        /// <param name="messageId">The Message Id</param>
        /// <param name="message">The Message Text</param>
        /// <param name="senderName">The sender name</param>
        /// <param name="type">The type of notification</param>
        /// <returns>bool object</returns>
        bool SendMessageNotificationToAndroid(string gcmId, string messageId, string message, string senderName, string type = "");

        /// <summary>
        /// Sends the report user notification to android.
        /// </summary>
        /// <param name="gcmId">The GCM identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        bool SendReportUserNotificationToAndroid(string gcmId, string userId, string username, string message, string type = "");

        /// <summary>
        /// Sends the report company notification to android.
        /// </summary>
        /// <param name="gcmId">The GCM identifier.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="companyname">The companyname.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        bool SendReportCompanyNotificationToAndroid(string gcmId, string companyId, string companyname, string message, string type = "");

        /// <summary>
        /// Sends the not call again notification to android.
        /// </summary>
        /// <param name="gcmId">The GCM identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        bool SendNotCallAgainNotificationToAndroid(string gcmId, string userId, string username, string message, string type = "");

        /// <summary>
        /// Sends the mark purchased notification to android.
        /// </summary>
        /// <param name="gcmId">The GCM identifier.</param>
        /// <param name="markedByName">Name of the marked by.</param>
        /// <param name="markedById">The marked by identifier.</param>
        /// <param name="markedToName">Name of the marked to.</param>
        /// <param name="markedToId">The marked to identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        bool SendMarkPurchasedNotificationToAndroid(string gcmId, string markedByName, string markedById, string markedToName, string markedToId, string interest, string message, string type = "");

        /// <summary>
        /// Sends the rating notification to android.
        /// </summary>
        /// <param name="gcmId">The GCM Id.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="companyname">The companyname.</param>
        /// <param name="message">The message.</param>
        /// <param name="rating">The rating.</param>
        /// <param name="type">The type.</param>
        /// <returns>Boolean object.</returns>
        bool SendRatingNotificationToAndroid(string gcmId, string companyId, string companyname, string message, decimal rating, string type = "");

        /// <summary>
        /// Sends the purchased notification to android.
        /// </summary>
        /// <param name="gcmId">The GCM Id.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="companyname">The companyname.</param>
        /// <param name="interest">The interest.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>Boolean object.</returns>
        bool SendPurchasedNotificationToAndroid(string gcmId, string companyId, string companyname, string interest, string message, string type = "");

        /// <summary>
        /// Send Credit Notification to Android Device
        /// </summary>
        /// <param name="gcmId">The GCMID </param>
        /// <param name="messageId">The Message Id</param>
        /// <param name="amount">The Amount</param>
        /// <param name="senderName">The sender name</param>
        /// <param name="type">The type of notification</param>
        /// <returns>bool object</returns>
        bool SendCreditNotificationToAndroid(string gcmId, string messageId, string amount, string senderName, string type = "");

        /// <summary>
        /// Sends the message notification to iOS.
        /// </summary>
        /// <param name="udId">The ud identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="message">The message.</param>
        /// <param name="senderName">The sender name.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="type">The type.</param>
        /// <returns>Boolean object.</returns>
        bool SendMessageNotificationToiOS(string udId, string messageId, string message, string senderName, int badge, string type = "");

        /// <summary>
        /// Sends the report user notification to iOS.
        /// </summary>
        /// <param name="udId">The UDID.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        bool SendReportUserNotificationToiOS(string udId, string userId, string username, string message, string type = "");

        /// <summary>
        /// Sends the report company notification to iOS.
        /// </summary>
        /// <param name="udId">The UDID.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="companyname">The companyname.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        bool SendReportCompanyNotificationToiOS(string udId, string companyId, string companyname, string message, string type = "");

        /// <summary>
        /// Sends the not call again notification to iOS.
        /// </summary>
        /// <param name="udId">The UDID.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        bool SendNotCallAgainNotificationToiOS(string udId, string userId, string username, string message, string type = "");

        /// <summary>
        /// Sends the mark purchased notification to iOS.
        /// </summary>
        /// <param name="udId">The UDID.</param>
        /// <param name="markedByName">Name of the marked by.</param>
        /// <param name="markedById">The marked by identifier.</param>
        /// <param name="markedToName">Name of the marked to.</param>
        /// <param name="markedToId">The marked to identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        bool SendMarkPurchasedNotificationToiOS(string udId, string markedByName, string markedById, string markedToName, string markedToId, string interest, string message, string type = "");

        /// <summary>
        /// Sends the rating notification to iOS.
        /// </summary>
        /// <param name="udId">The UDID.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="companyname">The companyname.</param>
        /// <param name="message">The message.</param>
        /// <param name="rating">The rating.</param>
        /// <param name="type">The type.</param>
        /// <returns>Boolean object.</returns>
        bool SendRatingNotificationToiOS(string udId, string companyId, string companyname, string message, decimal rating, string type = "");

        /// <summary>
        /// Sends the purchased notification to iOS.
        /// </summary>
        /// <param name="udId">The UDID.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="companyname">The companyname.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>Boolean object.</returns>
        bool SendPurchasedNotificationToiOS(string udId, string companyId, string companyname, string message, string type = "");

        /// <summary>
        /// Sends the credit notification to ios.
        /// </summary>
        /// <param name="udId">The ud identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="senderName">Name of the sender.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="type">The type.</param>
        /// <returns>Boolean object.</returns>
        bool SendCreditNotificationToiOS(string udId, string messageId, string amount, string senderName, int badge, string type = "");

        /// <summary>
        /// Saves the notification log.
        /// </summary>
        /// <param name="notificationLogDto">The notification log dto.</param>
        /// <returns>NotificationLogDto object.</returns>
        NotificationLogDto SaveNotificationLog(NotificationLogDto notificationLogDto);
    }
}
