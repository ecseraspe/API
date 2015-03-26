// ---------------------------------------------------------------------------------------------------
// <copyright file="SendNotification.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-23</date>
// <summary>
//     The SendNotification class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Quartz;
    using StructureMap;
    using Youffer.API.DependencyResolution;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Common.Helper;
    using Youffer.Common.LogService;
    using Youffer.Common.Notification;
    using Youffer.DataService;
    using Youffer.DataService.DBSchema;
    using Youffer.Framework.Notification;
    using Youffer.Resources.Constants;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.Enum;
    using Youffer.Resources.MySqlDbSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Class SendNotification
    /// </summary>
    public class SendNotification : IJob
    {
        /// <summary>
        /// The Logger service
        /// </summary>
        private readonly ILoggerService loggerservice;

        /// <summary>
        /// The youffer message service
        /// </summary>
        private readonly IYoufferMessageService youfferMessageService;

        /// <summary>
        /// The IUserService service instance
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The IPushMessageService instance
        /// </summary>
        private readonly IPushMessageService pushMessageService;

        /// <summary>
        /// The common service
        /// </summary>
        private readonly ICommonService commonService;

        /// <summary>
        /// The CRM manager service
        /// </summary>
        private readonly ICRMManagerService crmManagerService;

        /// <summary>
        /// The authentication repository
        /// </summary>
        private readonly IAuthRepository authRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendNotification"/> class.
        /// </summary>
        public SendNotification()
        {
            var container = new Container(new DefaultRegistry());
            this.loggerservice = container.GetInstance<ILoggerService>();
            this.youfferMessageService = container.GetInstance<IYoufferMessageService>();
            this.userService = container.GetInstance<IUserService>();
            this.pushMessageService = container.GetInstance<IPushMessageService>();
            this.commonService = container.GetInstance<ICommonService>();
            this.crmManagerService = container.GetInstance<ICRMManagerService>();
            this.authRepository = container.GetInstance<IAuthRepository>();
        }

        /// <summary>
        /// Executes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            if (key.Group == "myJobSendMsgFromCRM")
            {
                this.SendMessageToYoufferInbox();
            }
            else if (key.Group == "myJobEnterPhone")
            {
                this.SendEnterPhoneMessageAndNotification();
            }
            else if (key.Group == "myJobEnterNeeds")
            {
                this.SendEnterNeedsMessageAndNotification();
            }
            else if (key.Group == "myJobBroadcastMsgFromCRM")
            {
                this.BroadcastMessageToYoufferInbox();
            }
            else if (key.Group == "myjobUpdateCreditBalance")
            {
                this.UpdateCreditBalance();
            }
            else if (key.Group == "myjobUpdateCashBalance")
            {
                this.UpdateCashBalance();
            }
        }

        /// <summary>
        /// Sends the message to youffer inbox.
        /// </summary>
        private void SendMessageToYoufferInbox()
        {
            try
            {
                using (MySqlContext dbContext = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(dbContext);
                    var lst = tmp.SqlQuery<SendMessageFromCrmDto>("CALL SendMessageToYoufferInbox();").ToList();
                    List<string> succ = new List<string>();
                    List<string> fail = new List<string>();
                    foreach (var item in lst)
                    {
                        MessagesDto message = new MessagesDto();
                        message.ModifiedBy = message.FromUser = message.CreatedBy = "YoufferAdmin";
                        message.IsDeleted = false;

                        string userId = string.Empty;
                        string crmUserId;
                        if (item.setype == "Accounts")
                        {
                            crmUserId = AppSettings.Get<string>(ConfigConstants.OrganisationId) + item.sendmessage_tks_sendto;
                        }
                        else
                        {
                            crmUserId = AppSettings.Get<string>(ConfigConstants.ContactId) + item.sendmessage_tks_sendto;
                        }

                        var user = this.userService.GetContactByCrmId(crmUserId);
                        if (user != null && !string.IsNullOrWhiteSpace(user.UserName))
                        {
                            userId = user.Id;
                        }

                        message.CompanyId = AppSettings.Get<string>(ConfigConstants.SuperUserId);
                        message.UserId = userId;
                        message.ToUser = userId;
                        message.Name = "Youffer Admin";
                        message.MediaId = 0;
                        message.Message = item.sendmessage_tks_message;

                        message = this.youfferMessageService.CreateMessage(message);

                        if (message.Id > 0)
                        {
                            succ.Add(item.sendmessageid.ToString());
                        }
                        else
                        {
                            fail.Add(item.sendmessageid.ToString());
                        }

                        string gcmId = item.GCMId;
                        string udId = item.UDId;

                        if (!string.IsNullOrEmpty(gcmId))
                        {
                            this.loggerservice.LogException("GCMID - " + gcmId);
                            this.pushMessageService.SendMessageNotificationToAndroid(gcmId, message.Id.ToString(), message.Message, "Youffer", item.setype == "Accounts" ? Notification.usermsg.ToString() : Notification.companymsg.ToString());
                        }

                        if (!string.IsNullOrEmpty(udId))
                        {
                            int unreadMsgCount = this.youfferMessageService.GetUnreadMsgCount(message.UserId, false);
                            this.loggerservice.LogException("UDID - " + udId);
                            this.pushMessageService.SendMessageNotificationToiOS(udId, message.Id.ToString(), message.Message, "Youffer", unreadMsgCount, Notification.usermsg.ToString());
                        }
                    }

                    string passresult = succ.Any() ? succ.Aggregate((a, b) => a + "," + b) : string.Empty;
                    string failresult = fail.Any() ? fail.Aggregate((a, b) => a + "," + b) : string.Empty;
                    if (!string.IsNullOrWhiteSpace(passresult) || !string.IsNullOrWhiteSpace(failresult))
                    {
                        var res = tmp.SqlQuery<int>("CALL UpdateMessageStatus({0}, {1});", passresult, failresult).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                this.loggerservice.LogException("SendMessageToYoufferInboxFromCRM:- " + ex.Message);
            }
        }

        /// <summary>
        /// Broadcasts the message to youffer inbox.
        /// </summary>
        private void BroadcastMessageToYoufferInbox()
        {
            try
            {
                using (MySqlContext dbContext = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(dbContext);
                    var lst = tmp.SqlQuery<BroadcastMessageFromCRMDto>("CALL BroadcastMessageToYoufferInbox();").ToList();
                    foreach (var item in lst)
                    {
                        MessagesDto message = new MessagesDto();
                        message.ModifiedBy = message.FromUser = message.CreatedBy = "YoufferAdmin";
                        message.IsDeleted = false;

                        string userId = string.Empty;
                        string crmUserId;
                        if (item.SendTo == "Organizations")
                        {
                            crmUserId = AppSettings.Get<string>(ConfigConstants.OrganisationId) + item.RecId;
                        }
                        else
                        {
                            crmUserId = AppSettings.Get<string>(ConfigConstants.ContactId) + item.RecId;
                        }

                        var user = this.userService.GetContactByCrmId(crmUserId);
                        if (user != null && !string.IsNullOrWhiteSpace(user.UserName))
                        {
                            userId = user.Id;
                        }

                        message.CompanyId = AppSettings.Get<string>(ConfigConstants.SuperUserId);
                        message.UserId = userId;
                        message.ToUser = userId;
                        message.Name = "Youffer Admin";
                        message.MediaId = 0;
                        message.Message = item.Message;

                        message = this.youfferMessageService.CreateMessage(message);

                        string gcmId = item.GCMId;
                        string udId = item.UDId;

                        if (!string.IsNullOrEmpty(gcmId))
                        {
                            this.pushMessageService.SendMessageNotificationToAndroid(gcmId, message.Id.ToString(), message.Message, "Youffer", item.SendTo == "Organizations" ? Notification.usermsg.ToString() : Notification.companymsg.ToString());
                        }

                        if (!string.IsNullOrEmpty(udId))
                        {
                            int unreadMsgCount = this.youfferMessageService.GetUnreadMsgCount(message.UserId, false);
                            this.pushMessageService.SendMessageNotificationToiOS(udId, message.Id.ToString(), message.Message, "Youffer", unreadMsgCount, Notification.usermsg.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.loggerservice.LogException("BroadcastMessageToYoufferInbox:- " + ex.Message);
            }
        }

        /// <summary>
        /// Sends the enter phone message and notification.
        /// </summary>
        private void SendEnterPhoneMessageAndNotification()
        {
            try
            {
                using (MySqlContext dbContext = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(dbContext);
                    var lst = tmp.SqlQuery<SendEnterPhoneAndNeedsMessageDto>("CALL SendEnterPhoneMessageAndNotification();").ToList();

                    foreach (var item in lst)
                    {
                        string userId = string.Empty;
                        string crmUserId;

                        crmUserId = AppSettings.Get<string>(ConfigConstants.ContactId) + item.contactId;
                        var user = this.userService.GetContactByCrmId(crmUserId);
                        if (user != null && !string.IsNullOrWhiteSpace(user.UserName))
                        {
                            userId = user.Id;
                        }

                        MessagesDto message = new MessagesDto();
                        message.ModifiedBy = message.FromUser = message.CreatedBy = "YoufferAdmin";
                        message.IsDeleted = false;
                        message.CompanyId = AppSettings.Get<string>(ConfigConstants.SuperUserId);
                        message.UserId = message.ToUser = userId;
                        message.Name = "Youffer Admin";
                        message.MediaId = 0;

                        MessageTemplatesDto msgTemplate = this.commonService.GetMessageTemplate(MessageTemplateType.EnterPhoneMsg);
                        message.Message = msgTemplate.TemplateText;

                        message = this.youfferMessageService.CreateMessage(message);

                        string gcmId = item.GCMId;
                        string udId = item.UDId;

                        if (!string.IsNullOrEmpty(gcmId))
                        {
                            this.pushMessageService.SendMessageNotificationToAndroid(gcmId, message.Id.ToString(), message.Message, "Youffer", Notification.companymsg.ToString());
                        }

                        if (!string.IsNullOrEmpty(udId))
                        {
                            int unreadMsgCount = this.youfferMessageService.GetUnreadMsgCount(message.UserId, false);
                            this.pushMessageService.SendMessageNotificationToiOS(udId, message.Id.ToString(), message.Message, "Youffer", unreadMsgCount, Notification.usermsg.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.loggerservice.LogException("SendEnterPhoneMessageAndNotification:- " + ex.Message);
            }
        }

        /// <summary>
        /// Sends the enter needs message and notification.
        /// </summary>
        private void SendEnterNeedsMessageAndNotification()
        {
            try
            {
                using (MySqlContext dbContext = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(dbContext);
                    var lst = tmp.SqlQuery<SendEnterPhoneAndNeedsMessageDto>("CALL SendEnterNeedsMessageAndNotification();").ToList();

                    foreach (var item in lst)
                    {
                        string userId = string.Empty;
                        string crmUserId;

                        crmUserId = AppSettings.Get<string>(ConfigConstants.ContactId) + item.contactId;
                        var user = this.userService.GetContactByCrmId(crmUserId);
                        if (user != null && !string.IsNullOrWhiteSpace(user.UserName))
                        {
                            userId = user.Id;
                        }

                        MessagesDto message = new MessagesDto();
                        message.ModifiedBy = message.FromUser = message.CreatedBy = "YoufferAdmin";
                        message.IsDeleted = false;
                        message.CompanyId = AppSettings.Get<string>(ConfigConstants.SuperUserId);
                        message.UserId = message.ToUser = userId;
                        message.Name = "Youffer Admin";
                        message.MediaId = 0;

                        MessageTemplatesDto msgTemplate = this.commonService.GetMessageTemplate(MessageTemplateType.EnterNeedsMsg);
                        message.Message = msgTemplate.TemplateText;

                        message = this.youfferMessageService.CreateMessage(message);

                        string gcmId = item.GCMId;
                        string udId = item.UDId;

                        if (!string.IsNullOrEmpty(gcmId))
                        {
                            this.pushMessageService.SendMessageNotificationToAndroid(gcmId, message.Id.ToString(), message.Message, "Youffer", Notification.companymsg.ToString());
                        }

                        if (!string.IsNullOrEmpty(udId))
                        {
                            int unreadMsgCount = this.youfferMessageService.GetUnreadMsgCount(message.UserId, false);
                            this.pushMessageService.SendMessageNotificationToiOS(udId, message.Id.ToString(), message.Message, "Youffer", unreadMsgCount, Notification.usermsg.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.loggerservice.LogException("SendEnterNeedsMessageAndNotification:- " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the credit balance.
        /// </summary>
        private void UpdateCreditBalance()
        {
            try
            {
                using (MySqlContext dbContext = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(dbContext);
                    var lst = tmp.SqlQuery<SendUpdateCreditBalanceMessageDto>("CALL SendUpdateCreditBalanceMsgAndNotification();").ToList();

                    foreach (var item in lst)
                    {
                        string orgId = string.Empty;
                        string crmUserId;

                        crmUserId = AppSettings.Get<string>(ConfigConstants.OrganisationId) + item.OrgId;
                        var user = this.userService.GetContactByCrmId(crmUserId);
                        if (user != null && !string.IsNullOrWhiteSpace(user.UserName))
                        {
                            orgId = user.Id;
                        }

                        ////Update Organisation's credit balance
                        OrganisationModel org = this.crmManagerService.GetOrganisation(orgId);
                        org.CreditBalance += item.CreditBalance;
                        this.crmManagerService.UpdateOrganisation(orgId, org);

                        string currency = org.BillCountry.ToLower() == "india" ? "₹" : "$";
                        string amount = currency + item.CreditBalance.ToString("#");

                        ////Send email to the organisation
                        this.authRepository.SendCreditBalanceEmail(orgId, org.AccountName, amount);

                        ////Send message and notification to organisation
                        MessagesDto message = new MessagesDto();
                        message.ModifiedBy = message.FromUser = message.CreatedBy = "YoufferAdmin";
                        message.IsDeleted = false;
                        message.CompanyId = AppSettings.Get<string>(ConfigConstants.SuperUserId);
                        message.UserId = message.ToUser = orgId;
                        message.Name = "Youffer Admin";
                        message.MediaId = 0;

                        MessageTemplatesDto msgTemplate = this.commonService.GetMessageTemplate(MessageTemplateType.UpdateCreditBalanceMsg);
                        msgTemplate.TemplateText = msgTemplate.TemplateText.Replace("{{Amount}}", amount);
                        message.Message = msgTemplate.TemplateText;
                        message = this.youfferMessageService.CreateMessage(message);

                        ////Notifications
                        SignalRHub hub = new SignalRHub();
                        UserBalanceModelDto balance = new UserBalanceModelDto { CashBalance = org.CashBalance, CreditBalance = org.CreditBalance };
                        hub.SendMessage(message.UserId, message);
                        hub.SendCreditUpdateMessage(message.UserId, balance);

                        string gcmId = item.GCMId;
                        string udId = item.UDId;

                        if (!string.IsNullOrEmpty(gcmId))
                        {
                            this.pushMessageService.SendMessageNotificationToAndroid(gcmId, message.Id.ToString(), message.Message, "Youffer", Notification.usermsg.ToString());
                            this.pushMessageService.SendCreditNotificationToAndroid(gcmId, message.Id.ToString(), amount, "Youffer", Notification.updatecreditbal.ToString());
                        }

                        if (!string.IsNullOrEmpty(udId))
                        {
                            int unreadMsgCount = this.youfferMessageService.GetUnreadMsgCount(message.UserId, false);
                            this.pushMessageService.SendMessageNotificationToiOS(udId, message.Id.ToString(), message.Message, "Youffer", unreadMsgCount, Notification.usermsg.ToString());
                            this.pushMessageService.SendCreditNotificationToiOS(udId, message.Id.ToString(), amount, "Youffer", unreadMsgCount, Notification.updatecreditbal.ToString());
                        }

                        var res = tmp.SqlQuery<int>("CALL UpdateCreditBalanceStatus({0});", item.Id).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                this.loggerservice.LogException("UpdateCreditBalance:- " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the cash balance.
        /// </summary>
        private void UpdateCashBalance()
        {
            try
            {
                using (MySqlContext dbContext = new MySqlContext())
                {
                    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(dbContext);
                    var lst = tmp.SqlQuery<UpdateCashBalanceDto>("CALL UpdateCashBalance();").ToList();

                    foreach (var item in lst)
                    {
                        string orgId = string.Empty;
                        string crmUserId;

                        crmUserId = AppSettings.Get<string>(ConfigConstants.OrganisationId) + item.OrgId;
                        var user = this.userService.GetContactByCrmId(crmUserId);
                        if (user != null && !string.IsNullOrWhiteSpace(user.UserName))
                        {
                            orgId = user.Id;
                        }

                        ////Update Organisation's cash balance
                        OrganisationModel org = this.crmManagerService.GetOrganisation(orgId);
                        org.CashBalance += item.CashBalance;
                        this.crmManagerService.UpdateOrganisation(orgId, org);

                        var res = tmp.SqlQuery<int>("CALL UpdateCashBalanceStatus({0});", item.Id).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                this.loggerservice.LogException("UpdateCashBalance:- " + ex.Message);
            }
        }
    }
}
