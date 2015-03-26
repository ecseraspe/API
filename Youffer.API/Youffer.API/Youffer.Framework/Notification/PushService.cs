// ---------------------------------------------------------------------------------------------------
// <copyright file="PushService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-10</date>
// <summary>
//     The PushService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Notification
{
    using System;
    using System.IO;
    using PushSharp;
    using PushSharp.Android;
    using PushSharp.Apple;
    using Youffer.Common.DataService;
    using Youffer.Common.Helper;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.Common.Notification;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.Constants;
    using Youffer.Resources.Enum;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The PushService class
    /// </summary>
    public class PushService : IPushMessageService
    {
        /// <summary>
        /// The pushBroker instance
        /// </summary>
        private readonly PushBroker pushBroker;

        /// <summary>
        /// The logger service
        /// </summary>
        private readonly ILoggerService loggerService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The notification log repository
        /// </summary>
        private readonly IRepository<NotificationLog> notificationLogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PushService" /> class.
        /// </summary>
        /// <param name="googleGcmApiKey"> The google api key</param>
        /// <param name="appleCertPwd"> The Api certificate password</param>
        /// <param name="outputPath">The output path</param>
        /// <param name="loggerService"> The logger service</param>
        /// <param name="mapperFactory">the mapper factory</param>
        /// <param name="notificationLogRepository">The notification log repository</param>
        public PushService(string googleGcmApiKey, string appleCertPwd, string outputPath, ILoggerService loggerService, IMapperFactory mapperFactory, IRepository<NotificationLog> notificationLogRepository)
        {
            this.loggerService = loggerService;
            this.mapperFactory = mapperFactory;
            this.notificationLogRepository = notificationLogRepository;

            this.pushBroker = new PushBroker();
            try
            {
                this.pushBroker.RegisterGcmService(new GcmPushChannelSettings(googleGcmApiKey));
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("Register Android notification service" + ex.Message);
            }

            var appleCert = File.ReadAllBytes(outputPath);

            try
            {
                this.pushBroker.RegisterAppleService(new ApplePushChannelSettings(appleCert, appleCertPwd));
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("Register iOS notification service" + ex.Message);
            }
        }

        /// <summary>
        /// Send Message Notification to Android Device
        /// </summary>
        /// <param name="gcmId">The GCMID </param>
        /// <param name="messageId">The Message Id</param>
        /// <param name="message">The Message Text</param>
        /// <param name="senderName">The sender name</param>
        /// <param name="type">The type of notification</param>
        /// <returns>bool object</returns>
        public bool SendMessageNotificationToAndroid(string gcmId, string messageId, string message, string senderName, string type = "")
        {
            message = "You have a new message from " + senderName + ".";
            try
            {
                this.pushBroker.QueueNotification(new GcmNotification().ForDeviceRegistrationId(gcmId)
                    .WithJson("{\"data.message\":\"" + message + "\",\"data.messageid\":\"" + messageId + "\",\"data.type\":\"" + type + "\"}"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendMessageNotificationToAndroid = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends the report user notification to android.
        /// </summary>
        /// <param name="gcmId">The GCM identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        public bool SendReportUserNotificationToAndroid(string gcmId, string userId, string username, string message, string type = "")
        {
            try
            {
                this.pushBroker.QueueNotification(new GcmNotification().ForDeviceRegistrationId(gcmId)
                    .WithJson("{\"data.message\":\"" + message + "\",\"data.companyid\":\"" + userId + "\",\"data.companyname\":\"" + username + "\",\"data.type\":\"" + type + "\"}"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendReportUserNotificationToAndroid = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends the report company notification to android.
        /// </summary>
        /// <param name="gcmId">The GCM identifier.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="companyname">The companyname.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        public bool SendReportCompanyNotificationToAndroid(string gcmId, string companyId, string companyname, string message, string type = "")
        {
            try
            {
                this.pushBroker.QueueNotification(new GcmNotification().ForDeviceRegistrationId(gcmId)
                    .WithJson("{\"data.message\":\"" + message + "\",\"data.userid\":\"" + companyId + "\",\"data.username\":\"" + companyname + "\",\"data.type\":\"" + type + "\"}"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendReportCompanyNotificationToAndroid = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends the not call again notification to android.
        /// </summary>
        /// <param name="gcmId">The GCM identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        public bool SendNotCallAgainNotificationToAndroid(string gcmId, string userId, string username, string message, string type = "")
        {
            try
            {
                this.pushBroker.QueueNotification(new GcmNotification().ForDeviceRegistrationId(gcmId)
                    .WithJson("{\"data.message\":\"" + message + "\",\"data.userid\":\"" + userId + "\",\"data.username\":\"" + username + "\",\"data.type\":\"" + type + "\"}"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendNotCallAgainNotificationToAndroid = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

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
        public bool SendMarkPurchasedNotificationToAndroid(string gcmId, string markedByName, string markedById, string markedToName, string markedToId, string interest, string message, string type = "")
        {
            try
            {
                this.pushBroker.QueueNotification(new GcmNotification().ForDeviceRegistrationId(gcmId)
                    .WithJson("{\"data.message\":\"" + message + "\",\"data.markedById\":\"" + markedById + "\",\"data.markedByName\":\"" + markedByName + "\",\"data.markedToId\":\"" + markedToId + "\",\"data.markedToName\":\"" + markedToName + "\",\"data.interest\":\"" + interest + "\",\"data.type\":\"" + type + "\"}"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendMarkPurchasedNotificationToAndroid = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

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
        public bool SendRatingNotificationToAndroid(string gcmId, string companyId, string companyname, string message, decimal rating, string type = "")
        {
            try
            {
                this.pushBroker.QueueNotification(new GcmNotification().ForDeviceRegistrationId(gcmId)
                    .WithJson("{\"data.message\":\"" + message + "\",\"data.companyid\":\"" + companyId + "\",\"data.companyname\":\"" + companyname + "\",\"data.rating\":\"" + rating + "\",\"data.type\":\"" + type + "\"}"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendRatingNotificationToAndroid = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

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
        public bool SendPurchasedNotificationToAndroid(string gcmId, string companyId, string companyname, string interest, string message, string type = "")
        {
            message = "Congrats! you just made $5.";
            try
            {
                this.pushBroker.QueueNotification(new GcmNotification().ForDeviceRegistrationId(gcmId)
                    .WithJson("{\"data.message\":\"" + message + "\",\"data.companyid\":\"" + companyId + "\",\"data.companyname\":\"" + companyname + "\",\"data.interest\":\"" + interest + "\",\"data.type\":\"" + type + "\"}"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendPurchasedNotificationToAndroid = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Send Credit Notification to Android Device
        /// </summary>
        /// <param name="gcmId">The GCMID </param>
        /// <param name="messageId">The Message Id</param>
        /// <param name="amount">The Amount</param>
        /// <param name="senderName">The sender name</param>
        /// <param name="type">The type of notification</param>
        /// <returns>bool object</returns>
        public bool SendCreditNotificationToAndroid(string gcmId, string messageId, string amount, string senderName, string type = "")
        {
            string message = amount + " has been added to your account";
            try
            {
                this.pushBroker.QueueNotification(new GcmNotification().ForDeviceRegistrationId(gcmId)
                    .WithJson("{\"data.message\":\"" + message + "\",\"data.messageid\":\"" + messageId + "\",\"data.type\":\"" + type + "\"}"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendCreditNotificationToAndroid = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = gcmId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.Android), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

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
        public bool SendMessageNotificationToiOS(string udId, string messageId, string message, string senderName, int badge, string type = "")
        {
            message = "You have a new message from " + senderName + ".";
            try
            {
                this.pushBroker.QueueNotification(new AppleNotification().ForDeviceToken(udId)
                       .WithAlert(message)
                       .WithCustomItem("messageId", messageId)
                       .WithCustomItem("message", message)
                       .WithCustomItem("type", type)
                       .WithBadge(badge)
                       .WithSound("sound.caf"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendMessageNotificationToiOS = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends the report user notification to iOS.
        /// </summary>
        /// <param name="udId">The UDID.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        public bool SendReportUserNotificationToiOS(string udId, string userId, string username, string message, string type = "")
        {
            message = username + " has reported you.";
            try
            {
                this.pushBroker.QueueNotification(new AppleNotification().ForDeviceToken(udId)
                       .WithAlert(message)
                       .WithCustomItem("userId", userId)
                       .WithCustomItem("username", username)
                       .WithCustomItem("message", message)
                       .WithCustomItem("type", type)
                       .WithSound("sound.caf"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendReportUserNotificationToiOS = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends the report company notification to iOS.
        /// </summary>
        /// <param name="udId">The UDID.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="companyname">The companyname.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        public bool SendReportCompanyNotificationToiOS(string udId, string companyId, string companyname, string message, string type = "")
        {
            message = companyname + " has reported you.";
            try
            {
                this.pushBroker.QueueNotification(new AppleNotification().ForDeviceToken(udId)
                       .WithAlert(message)
                       .WithCustomItem("companyId", companyId)
                       .WithCustomItem("companyname", companyname)
                       .WithCustomItem("message", message)
                       .WithCustomItem("type", type)
                       .WithSound("sound.caf"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendReportCompanyNotificationToiOS = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends the not call again notification to iOS.
        /// </summary>
        /// <param name="udId">The UDID.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>bool object</returns>
        public bool SendNotCallAgainNotificationToiOS(string udId, string userId, string username, string message, string type = "")
        {
            message = username + " has asked you not to call again.";
            try
            {
                this.pushBroker.QueueNotification(new AppleNotification().ForDeviceToken(udId)
                       .WithAlert(message)
                       .WithCustomItem("userId", userId)
                       .WithCustomItem("username", username)
                       .WithCustomItem("message", message)
                       .WithCustomItem("type", type)
                       .WithSound("sound.caf"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendNotCallAgainNotificationToiOS = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

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
        public bool SendMarkPurchasedNotificationToiOS(string udId, string markedByName, string markedById, string markedToName, string markedToId, string interest, string message, string type = "")
        {
            message = markedByName + " has purchased " + (string.IsNullOrEmpty(markedToName) ? "you" : markedToName) + ".";
            try
            {
                this.pushBroker.QueueNotification(new AppleNotification().ForDeviceToken(udId)
                       .WithAlert(message)
                       .WithCustomItem("markedByName", markedByName)
                       .WithCustomItem("markedById", markedById)
                       .WithCustomItem("markedToName", markedToName)
                       .WithCustomItem("markedToId", markedToId)
                       .WithCustomItem("interest", interest)
                       .WithCustomItem("message", message)
                       .WithCustomItem("type", type)
                       .WithSound("sound.caf"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendMarkPurchasedNotificationToiOS = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

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
        public bool SendRatingNotificationToiOS(string udId, string companyId, string companyname, string message, decimal rating, string type = "")
        {
            message = companyname + " has rated you " + rating + ".";
            try
            {
                this.pushBroker.QueueNotification(new AppleNotification().ForDeviceToken(udId)
                       .WithAlert(message)
                       .WithCustomItem("companyId", companyId)
                       .WithCustomItem("companyname", companyname)
                       .WithCustomItem("message", message)
                       .WithCustomItem("type", type)
                       .WithSound("sound.caf"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendRatingNotificationToiOS = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends the purchased notification to iOS.
        /// </summary>
        /// <param name="udId">The UDID.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="companyname">The companyname.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>Boolean object.</returns>
        public bool SendPurchasedNotificationToiOS(string udId, string companyId, string companyname, string message, string type = "")
        {
            message = "Congrats! you just made $5.";
            try
            {
                this.pushBroker.QueueNotification(new AppleNotification().ForDeviceToken(udId)
                       .WithAlert(message)
                       .WithCustomItem("companyId", companyId)
                       .WithCustomItem("companyname", companyname)
                       .WithCustomItem("message", message)
                       .WithCustomItem("type", type)
                       .WithSound("sound.caf"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendPurchasedNotificationToiOS = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

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
        public bool SendCreditNotificationToiOS(string udId, string messageId, string amount, string senderName, int badge, string type = "")
        {
            string message = amount + " has been added to your account";
            try
            {
                this.pushBroker.QueueNotification(new AppleNotification().ForDeviceToken(udId)
                       .WithAlert(message)
                       .WithCustomItem("messageId", messageId)
                       .WithCustomItem("message", message)
                       .WithCustomItem("type", type)
                       .WithBadge(badge)
                       .WithSound("sound.caf"));

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = true };
                this.SaveNotificationLog(notificationLogDto);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("SendCreditNotificationToiOS = " + ex.Message);

                NotificationLogDto notificationLogDto = new NotificationLogDto() { SentToId = udId, Message = message, NotificationType = type, OSType = Convert.ToInt32(OSType.iOS), IsSuccess = false };
                this.SaveNotificationLog(notificationLogDto);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Saves the notification log.
        /// </summary>
        /// <param name="notificationLogDto">The notification log dto.</param>
        /// <returns>NotificationLogDto object.</returns>
        public NotificationLogDto SaveNotificationLog(NotificationLogDto notificationLogDto)
        {
            try
            {
                NotificationLog notificationLog = this.mapperFactory.GetMapper<NotificationLogDto, NotificationLog>().Map(notificationLogDto);
                this.notificationLogRepository.Insert(notificationLog);
                this.notificationLogRepository.Commit();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("notificationLogDto - " + ex.Message);
            }

            return notificationLogDto;
        }
    }
}
