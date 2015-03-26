// ---------------------------------------------------------------------------------------------------
// <copyright file="YoufferMessageService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-5</date>
// <summary>
//     The YoufferMessageService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data.CRMService
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Class YoufferMessageService
    /// </summary>
    public class YoufferMessageService : IYoufferMessageService
    {
        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The company notes repository
        /// </summary>
        private readonly IRepository<Messages> messageRepository;

        /// <summary>
        /// The message thread repository
        /// </summary>
        private readonly IRepository<MessageThread> messageThreadRepository;

        /// <summary>
        /// The message media repository
        /// </summary>
        private readonly IRepository<MessageMedia> messageMediaRepository;

        /// <summary>
        /// The contact us repository
        /// </summary>
        private readonly IRepository<ContactUs> contactUsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="YoufferMessageService"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="messagesRepository">The messages repository.</param>
        /// <param name="messageThreadRepository">The message thread repository.</param>
        /// <param name="messageMediaRepository">The message media repository.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="contactUsRepository">The contact us repository.</param>
        public YoufferMessageService(ILoggerService loggerService, IRepository<Messages> messagesRepository, IRepository<MessageThread> messageThreadRepository, IRepository<MessageMedia> messageMediaRepository, IMapperFactory mapperFactory, IRepository<ContactUs> contactUsRepository)
        {
            this.LoggerService = loggerService;
            this.messageRepository = messagesRepository;
            this.messageThreadRepository = messageThreadRepository;
            this.messageMediaRepository = messageMediaRepository;
            this.mapperFactory = mapperFactory;
            this.contactUsRepository = contactUsRepository;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The MessagesDto object.</returns>
        public MessagesDto CreateMessage(MessagesDto message)
        {
            try
            {
                MessageThreadDto messageThread = this.GetMessageThread(message.FromUser, message.ToUser);
                if (messageThread == null || messageThread.Id < 1)
                {
                    messageThread = new MessageThreadDto { FromUser = message.FromUser, ToUser = message.ToUser, CreatedBy = message.FromUser, ModifiedBy = message.FromUser };
                    messageThread = this.CreateMessageThread(messageThread);
                }

                if (messageThread.IsDeleted || messageThread.IsDeletedByUser || messageThread.IsDeletedByCompany)
                {
                    messageThread.IsDeleted = messageThread.IsDeletedByUser = messageThread.IsDeletedByCompany = false;
                    messageThread.ModifiedOn = DateTime.UtcNow;
                    MessageThread thread = this.mapperFactory.GetMapper<MessageThreadDto, MessageThread>().Map(messageThread);
                    this.messageThreadRepository.Update(thread);
                    this.messageThreadRepository.Commit();
                }

                message.ThreadId = messageThread.Id;
                Messages msg = this.mapperFactory.GetMapper<MessagesDto, Messages>().Map(message);
                this.messageRepository.Insert(msg);
                this.messageRepository.Commit();

                message = this.mapperFactory.GetMapper<Messages, MessagesDto>().Map(msg);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("CreateMessage - " + ex.Message);
            }

            return message;
        }

        /// <summary>
        /// Gets all messages.
        /// </summary>
        /// <param name="requestedBy">The requested userId</param>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="lastfetchedId">The lastfetched identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// List of MessagesDto object.
        /// </returns>
        public List<MessagesDto> GetAllMessages(string requestedBy, long threadId, int lastfetchedId, int fetchCount, string sortBy, string direction)
        {
            List<MessagesDto> lstMessagesModel = new List<MessagesDto>();
            try
            {
                object[] sqlCol = { new SqlParameter("@RequestedBy", requestedBy), new SqlParameter("@ThreadId", threadId), new SqlParameter("@FetchCount", fetchCount), new SqlParameter("@LastseenId", lastfetchedId), new SqlParameter("@SortDirection", direction), new SqlParameter("@SortBy", sortBy) };
                List<MessagesDto> lstMessages = this.messageRepository.SqlQuery<MessagesDto>("GetMessages_v1 @RequestedBy, @ThreadId, @FetchCount, @LastseenId, @SortDirection, @SortBy ", sqlCol).ToList();

                foreach (var item in lstMessages)
                {
                    item.MessageMedia = this.GetMessageMedia(item.Id);
                    lstMessagesModel.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetAllMessages - " + ex.Message);
            }

            return lstMessagesModel;
        }

        /// <summary>
        /// Gets all user's messages.
        /// </summary> 
        /// <param name="userId"> The User Id.</param>
        /// <param name="lastfetchedId">The lastfetched identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// List of MessagesDto object.
        /// </returns>
        public List<MessagesDto> GetUserAllMessage(string userId, int lastfetchedId, int fetchCount, string sortBy, string direction)
        {
            List<MessagesDto> lstMessagesModel = new List<MessagesDto>();
            try
            {
                object[] sqlCol = { new SqlParameter("@UserId", userId), new SqlParameter("@FetchCount", fetchCount), new SqlParameter("@LastseenId", lastfetchedId), new SqlParameter("@SortDirection", direction), new SqlParameter("@SortBy", sortBy) };
                List<MessagesDto> lstMessages = this.messageRepository.SqlQuery<MessagesDto>("GetUserMessages @UserId, @FetchCount, @LastseenId, @SortDirection, @SortBy ", sqlCol).ToList();

                foreach (var item in lstMessages)
                {
                    item.MessageMedia = this.GetMessageMedia(item.Id);
                    lstMessagesModel.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetUserAllMessage - " + ex.Message);
            }

            return lstMessagesModel;
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="msgId">The MSG identifier.</param>
        /// <param name="userId"> The user Id. </param>
        /// <returns>Boolean object.</returns>
        public bool DeleteMessage(string msgId, string userId)
        {
            try
            {
                object[] sqlCol = { new SqlParameter("@MessageId", msgId), new SqlParameter("@UserId", userId) };
                var res = this.messageRepository.SqlQuery<int>("DeleteMessages @MessageId, @UserId ", sqlCol).FirstOrDefault();
                return true;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("DeleteMessage - " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Creates the message thread.
        /// </summary>
        /// <param name="messageThread">The message thread.</param>
        /// <returns>MessageThreadDto object.</returns>
        public MessageThreadDto CreateMessageThread(MessageThreadDto messageThread)
        {
            try
            {
                MessageThread msgThread = this.mapperFactory.GetMapper<MessageThreadDto, MessageThread>().Map(messageThread);
                this.messageThreadRepository.Insert(msgThread);
                this.messageThreadRepository.Commit();

                messageThread.Id = msgThread.Id;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("CreateMessageThread - " + ex.Message);
            }

            return messageThread;
        }

        /// <summary>
        /// Makes the message media entry.
        /// </summary>
        /// <param name="messageMedia">The message media.</param>
        /// <returns>MessageMediaDto object</returns>
        public MessageMediaDto MakeMessageMediaEntry(MessageMediaDto messageMedia)
        {
            try
            {
                MessageMedia msgMedia = this.mapperFactory.GetMapper<MessageMediaDto, MessageMedia>().Map(messageMedia);
                this.messageMediaRepository.Insert(msgMedia);
                this.messageMediaRepository.Commit();

                messageMedia.Id = msgMedia.Id;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("MakeMessageMediaEntry - " + ex.Message);
            }

            return messageMedia;
        }

        /// <summary>
        /// Gets the message thread.
        /// </summary>
        /// <param name="fromUserId">From user identifier.</param>
        /// <param name="toUserId">To user identifier.</param>
        /// <returns>MessageThreadDto object.</returns>
        public MessageThreadDto GetMessageThread(string fromUserId, string toUserId)
        {
            MessageThreadDto messageThread = new MessageThreadDto();
            try
            {
                MessageThread msgThread = this.messageThreadRepository.Find(x => (x.FromUser == fromUserId && x.ToUser == toUserId) || (x.FromUser == toUserId && x.ToUser == fromUserId)).FirstOrDefault();
                messageThread = this.mapperFactory.GetMapper<MessageThread, MessageThreadDto>().Map(msgThread);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMessageThread - " + ex.Message);
            }

            return messageThread;
        }

        /// <summary>
        /// Gets all company messages.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastfetchedId">The lastfetched identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of MessagesDto object.</returns>
        public List<MessagesDto> GetAllCompanyMessages(string companyId, int lastfetchedId, int fetchCount, string sortBy, string direction)
        {
            List<MessagesDto> lstMessagesModel = new List<MessagesDto>();
            try
            {
                object[] sqlCol = { new SqlParameter("@CompanyId", companyId), new SqlParameter("@FetchCount", fetchCount), new SqlParameter("@LastseenId", lastfetchedId), new SqlParameter("@SortDirection", direction), new SqlParameter("@SortBy", sortBy) };
                List<MessagesDto> lstMessages = this.messageRepository.SqlQuery<MessagesDto>("GetCompanyMessages @CompanyId, @FetchCount, @LastseenId, @SortDirection, @SortBy ", sqlCol).ToList();

                foreach (var item in lstMessages)
                {
                    item.MessageMedia = this.GetMessageMedia(item.Id);
                    lstMessagesModel.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetAllCompanyMessage - " + ex.Message);
            }

            return lstMessagesModel;
        }

        /// <summary>
        /// Creates the contact us message.
        /// </summary>
        /// <param name="contactUs">The contact us.</param>
        /// <param name="fromAdmin"> is Message from Admin</param>
        /// <returns>ContactUsDto object.</returns>
        public ContactUsDto CreateContactUsMessage(ContactUsDto contactUs, bool fromAdmin = false)
        {
            try
            {
                ContactUs contactUsMsg = this.contactUsRepository.Find(x => x.UserId == contactUs.UserId && !x.IsDeleted).FirstOrDefault() ?? new ContactUs();

                if (contactUsMsg.Id > 0)
                {
                    contactUs.ThreadId = contactUsMsg.ThreadId;
                }
                else
                {
                    MessageThreadDto msgThreadDto = new MessageThreadDto();
                    if (!fromAdmin)
                    {
                        msgThreadDto.FromUser = msgThreadDto.CreatedBy = msgThreadDto.ModifiedBy = contactUs.UserId;
                        msgThreadDto.ToUser = "YoufferAdmin";
                    }
                    else
                    {
                        msgThreadDto.FromUser = msgThreadDto.CreatedBy = msgThreadDto.ModifiedBy = "YoufferAdmin";
                        msgThreadDto.ToUser = contactUs.UserId;
                    }

                    msgThreadDto.IsDeleted = msgThreadDto.IsDeletedByCompany = msgThreadDto.IsDeletedByUser = false;
                    msgThreadDto = this.CreateMessageThread(msgThreadDto);
                    contactUs.ThreadId = msgThreadDto.Id;
                }

                contactUsMsg = this.mapperFactory.GetMapper<ContactUsDto, ContactUs>().Map(contactUs);
                this.contactUsRepository.Insert(contactUsMsg);
                this.contactUsRepository.Commit();

                contactUs.Id = contactUsMsg.Id;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("CreateContactUsMessage - " + ex.Message);
            }

            return contactUs;
        }

        /// <summary>
        /// Gets the contact us message.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// List of ContactUsDto object.
        /// </returns>
        public List<ContactUsDto> GetContactUsMessage(string userId, long threadId, int lastpageId, int fetchCount, string sortBy, string direction)
        {
            List<ContactUsDto> lstContactUsModel = new List<ContactUsDto>();
            try
            {
                object[] sqlCol = { new SqlParameter("@RequestedBy", userId), new SqlParameter("@ThreadId", threadId), new SqlParameter("@FetchCount", fetchCount), new SqlParameter("@LastseenId", lastpageId), new SqlParameter("@SortDirection", direction), new SqlParameter("@SortBy", sortBy) };
                lstContactUsModel = this.contactUsRepository.SqlQuery<ContactUsDto>("GetContactUsMessages_v1 @RequestedBy, @ThreadId, @FetchCount, @LastseenId, @SortDirection, @SortBy ", sqlCol).ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetContactUsMessage - " + ex.Message);
            }

            return lstContactUsModel;
        }

        /// <summary>
        /// Deletes the thread.
        /// </summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="userId">the user id.</param>
        /// <returns>Boolean object.</returns>
        public bool DeleteThread(long threadId, string userId)
        {
            try
            {
                object[] sqlCol = { new SqlParameter("@RequestedBy", userId), new SqlParameter("@ThreadId", threadId) };
                var res = this.messageRepository.SqlQuery<int>("DeleteMessageThread @RequestedBy, @ThreadId", sqlCol).FirstOrDefault();
                return res == 1;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("DeleteThread - " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Gets the message media.
        /// </summary>
        /// <param name="msgId">The MSG identifier.</param>
        /// <returns>MessageMediaDto object.</returns>
        public MessageMediaDto GetMessageMedia(long msgId)
        {
            MessageMediaDto messageMedia = new MessageMediaDto();

            try
            {
                MessageMedia media = this.messageMediaRepository.Find(x => x.MessageId == msgId && !x.IsDeleted).FirstOrDefault();
                messageMedia = this.mapperFactory.GetMapper<MessageMedia, MessageMediaDto>().Map(media);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMessageMedia - " + ex.Message);
            }

            return messageMedia;
        }

        /// <summary>
        /// Sends the bulk message.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="message">The message.</param>
        /// <param name="originalFileName">Name of the original file.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="size">The size.</param>
        /// <param name="filename">The filename.</param>
        /// <returns>Boolean object.</returns>
        public bool SendBulkMessage(string companyId, string message, string originalFileName, string extension, string size, string filename)
        {
            bool isSuccess = true;

            try
            {
                object[] sqlCol = { new SqlParameter("@companyId", companyId), new SqlParameter("@Message", message), new SqlParameter("@OriginalFileName", originalFileName), new SqlParameter("@Extension", extension), new SqlParameter("@Size", size), new SqlParameter("@FileName", filename) };
                this.messageRepository.SqlQuery<MessagesDto>("SendBulkMessage @companyId, @Message, @OriginalFileName, @Extension, @Size, @FileName", sqlCol).FirstOrDefault();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("SendBulkMessage - " + ex.Message);
            }

            return isSuccess;
        }

        /// <summary>
        /// Gets the unread MSG count.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isUser">Is User</param>
        /// <returns>Int value</returns>
        public int GetUnreadMsgCount(string userId, bool isUser)
        {
            int unreadMsgCnt = 0;
            try
            {
                object[] sqlCol = { new SqlParameter("@UserId", userId), new SqlParameter("@IsUser", isUser) };
                unreadMsgCnt = this.messageRepository.SqlQuery<int>("GetUnreadMsgCount @UserId, @IsUser", sqlCol).FirstOrDefault();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetUnreadMsgCount " + ex.Message);
            }

            return unreadMsgCnt;
        }
    }
}
