// ---------------------------------------------------------------------------------------------------
// <copyright file="IContactUsService.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-16</date>
// <summary>
//     The IContactUsService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.CRMService
{
    using System.Collections.Generic;
    using Youffer.CRM;

    /// <summary>
    /// Interface IContactUsService
    /// </summary>
    public interface IContactUsService
    {
        /// <summary>
        /// Create ContactUs Message
        /// </summary>
        /// <param name="contactUs">The VTigerContactUs model</param>
        /// <returns>The VTigerContactUs object</returns>
        VTigerContactUs CreateContactUsMessage(VTigerContactUs contactUs);

        /// <summary>
        /// Read ContactUs Message
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>The VTigerContactUs object</returns>
        VTigerContactUs ReadContactUsMessage(string userId);

        /// <summary>
        /// Reads all contact us message.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of VtigerContactUs object.</returns>
        List<VTigerContactUs> ReadAllContactUsMessage(string userId, int lastpageId, int fetchCount, string sortBy, string direction);

        /// <summary>
        /// Marks the contact us messages deleted.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Boolean object.</returns>
        bool MarkContactUsMessagesDeleted(string userId);

        /// <summary>
        /// Checks if contact us MSG.
        /// </summary>
        /// <param name="msgid">The msgid.</param>
        /// <returns>Bool object</returns>
        bool CheckIfContactUsMsg(string msgid);

        /// <summary>
        /// Deletes the contactus message.
        /// </summary>
        /// <param name="msgid">The msgid.</param>
        /// <returns>Bool object</returns>
        bool DeleteContactusMessage(string msgid);
    }
}
