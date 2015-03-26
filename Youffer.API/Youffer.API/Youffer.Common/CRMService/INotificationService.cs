// ---------------------------------------------------------------------------------------------------
// <copyright file="INotificationService.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-14</date>
// <summary>
//     The INotificationService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.CRMService
{
    using Youffer.CRM;

    /// <summary>
    /// Interface INotificationService
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Create Notification
        /// </summary>
        /// <param name="notification">The VTigerNotifications model</param>
        /// <returns>The VTigerNotifications object</returns>
        VTigerNotifications CreateNotification(VTigerNotifications notification);
    }
}
