// ---------------------------------------------------------------------------------------------------
// <copyright file="ContactService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-18</date>
// <summary>
//     The ContactService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.CRMService
{
    using System;

    using Common.CRMService;
    using CRM;
    using Youffer.Common.LogService;
    using Youffer.Framework.Data;

    /// <summary>
    /// The ContactService class
    /// </summary>
    public class ContactService : IContactService
    {
        /// <summary>
        /// VTiger service instance
        /// </summary>
        private readonly IVTigerService vTigerService;

        /// <summary>
        ///  Initializes a new instance of the <see cref="ContactService" /> class.
        /// </summary>
        /// <param name="vTigerService">the vtiger service</param>
        /// <param name="loggerService">the logger service</param>
        public ContactService(IVTigerService vTigerService, ILoggerService loggerService)
        {
            this.vTigerService = vTigerService;
            this.LoggerService = loggerService;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        ///  Adding Contact into CRM
        /// </summary>
        /// <param name="contact"> the Contact entity. </param>
        /// <returns> VTigerContact entity </returns>
        public VTigerContact CreateContact(VTigerContact contact)
        {
            try
            {
                contact = this.vTigerService.Create<VTigerContact>(contact);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Adding Contact :- " + ex.Message);
                contact = new VTigerContact();
            }

            return contact;
        }

        /// <summary>
        ///  Retrieving Contact from CRM
        /// </summary>
        /// <param name="contactId"> the Contact id. </param>
        /// <returns> VTigerContact entity </returns>
        public VTigerContact ReadContact(string contactId)
        {
            VTigerContact contact = new VTigerContact();
            try
            {
                contact = this.vTigerService.Retrieve<VTigerContact>(contactId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Retrieving Contact :- " + ex.Message);
            }

            return contact;
        }

        /// <summary>
        ///  Updating Contact in CRM
        /// </summary>
        /// <param name="contact">the Contact entity. </param>
        /// <returns> VTigerContact entity </returns>
        public VTigerContact UpdateContact(VTigerContact contact)
        {
            try
            {
                contact = this.vTigerService.Update<VTigerContact>(contact);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Updating Contact :- " + ex.Message);
                return null;
            }

            return contact;
        }

        /// <summary>
        ///  Deleting Contact from CRM
        /// </summary>
        /// <param name="contactId"> the Contact id. </param>
        /// <returns> bool object </returns>
        public bool DeleteContact(string contactId)
        {
            try
            {
                this.vTigerService.Delete(contactId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Deleteing Contact :- " + ex.Message);
                return false;
            }

            return true;
        }
    }
}
