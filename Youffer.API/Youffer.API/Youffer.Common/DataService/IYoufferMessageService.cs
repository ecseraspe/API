// ---------------------------------------------------------------------------------------------------
// <copyright file="IYoufferMessageService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-5</date>
// <summary>
//     The IYoufferMessageService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using System.Collections.Generic;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Interface IYoufferMessageService
    /// </summary>
    public interface IYoufferMessageService
    {
        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The MessagesDto object.</returns>
        MessagesDto CreateMessage(MessagesDto message);

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="msgId">The MSG identifier.</param>
        /// <param name="userId"> The user Id. </param>
        /// <returns>Boolean object.</returns>
        bool DeleteMessage(string msgId, string userId);

        /// <summary>
        /// Creates the message thread.
        /// </summary>
        /// <param name="messageThread">The message thread.</param>
        /// <returns>MessageThreadDto object.</returns>
        MessageThreadDto CreateMessageThread(MessageThreadDto messageThread);

        /// <summary>
        /// Makes the message media entry.
        /// </summary>
        /// <param name="messageMedia">The message media.</param>
        /// <returns>MessageMediaDto object</returns>
        MessageMediaDto MakeMessageMediaEntry(MessageMediaDto messageMedia);

        /// <summary>
        /// Gets the message thread.
        /// </summary>
        /// <param name="fromUserId">From user identifier.</param>
        /// <param name="toUserId">To user identifier.</param>
        /// <returns>MessageThreadDto object.</returns>
        MessageThreadDto GetMessageThread(string fromUserId, string toUserId);

        /// <summary>
        /// Gets all messages.
        /// </summary>
        /// <param name="requestedBy">The requested userId</param>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="lastfetchedId">The lastfetched identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of MessagesDto object.</returns>
        List<MessagesDto> GetAllMessages(string requestedBy, long threadId, int lastfetchedId, int fetchCount, string sortBy, string direction);

        /// <summary>
        /// Gets all company messages.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastfetchedId">The lastfetched identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of MessagesDto object.</returns>
        List<MessagesDto> GetAllCompanyMessages(string companyId, int lastfetchedId, int fetchCount, string sortBy, string direction);

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
        List<MessagesDto> GetUserAllMessage(string userId, int lastfetchedId, int fetchCount, string sortBy, string direction);

        /// <summary>
        /// Creates the contact us message.
        /// </summary>
        /// <param name="contactUs">The contact us.</param>
        /// <param name="fromAdmin"> is Message from Admin</param>
        /// <returns>ContactUsDto object.</returns>
        ContactUsDto CreateContactUsMessage(ContactUsDto contactUs, bool fromAdmin = false);

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
        List<ContactUsDto> GetContactUsMessage(string userId, long threadId, int lastpageId, int fetchCount, string sortBy, string direction);

        /// <summary>
        /// Deletes the thread.
        /// </summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="userId">the user id.</param>
        /// <returns>Boolean object.</returns>
        bool DeleteThread(long threadId, string userId);

        /// <summary>
        /// Gets the message media.
        /// </summary>
        /// <param name="msgId">The MSG identifier.</param>
        /// <returns>MessageMediaDto object.</returns>
        MessageMediaDto GetMessageMedia(long msgId);

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
        bool SendBulkMessage(string companyId, string message, string originalFileName, string extension, string size, string filename);

        /// <summary>
        /// Gets the unread MSG count.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isUser">Is User</param>
        /// <returns>Int value</returns>
        int GetUnreadMsgCount(string userId, bool isUser);
    }
}
