// ---------------------------------------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-14</date>
// <summary>
//     The NotificationService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data.CRMService
{
    using System;
    using Youffer.Common.CRMService;
    using Youffer.Common.LogService;
    using Youffer.CRM;

    /// <summary>
    /// Class NotificationService
    /// </summary>
    public class NotificationService : INotificationService
    {
        /// <summary>
        /// VTiger service instance
        /// </summary>
        private readonly IVTigerService vTigerService;

        /// <summary>
        ///  Initializes a new instance of the <see cref="NotificationService" /> class.
        /// </summary>
        /// <param name="vTigerService">the vtiger service</param>
        /// <param name="loggerService">the logger service</param>
        public NotificationService(IVTigerService vTigerService, ILoggerService loggerService)
        {
            this.vTigerService = vTigerService;
            this.LoggerService = loggerService;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Create Notification
        /// </summary>
        /// <param name="notification">The VTigerNotifications model</param>
        /// <returns>The VTigerNotifications object</returns>
        public VTigerNotifications CreateNotification(VTigerNotifications notification)
        {
            try
            {
                notification = this.vTigerService.Create<VTigerNotifications>(notification);
                return notification;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Adding Notification :- " + ex.Message);
            }

            return new VTigerNotifications();
        }
    }
}
