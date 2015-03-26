// ---------------------------------------------------------------------------------------------------
// <copyright file="ICRMManagerService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-20</date>
// <summary>
//     The ICRMManagerService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.CRMService
{
    using System.Collections.Generic;
    using System.Data;
    using Youffer.CRM;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.MySqlDbSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The ICRMManagerService class
    /// </summary>
    public interface ICRMManagerService
    {
        /// <summary>
        /// Add Contacts
        /// </summary>
        /// <param name="contactInfo"> The contact Model. </param>
        /// <param name="appUser"> The Application user. </param>
        /// <returns> ContactModel object</returns>
        ContactModel AddContact(ContactModel contactInfo, ApplicationUserDto appUser = null);

        /// <summary>
        /// Get the contact from the CRM 
        /// </summary>
        /// <param name="contactId"> The Contact Id. </param>
        /// <returns> Contact Model </returns>
        ContactModel GetContact(string contactId);

        /// <summary>
        /// Updates the contact.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="contactInfo">The contact information.</param> 
        /// <returns>ContactModel object.</returns>
        ContactModel UpdateContact(string userId, ContactModel contactInfo);

        /// <summary>
        /// Delete User from Youffer and Conatct from the CRM.
        /// </summary>
        /// <param name="appUser"> The application user. </param>
        /// <returns> bool object </returns>
        bool DeleteContact(ApplicationUserDto appUser);

        /// <summary>
        /// Add Organisation
        /// </summary>
        /// <param name="organisationInfo"> The Organisation Model. </param>
        /// <param name="appUser"> The application user. </param>
        /// <returns> Organisation Model </returns>
        OrganisationModel AddOrganisation(OrganisationModel organisationInfo, ApplicationUserDto appUser = null);

        /// <summary>
        /// Gets the Organisation from the CRM 
        /// </summary>
        /// <param name="organisationId"> The Organisation Id. </param>
        /// <returns> Organisation Model </returns>
        OrganisationModel GetOrganisation(string organisationId);

        /// <summary>
        /// Update the Organisation on the CRM 
        /// </summary>
        /// <param name="organisationId"> The Organisation Id. </param>
        /// <param name="orgInfo"> The Organisation Entity. </param>
        /// <returns> Organisation Model </returns>
        OrganisationModel UpdateOrganisation(string organisationId, OrganisationModel orgInfo);

        /// <summary>
        /// Gets the Lead from the CRM 
        /// </summary>
        /// <param name="leadId"> The Lead Id. </param>
        /// <returns> Lead Model </returns>
        LeadModel GetLead(string leadId);

        /// <summary>
        /// Update the lead in the CRM 
        /// </summary>
        /// <param name="leadId"> The Lead Id. </param>
        /// <param name="leadInfo"> The Lead Entity. </param>
        /// <returns> Lead Model </returns>
        LeadModel UpdateLead(string leadId, LeadModel leadInfo);

        /// <summary>
        /// Searches the leads.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns>List of VTigerLead object.</returns>
        List<VTigerLead> SearchLeads(SearchModelDto searchModel);

        /// <summary>
        /// Trending search.
        /// </summary>
        /// <returns>List of string.</returns>
        List<string> TrendingSearch();

        /// <summary>
        /// Gets the dashboard.
        /// </summary>
        /// <param name="orgModel">The org model.</param>
        /// <param name="lastPageId">The last page identifier.</param>
        /// <param name="fetchCnt">The fetch count.</param>
        /// <param name="interest"> The interest name. </param>
        /// <returns>List of VTigerLead object</returns>
        List<VTigerLead> GetDashboard(OrganisationModel orgModel, int lastPageId, int fetchCnt, string interest);

        /// <summary>
        /// Searches the client for review page.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>List of VTigerPotential object.</returns>
        List<VTigerPotential> SearchClientForReviewPage(string searchText, string companyId, int lastpageId, int fetchCount);

        /// <summary>
        /// Searches the client for my client page.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="companyId"> the company id. </param>
        /// <param name="lastpageId">The last page id.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>List of VTigerPotential object.</returns>
        List<VTigerPotential> SearchClientForMyClientPage(string searchText, string companyId, int lastpageId, int fetchCount);

        /// <summary>
        /// Gets the unreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>List of VTigerPotential object.</returns>
        List<VTigerPotential> GetUnReviewdClients(string companyId, int lastpageId, int fetchCount);

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>List of StatisticsModelDto object.</returns>
        List<StatisticsModelDto> GetStatistics(string companyId);

        /// <summary>
        /// Adds the opportunity.
        /// </summary>
        /// <param name="buyUserModelDto">The buy user model dto.</param>
        /// <returns>Boolean object.</returns>
        bool AddOpportunity(BuyUserModelDto buyUserModelDto);

        /// <summary>
        /// Adds the opportunity.
        /// </summary>
        /// <param name="leadInfo">The lead information.</param>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="price">The price.</param>
        /// <param name="payPalDetailsDto">The pay pal details dto.</param>
        /// <param name="appUser">The application user.</param>
        /// <returns>Boolean object.</returns>
        bool AddOpportunity(LeadModel leadInfo, string companyId, string contactId, double price, PayPalDetailsDto payPalDetailsDto, ApplicationUserDto appUser = null);

        /// <summary>
        /// Gets the opportunities.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// List of VTigerPotential object.
        /// </returns>
        List<VTigerPotential> GetOpportunities(string companyId, int lastpageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc");

        /// <summary>
        /// Determines whether the user can be purchased or not.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>Bpplean object.</returns>
        bool CanPurchaseUser(string companyId, string userId, string interest);

        /// <summary>
        /// Deletes the opportunity.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Boolean object</returns>
        bool DeleteOpportunity(string userId);

        /// <summary>
        /// Gets the owner companies.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of OwnerCompaniesDto object.</returns>
        List<OwnerCompaniesDto> GetOwnerCompanies(string userId);

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable object.</returns>
        DataTable GetQueryResult(string query);

        /// <summary>
        /// Gets the meta data.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="fieldIndex">Index of the field.</param>
        /// <returns>List of VTigerPicklistItem object.</returns>
        List<VTigerPicklistItem> GetMetaData(VTigerType module, int fieldIndex);

        /// <summary>
        /// Reports the company.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>Boolean object.</returns>
        bool ReportCompany(string contactId, string orgId);

        /// <summary>
        /// Reports the user.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>Boolean object.</returns>
        bool ReportUser(string contactId, string orgId);

        /// <summary>
        /// Marks the not call.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>Boolean object.</returns>
        bool MarkNotCall(string contactId, string orgId);

        /// <summary>
        /// Gets the purchased user total amount.
        /// </summary>
        /// <param name="crmContactId">The CRM contact identifier.</param>
        /// <returns>Decimal value</returns>
        decimal GetPurchasedUserTotalAmount(string crmContactId);

        /// <summary>
        /// Checks if user blocked.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>Bool object</returns>
        bool CheckIfUserBlocked(string contactId, string orgId);

        /// <summary>
        /// Get the Query Result Based on Entity
        /// </summary>
        /// <typeparam name="T"> Type of entity</typeparam>
        /// <param name="query"> The query values</param>
        /// <returns> IEnumerable object</returns>
        IEnumerable<T> GetQueryResult<T>(string query) where T : VTigerEntity;

        /// <summary>
        /// Get the Query Result Based on Entity
        /// </summary>
        /// <typeparam name="T"> Type of entity</typeparam>
        /// <param name="interest"> The interest values</param>
        /// <param name="lastseenId"> The last seen id</param>
        /// <param name="fetchCount"> The fetch count</param>
        /// <returns> IEnumerable object</returns>
        IEnumerable<T> GetHomePageLeads<T>(string interest, int lastseenId, int fetchCount) where T : VTigerEntity;

        /// <summary>
        /// Get the Top Leads
        /// </summary>
        /// <param name="topLeadsDto">The top leads Dto.</param>
        /// <returns>List of VTigerDashBoardData</returns>
        List<VTigerDashBoardData> GetTopLeads(TopLeadsDto topLeadsDto);

        /// <summary>
        /// Add Company Notes
        /// </summary>
        /// <param name="notes"> the note </param>
        /// <returns> CRMCompanyNotes obj </returns>
        CRMCompanyNotes AddCompanyNote(CRMCompanyNotes notes);

        /// <summary>
        ///  Retrieving note from CRM
        /// </summary>
        /// <param name="contactId"> the user id. </param>
        /// <param name="companyId"> the company id. </param>
        /// <param name="lastSeenId"> The last seen Id. </param>
        /// <param name="fetchCount"> The fetch Count</param>
        /// <returns> CRMCompanyNotes list </returns>
        List<CRMCompanyNotes> ReadNotes(string contactId, string companyId, int lastSeenId, int fetchCount);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> Boolean object </returns>
        bool DeleteNote(string noteId);

        /// <summary>
        /// Create User review
        /// </summary>
        /// <param name="review">the CRMUserReview model</param>
        /// <returns> CRMUserReview obj </returns>
        CRMUserReview AddUserReview(CRMUserReview review);

        /// <summary>
        /// Read user reviews
        /// </summary>
        /// <param name="contactId"> The contact Id. </param>
        /// <param name="organisationId"> The Organisation Id.</param>
        /// <param name="interest">The interest</param>
        /// <param name="lastPageId">The last page Id</param>
        /// <param name="fetchCount">the fetchCount</param>
        /// <returns> CRMUserReview list </returns>
        List<CRMUserReview> ReadUserReviews(string contactId, string organisationId = "", string interest = "", int lastPageId = 0, int fetchCount = 100000);

        /// <summary>
        /// Update user reviews
        /// </summary>
        /// <param name="review">the CRMUserReview model</param>
        /// <returns> CRMUserReview obj </returns>
        CRMUserReview UpdateUserReview(CRMUserReview review);

        /// <summary>
        /// Get the user rank
        /// </summary>
        /// <param name="contactId"> The contact Id</param>
        /// <returns>rank of user</returns>
        decimal GetUserRank(string contactId);

        /// <summary>
        /// Gets the user reviews.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The list of CRMUserReview model.</returns>
        List<CRMUserReview> GetUserReviews(string userId);

        /// <summary>
        /// Gets the reviews for company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastPageId">The lastPage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of CRMUserReview model.</returns>
        List<CRMUserReview> GetReviewsForCompany(string companyId, int lastPageId, int fetchCount, string sortBy, string direction);

        /// <summary>
        /// Mark user as purchased
        /// </summary>
        /// <param name="userId"> The user Id</param>
        /// <param name="contactId"> The contact Id</param>
        /// <param name="userName">The user name</param>
        /// <param name="organisationId"> The organisation Id</param>
        /// <param name="interestName"> The interest Name</param>
        /// <param name="leadId"> The leadId </param>
        /// <returns> bool object</returns>
        bool MarkUserAsPurchased(string userId, string contactId, string userName, string organisationId, string interestName, string leadId);

        /// <summary>
        /// Send Verification Code
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="phoneNumber"> The Phone Number</param>
        /// <param name="code">THe code number</param>
        /// <param name="message">The message</param>
        /// <returns> bool Object</returns>
        bool SendVerificationCode(string userId, string phoneNumber, string code, string message);

        /// <summary>
        /// Check and Update phone number after code verification
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="code">The code</param>
        /// <param name="errorMessage"> The error Message</param>
        /// <returns>bool object</returns>
        bool UpdatePhoneNumberAfterVerification(string userId, string code, out string errorMessage);

        /// <summary>
        /// THe Dashboard data
        /// </summary>
        /// <param name="orgCRMId"> org Id</param>
        /// <param name="lastPageId"> last page id</param>
        /// <param name="fetchCnt">fetch count</param>
        /// <returns>List of VTigerDashBoardData</returns>
        List<VTigerDashBoardData> GetNewDashboard(string orgCRMId, int lastPageId, int fetchCnt);

        /// <summary> 
        /// Get the searched Leads
        /// </summary>
        /// <param name="orgId"> The org Id</param>
        /// <param name="model"> lThe search model</param> 
        /// <returns>List of VTigerDashBoardData</returns>
        List<VTigerDashBoardData> GetSearchedLeads(string orgId, SearchModelDto model);

        /// <summary>
        /// Gets the unreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="searchText"> The searchText </param>
        /// <returns>List of VTigerPotential object.</returns>
        List<VtigerPotentialData> GetUnReviewdClients1(string companyId, int lastpageId, int fetchCount, string searchText = "");

        /// <summary>
        /// Gets the Count ofunreviewd clients.
        /// </summary>
        /// <param name="companyId">The company identifier.</param> 
        /// <param name="searchText"> The searchText </param>
        /// <returns> Result Count.</returns>
        int GetUnReviewdClientsCount(string companyId, string searchText = "");

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <param name="crmOrgId">The company identifier.</param>
        /// <param name="searchText"> THe searchText</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// List of VtigerPotentialData object.
        /// </returns>
        List<VtigerPotentialData> GetMyClients(string crmOrgId, string searchText = "", int lastpageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc");

        /// <summary>
        /// Create Notification
        /// </summary>
        /// <param name="crmNotification">The CRMNotifications model</param>
        /// <returns>The CRMNotifications object</returns>
        CRMNotifications CreateNotification(CRMNotifications crmNotification);

        /// <summary>
        /// Create ContactUs Message
        /// </summary>
        /// <param name="crmContactUs">The CRMContactUs model</param>
        /// <returns>The CRMContactUs object</returns>
        CRMContactUs CreateContactUsMessage(CRMContactUs crmContactUs);

        /// <summary>
        /// Create ContactUs Message
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>The CRMContactUs object</returns>
        CRMContactUs ReadContactUsMessage(string userId);

        /// <summary>
        /// Reads all contact us message.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of CRMContactUs object.</returns>
        List<CRMContactUs> ReadAllContactUsMessage(string userId, int lastpageId, int fetchCount, string sortBy, string direction);

        /// <summary>
        /// Marks the contact us messages deleted.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Boolean object.</returns>
        bool MarkContactUsMessagesDeleted(string userId);

        /// <summary>
        /// Checks if contact us MSG.
        /// </summary>
        /// <param name="msgId">The MSG identifier.</param>
        /// <returns>Boolean object.</returns>
        bool CheckIfContactUsMsg(string msgId);

        /// <summary>
        /// Deletes the contactus message.
        /// </summary>
        /// <param name="msgId">The MSG identifier.</param>
        /// <returns>Boolean object.</returns>
        bool DeleteContactusMessage(string msgId);

        /// <summary>
        /// Creates the payment request.
        /// </summary>
        /// <param name="crmRequestPayment">The CRM request payment.</param>
        /// <returns>CRMRequestPayment object.</returns>
        CRMRequestPayment CreatePaymentRequest(CRMRequestPayment crmRequestPayment);

        /// <summary>
        /// Updates the parent business types of organisations.
        /// </summary>
        /// <returns>Boolean object.</returns>
        bool UpdateParentBusinessTypesOfOrganisations();

        /// <summary>
        /// Updates the parent interest of users.
        /// </summary>
        /// <returns>Boolean object.</returns>
        bool UpdateParentInterestOfUsers();
    }
}
