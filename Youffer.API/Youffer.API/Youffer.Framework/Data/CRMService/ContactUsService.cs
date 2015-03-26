// ---------------------------------------------------------------------------------------------------
// <copyright file="ContactUsService.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-16</date>
// <summary>
//     The ContactUsService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data.CRMService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Youffer.Common.CRMService;
    using Youffer.Common.LogService;
    using Youffer.CRM;

    /// <summary>
    /// Class ContactUsService
    /// </summary>
    public class ContactUsService : IContactUsService
    {
        /// <summary>
        /// VTiger service instance
        /// </summary>
        private readonly IVTigerService vTigerService;

        /// <summary>
        ///  Initializes a new instance of the <see cref="ContactUsService" /> class.
        /// </summary>
        /// <param name="vTigerService">the vtiger service</param>
        /// <param name="loggerService">the logger service</param>
        public ContactUsService(IVTigerService vTigerService, ILoggerService loggerService)
        {
            this.vTigerService = vTigerService;
            this.LoggerService = loggerService;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Create ContactUs Message
        /// </summary>
        /// <param name="contactUs">The VTigerContactUs model</param>
        /// <returns>The VTigerContactUs object</returns>
        public VTigerContactUs CreateContactUsMessage(VTigerContactUs contactUs)
        {
            try
            {
                contactUs = this.vTigerService.Create<VTigerContactUs>(contactUs);
                return contactUs;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Adding ContactUs Message :- " + ex.Message);
            }

            return new VTigerContactUs();
        }

        /// <summary>
        /// Read ContactUs Message
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>The VTigerContactUs object</returns>
        public VTigerContactUs ReadContactUsMessage(string userId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from Contactus Where contactus_tks_isdeleted = 0 and contactus_tks_user = '" + userId + "' order by contactusno desc LIMIT 0, 1; ");
                var query = sb.ToString();
                VTigerContactUs contactUs = this.vTigerService.Query<VTigerContactUs>(query).FirstOrDefault();

                return contactUs;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Read ContactUs Message :- " + ex.Message);
            }

            return new VTigerContactUs();
        }

        /// <summary>
        /// Reads all contact us message.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of VtigerContactUs object.</returns>
        public List<VTigerContactUs> ReadAllContactUsMessage(string userId, int lastpageId, int fetchCount, string sortBy, string direction)
        {
            try
            {
                if (lastpageId < 1)
                {
                    lastpageId = 1;
                }

                var startVal = (lastpageId - 1) * fetchCount;

                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from Contactus Where contactus_tks_isdeleted = 0 and contactus_tks_user = '" + userId + "' LIMIT " + startVal + "," + fetchCount + "; ");
                var query = sb.ToString();
                this.LoggerService.LogException("Reading All ContactUs Messages Query :- " + query);
                IEnumerable<VTigerContactUs> contactUs = this.vTigerService.Query<VTigerContactUs>(query);

                return contactUs.ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Reading All ContactUs Messages :- " + ex.Message);
            }

            return new List<VTigerContactUs>();
        }

        /// <summary>
        /// Marks the contact us messages deleted.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Boolean object.</returns>
        public bool MarkContactUsMessagesDeleted(string userId)
        {
            bool isSuccess = true;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from Contactus Where contactus_tks_isdeleted = 0 and contactus_tks_user = '" + userId + "' LIMIT 0, " + int.MaxValue + "; ");
                var query = sb.ToString();
                IEnumerable<VTigerContactUs> contactUs = this.vTigerService.Query<VTigerContactUs>(query);
                foreach (var item in contactUs)
                {
                    item.contactus_tks_isdeleted = true;
                    this.vTigerService.Update(item);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("Mark ContactUs Messages Deleted :- " + ex.Message);
            }

            return isSuccess;
        }

        /// <summary>
        /// Checks if contact us MSG.
        /// </summary>
        /// <param name="msgid">The msgid.</param>
        /// <returns>Bool object</returns>
        public bool CheckIfContactUsMsg(string msgid)
        {
            bool isContactUs = true;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from Contactus Where contactus_tks_isdeleted = 0 and id = '" + msgid + "';");
                var query = sb.ToString();
                List<VTigerContactUs> contactUs = this.vTigerService.Query<VTigerContactUs>(query).ToList();
                isContactUs = contactUs.Count > 0;
            }
            catch (Exception ex)
            {
                isContactUs = false;
                this.LoggerService.LogException("Check If ContactUs Msg :- " + ex.Message);
            }

            return isContactUs;
        }

        /// <summary>
        /// Deletes the contactus message.
        /// </summary>
        /// <param name="msgid">The msgid.</param>
        /// <returns>Bool object</returns>
        public bool DeleteContactusMessage(string msgid)
        {
            bool isSuccess = true;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from Contactus Where contactus_tks_isdeleted = 0 and id = '" + msgid + "';");
                var query = sb.ToString();
                VTigerContactUs contactUs = this.vTigerService.Query<VTigerContactUs>(query).FirstOrDefault();
                contactUs.contactus_tks_isdeleted = true;
                this.vTigerService.Update(contactUs);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                this.LoggerService.LogException("Delete Contactus Message :- " + ex.Message);
            }

            return isSuccess;
        }
    }
}
