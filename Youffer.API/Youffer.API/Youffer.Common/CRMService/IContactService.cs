// ---------------------------------------------------------------------------------------------------
// <copyright file="IContactService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-18</date>
// <summary>
//     The IContactService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.CRMService
{
    using Youffer.CRM;

    /// <summary>
    /// the IContactService interface
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        ///  Adding Contact into CRM
        /// </summary>
        /// <param name="contact"> the Contact entity. </param>
        /// <returns> VTigerContact entity </returns>
        VTigerContact CreateContact(VTigerContact contact);

        /// <summary>
        ///  Retrieving Contact from CRM
        /// </summary>
        /// <param name="contactId"> the Contact id. </param>
        /// <returns> VTigerContact entity </returns>
        VTigerContact ReadContact(string contactId);

        /// <summary>
        ///  Updating Contact in CRM
        /// </summary>
        /// <param name="contact">the Contact entity. </param>
        /// <returns> VTigerContact entity </returns>
        VTigerContact UpdateContact(VTigerContact contact);

        /// <summary>
        ///  Deleting Contact from CRM
        /// </summary>
        /// <param name="contactId"> the Contact id. </param>
        /// <returns> bool object </returns>
        bool DeleteContact(string contactId);
    }
}
